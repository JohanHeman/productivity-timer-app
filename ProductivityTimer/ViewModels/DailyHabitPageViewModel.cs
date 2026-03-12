using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using ProductivityTimer.Models;

namespace ProductivityTimer.ViewModels
{
    public class DailyHabitPageViewModel : INotifyPropertyChanged
    {

        public ICommand AddDailyHabitCommand { get; }
        private bool _isRefreshing;

        private readonly IDailyHabitService _dailyHabitService;
        public DailyHabitPageViewModel(IDailyHabitService dailyHabitService)
        {
            _dailyHabitService = dailyHabitService;
            AddDailyHabitCommand = new Command(async () => await AddDailyHabitAsync());

        }


        public event PropertyChangedEventHandler? PropertyChanged;




        private string _habitName { get; set; }
        public string HabitName
        {
            get => _habitName;
            set
            {
                _habitName = value;
                OnPropertyChanged(nameof(HabitName));
            }
        }
        public ObservableCollection<DailyHabitRow> DailyHabits { get; } = new();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task AddDailyHabitAsync()
        {
            try
            {
                var habit = new DailyHabit { Name = HabitName };
                if (string.IsNullOrEmpty(HabitName))
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Habit name is required", "OK");
                    return;
                }

                await _dailyHabitService.AddHabitAsync(habit);
                HabitName = string.Empty;
                await LoadHabitsAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to add new habit", "OK");
                HabitName = string.Empty;
            }
        }
        private async Task LoadHabitsAsync()
        {
            _isRefreshing = true;
            try
            {
                DailyHabits.Clear();
                var habits = await _dailyHabitService.GetHabitsAsync();
                foreach (var habit in habits)
                {
                    var streak = await _dailyHabitService.GetHabitStreakAsync(habit);
                    var row = new DailyHabitRow { Habit = habit, Streak = streak, IsCompleted = streak > 0 };
                    row.PropertyChanged += OnHabitRowPropertyChanged;
                    DailyHabits.Add(row);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to load habits", "OK");
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        public Task InitializeAsync() => LoadHabitsAsync();


 
        private async void OnHabitRowPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_isRefreshing) return;
            if (e.PropertyName != nameof(DailyHabitRow.IsCompleted))
                return;

            if (sender is not DailyHabitRow row || !row.IsCompleted)
                return;

            await _dailyHabitService.CheckHabitAsync(row.Habit);
            await LoadHabitsAsync();
        }

    }
}