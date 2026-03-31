using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Entities;
using ProductivityTimer.Models;

namespace ProductivityTimer.ViewModels
{
    public class DailyHabitPageViewModel : INotifyPropertyChanged
    {

        public ICommand AddDailyHabitCommand { get; }
        public ICommand UpdateHabitCommand { get; }
        public ICommand RemoveHabitCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        private bool _isRefreshing; // flag to prevent functions from being called multiple times

        private readonly IDailyHabitService _dailyHabitService;
        public DailyHabitPageViewModel(IDailyHabitService dailyHabitService)
        {
            _dailyHabitService = dailyHabitService;
            AddDailyHabitCommand = new Command(async () => await AddDailyHabitAsync());
            UpdateHabitCommand = new Command<DailyHabitRow>(async (row) => await UpdateHabitAsync(row));
            RemoveHabitCommand = new Command<DailyHabitRow>(async (row) => await RemoveHabitAsync(row));
            NavigateHomeCommand = new Command(async () => await NavigateHomeAsync());
        }

        private async Task NavigateHomeAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async Task RemoveHabitAsync(DailyHabitRow row)
        {
            if (row == null) return;
            try
            {
                await _dailyHabitService.RemoveHabitAsync(row.Habit);
                await LoadHabitsAsync();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to remove habit", "OK");
            }
        }

        private async Task UpdateHabitAsync(DailyHabitRow row)
        {
            if (row == null) return;
            try
            {
                if (!row.IsEditing)
                {
                    row.EditableName = row.Habit.Name; // when entering edit mnode, set the displayed name to the current habit name
                    row.IsEditing = true;
                    return;
                }
                //create a new name, set it to Habit name, and update the habit with the service
                var newName = row.EditableName;
                if (string.IsNullOrWhiteSpace(newName))
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Habit name is required", "OK");
                    return;
                }

                row.Habit.Name = newName;
                await _dailyHabitService.UpdateHabitAsync(row.Habit);
                row.IsEditing = false;
                await LoadHabitsAsync();
            }
            catch (Exception)
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
            catch (Exception)
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
                    var isCompletedToday = await _dailyHabitService.IsHabitCompletedTodayAsync(habit);
                    
                    var streak = await _dailyHabitService.GetHabitStreakAsync(habit);

                    var row = new DailyHabitRow
                    {
                        Habit = habit,
                        Streak = streak,
                        IsCompleted = isCompletedToday,
                        IsEditing = false,
                        EditableName = habit.Name
                    };
                    row.PropertyChanged += OnHabitRowPropertyChanged; // subscribe the method to the DailyHabitRow event
                    DailyHabits.Add(row);
                }
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to load habits", "OK");
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        public Task InitializeAsync() => LoadHabitsAsync(); // to load the habits on startup in xaml.cs file


        // the sender is the DailyHabitRow from LoadHabitsAsync(), and e is the changed property
        private async void OnHabitRowPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_isRefreshing) return; // guard to prevent multiple checks of the same list 
            if (e.PropertyName != nameof(DailyHabitRow.IsCompleted)) // only react when the completion state changes
                return;

            if (sender is not DailyHabitRow row) // Ignore events that are not related
                return;

            try
            {
                if (row.IsCompleted)
                {
                    await _dailyHabitService.CheckHabitAsync(row.Habit);
                }
                else
                {
                    await _dailyHabitService.UnCheckDailyHabitAsync(row.Habit);
                }
                await LoadHabitsAsync();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to check habit", "OK");
            }
        }
    }
}