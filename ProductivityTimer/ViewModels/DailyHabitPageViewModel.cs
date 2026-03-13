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
        public ICommand UpdateHabitCommand { get; }
        public ICommand RemoveHabitCommand { get; }
        private bool _isRefreshing; // flag to prevent functions from being called multiple times

        private readonly IDailyHabitService _dailyHabitService;
        public DailyHabitPageViewModel(IDailyHabitService dailyHabitService)
        {
            _dailyHabitService = dailyHabitService;
            AddDailyHabitCommand = new Command(async () => await AddDailyHabitAsync());
            UpdateHabitCommand = new Command<DailyHabitRow>(async (row) => await UpdateHabitAsync(row));
            RemoveHabitCommand = new Command<DailyHabitRow>(async (row) => await RemoveHabitAsync(row));
        }

        private async Task RemoveHabitAsync(DailyHabitRow row)
        {
            if (row == null) return;
            try
            {
                await _dailyHabitService.RemoveHabitAsync(row.Habit);
                await LoadHabitsAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to remove habit", "OK");
            }
        }

        private async Task UpdateHabitAsync(DailyHabitRow row)
        {
            try
            {
                if (row == null) return;
                await _dailyHabitService.UpdateHabitAsync(row.Habit);
                await LoadHabitsAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to update habit", "OK");
            }
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
            if (_isRefreshing) return; // guard to prevent multiple loads of the same list 
            _isRefreshing = true;
            try
            {
                DailyHabits.Clear();
                var habits = await _dailyHabitService.GetHabitsAsync();
                foreach (var habit in habits) // foreach habit, create a new DailyHabitRow for the view
                {
                    var streak = await _dailyHabitService.GetHabitStreakAsync(habit);
                    var row = new DailyHabitRow { Habit = habit, Streak = streak, IsCompleted = streak > 0 };
                    row.PropertyChanged += OnHabitRowPropertyChanged; // subscribe the method to the DailyHabitRow event
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


        // the sender is the DailyHabitRow from LoadHabitsAsync(), and e is the changed property
        private async void OnHabitRowPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_isRefreshing) return; // guard to prevent multiple checks of the same list 
            if (e.PropertyName != nameof(DailyHabitRow.IsCompleted)) // only react when the completion state changes
                return;

            if (sender is not DailyHabitRow row || !row.IsCompleted) // Ignore events that are not related, and if the senders checkbox is not completed
                return;

            await _dailyHabitService.CheckHabitAsync(row.Habit); // saves 
            await LoadHabitsAsync(); // reloads the list
        }

    }
}