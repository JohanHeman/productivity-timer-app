using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace ProductivityTimer.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand WorkNavigationCommand { get; }
        public ICommand HistoryNavigationCommand { get; }
        public ICommand QuitCommand { get; }

        public MainPageViewModel()
        {
            WorkNavigationCommand = new Command(async () => await GoToWorkAsync());
            HistoryNavigationCommand = new Command(async () => await GoToHistoryAsync());
            QuitCommand = new Command(() => Application.Current?.Quit());
            _quoteText = string.Empty;
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
    }
}
