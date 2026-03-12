using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using ProductivityTimer.Application.Services;
using ProductivityTimer.Domain.Interfaces;

namespace ProductivityTimer.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IQuoteService _quoteService;
        private readonly ILogger<MainPageViewModel> _logger;
        private readonly QuoteApplicationService _getQuoteService;
        public ICommand WorkNavigationCommand { get; }
        public ICommand HistoryNavigationCommand { get; }
        public ICommand QuitCommand { get; }

        // displays quote text and navígation to other pages 

        public MainPageViewModel(IQuoteService quoteService, ILogger<MainPageViewModel> logger, QuoteApplicationService getQuoteService)
        {
            WorkNavigationCommand = new Command(async () => await GoToWorkAsync());
            HistoryNavigationCommand = new Command(async () => await GoToHistoryAsync());
            QuitCommand = new Command(() => Microsoft.Maui.Controls.Application.Current?.Quit());
            _quoteText = string.Empty;
            _quoteService = quoteService;
            _logger = logger;
            _getQuoteService = getQuoteService;
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

        public async Task InitializeAsync()
        {
            try
            {
                var quote = await _getQuoteService.GetQuoteAsync();
                QuoteText = $"{quote.Text}\n - {quote.Author}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get quote");
                QuoteText = "Failed to get quote";
            }
        }
    }

}
