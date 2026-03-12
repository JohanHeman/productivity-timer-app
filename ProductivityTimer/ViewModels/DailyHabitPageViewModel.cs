using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Models.Entities;

namespace ProductivityTimer.ViewModels
{
    public class DailyHabitPageViewModel : INotifyPropertyChanged
    {

        public ICommand AddDailyHabitCommand { get; }
        public ICommand CheckOffDailyHabitCommand { get; }
        public ICommand GetAllDailyHabitsCommand { get; }
        // i want to make one list displayed when you go to the page, here you can see your daily habits, and the streak for them too
        // you can add a new task in an input box, you can check of tasks on a little check box, and you can remove them by pressing remove, and then clicking on a task to remove it.

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
        public ObservableCollection<DailyHabit> DailyHabits { get; } = new();

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
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to add new habit", "OK");
                HabitName = string.Empty;
            }
        }
        private async Task LoadHabitsAsync()
        {
            try
            {
                DailyHabits.Clear();
                var habits = await _dailyHabitService.GetHabitsAsync();
                foreach (var habit in habits)
                {
                    DailyHabits.Add(habit);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Failed to load habits", "OK");
            }
        }

        public Task InitializeAsync() => LoadHabitsAsync();

    }
}
