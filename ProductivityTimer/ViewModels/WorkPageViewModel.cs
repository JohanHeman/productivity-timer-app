using Plugin.Maui.Audio;
using ProductivityTimer.Application.Facade;
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
        private bool _isTimerZero;
        private bool IsSessionStarted;
        private bool IsSPaused;
        public string SessionbuttonText => IsSessionStarted ? "Stop" : "Start";
        public string PauseButtonText => IsSPaused ? "continue" : "Pause";

        // from plugin.maui.audio library 
        private readonly IAudioManager _audioManager; // manages the audio files
        private IAudioPlayer? _audioPlayer; // plays the audio files

        private readonly IWorkFacade _workFacade; // creating instance of facade and calling more readable methods for the UI

        public WorkPageViewModel(IWorkFacade workfacade, IAudioManager audioManager)
        {
            _audioManager = audioManager;
            NavigateHomeCommand = new Command(async () => await GoToHomeAsync());
            _workFacade = workfacade;
            _workFacade.TimeChanged += OnTimeChanged; // subscribe OnTimeChanged to the TimeChanged event, so when TimeChanged event fires, OnTimeChanged method runs
            SessionCommand = new Command(async () => await OnSessionCommandExecuted());
            StateCommand = new Command(async () => await OnStateCommandExecuted());
            BreakCommand = new Command(async () => await OnBreakCommandExecuted());
        }

        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        // commands for buttons
        public ICommand NavigateHomeCommand { get; }
        public ICommand SessionCommand { get; }
        public ICommand StateCommand { get; }
        public ICommand BreakCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // timer text
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
            // time is passed from facade TimeChanged event
            RemainingTime = time; // updates remaining time to the ui

            if (time <= TimeSpan.Zero && !_isTimerZero) // whenever time is 0 and flag = false play alarm sound
            {
                _isTimerZero = true;
                if (_audioPlayer != null)
                {
                    _audioPlayer.Stop(); // reset sound
                    _audioPlayer.Play(); // play alarm sound
                }
            }
        }
        private async Task OnSessionCommandExecuted()
        {
            if (!IsSessionStarted)
            {
                IsSessionStarted = true;
                _isTimerZero = false;

                if (_audioPlayer == null)
                {
                    var stream = await FileSystem.OpenAppPackageFileAsync("Alarm.mp3"); // opens the alarm sound file
                    _audioPlayer = _audioManager.CreatePlayer(stream); // creates a player for the alarm sound
                }
                OnPropertyChanged(nameof(SessionbuttonText));
                await _workFacade.StartAsync();
            }
            else
            {
                IsSessionStarted = false;
                OnPropertyChanged(nameof(SessionbuttonText));
                await _workFacade.StopAndSaveAsync();
            }
        }

        private async Task OnStateCommandExecuted()
        {
            if (!IsSPaused)
            {
                IsSPaused = true;
                OnPropertyChanged(nameof(PauseButtonText));
                await _workFacade.PauseAsync();
            }
            else
            {
                IsSPaused = false;
                _audioPlayer?.Stop();
                _isTimerZero = false;
                OnPropertyChanged(nameof(PauseButtonText));
                await _workFacade.ContinueAsync();
            }
        }

        private async Task OnBreakCommandExecuted()
        {
            if (_audioPlayer != null)
            {
                _audioPlayer.Stop();
            }
            _isTimerZero = false;
            await _workFacade.BreakAsync();
        }
    }
}
