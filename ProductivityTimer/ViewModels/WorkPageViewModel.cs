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
            StartWorkTimerCommand = new Command(StartWorkTimer);
            StopWorkTimerCommand = new Command(StopWorkTimer);
            PauseWorkTimerCommand = new Command(PauseWorkTimer);
            _timerService = timerService;
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
            }
        }
        public void UpdateTimer()
        {
            RemainingTime = _timerService.GetRemainingTime();
            OnPropertyChanged(nameof(TimerText));
        }

        public void StartWorkTimer()
        {
            _timerService.StartTimer();
        }

        public void StopWorkTimer()
        {
            _timerService.StopTimer();
        }

        public void PauseWorkTimer()
        {
            _timerService.PauseTimer();
        }
    }
}
