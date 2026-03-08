using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;

namespace ProductivityTimer.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger<MainPageViewModel> _logger;
        public ICommand WorkNavigationCommand { get; }
        public ICommand HistoryNavigationCommand { get; }
        public ICommand QuitCommand { get; }

        public MainPageViewModel(IQuoteService quoteService, ILogger<MainPageViewModel> logger)
        {
            WorkNavigationCommand = new Command(async () => await GoToWorkAsync());
            HistoryNavigationCommand = new Command(async () => await GoToHistoryAsync());
            QuitCommand = new Command(() => Application.Current?.Quit());
            _quoteText = string.Empty;
            _quoteService = quoteService;
            _logger = logger;
        }

        private string _quoteText { get; set; }
        public string QuoteText
        {
            get => _quoteText;
            set
            {
                _quoteText = value;
                OnPropertyChanged(nameof(QuoteText));
            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task GoToWorkAsync()
        {
            await Shell.Current.GoToAsync("WorkPage");
        }

        private async Task GoToHistoryAsync()
        {
            await Shell.Current.GoToAsync("HistoryPage");
        }


        private async Task GetQuoteAsync()
        {
            var quote = await _quoteService.GetQuoteAsync();
            QuoteText = quote.Text;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await GetQuoteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get quote");
                QuoteText = "Failed to get quote";
            }
        }
    }

}
