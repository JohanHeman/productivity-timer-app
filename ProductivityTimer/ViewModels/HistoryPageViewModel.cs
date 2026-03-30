using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Application.Services.Enums;

namespace ProductivityTimer.ViewModels
{
    public class HistoryPageViewModel : INotifyPropertyChanged
    {
        private readonly IStatisticService _statisticService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public HistoryPageViewModel(IStatisticService statisticService)
        {
            _statisticService = statisticService;
            NavigateHomeCommand = new Command(async () => await GoToHomeAsync());
            ReviewDailyCommand = new Command(async () => await ReviewDailyAsync());
            ReviewWeeklyCommand = new Command(async () => await ReviewWeeklyAsync());
            ReviewMonthlyCommand = new Command(async () => await ReviewMonthlyAsync());
        }
        public ICommand ReviewDailyCommand { get; }
        public ICommand ReviewWeeklyCommand { get; }
        public ICommand ReviewMonthlyCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        private TimeSpan TotalDuration { get; set; }
        public string TotalHoursText => $"{(int)TotalDuration.TotalHours}h {TotalDuration.Minutes}m"; // formating  for display purposes

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async Task ReviewDailyAsync()
        {
            TotalDuration = await _statisticService.GetTotalHoursAsync(DateTime.Today, TimeRange.Day);
            OnPropertyChanged(nameof(TotalDuration));
            OnPropertyChanged(nameof(TotalHoursText));
        }


        private async Task ReviewWeeklyAsync()
        {
            TotalDuration = await _statisticService.GetTotalHoursAsync(DateTime.Today, TimeRange.Week);
            OnPropertyChanged(nameof(TotalDuration));
            OnPropertyChanged(nameof(TotalHoursText));
        }


        private async Task ReviewMonthlyAsync()
        {
            TotalDuration = await _statisticService.GetTotalHoursAsync(DateTime.Today, TimeRange.Month);
            OnPropertyChanged(nameof(TotalDuration));
            OnPropertyChanged(nameof(TotalHoursText));
        }


        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }


    }
}
