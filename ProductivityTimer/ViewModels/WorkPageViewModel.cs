using ProductivityTimer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace ProductivityTimer.ViewModels
{
    public class WorkPageViewModel : INotifyPropertyChanged
    {
        private readonly ITimerService _timerService;
        public WorkPageViewModel(ITimerService timerService)
        {
            NavigateHomeCommand = new Command(async () => await GoToHomeAsync());
            StartWorkTimerCommand = new Command(async () => await StartWorkTimerAsync());
            StopWorkTimerCommand = new Command(async () => await StopWorkTimerAsync());
            PauseWorkTimerCommand = new Command(async () => await PauseWorkTimerAsync());
            _timerService = timerService;
            _timerService.TimeChanged += OnTimeChanged;
        }

        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand StartWorkTimerCommand { get; }
        public ICommand StopWorkTimerCommand { get; }
        public ICommand PauseWorkTimerCommand { get; }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string TimerText => RemainingTime.ToString(@"mm\:ss");
        private TimeSpan _remainingTime;
        public TimeSpan RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
                OnPropertyChanged(nameof(TimerText));
            }
        }

        private void OnTimeChanged(TimeSpan time)
        {
            RemainingTime = time;
        }

        private async Task StartWorkTimerAsync() => await _timerService.StartTimerAsync();
        private async Task StopWorkTimerAsync() => await _timerService.StopTimerAsync();
        private async Task PauseWorkTimerAsync() => await _timerService.PauseTimerAsync();

    }
}
