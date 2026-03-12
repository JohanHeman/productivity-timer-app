using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProductivityTimer.Domain.Models.Entities;

namespace ProductivityTimer.Models
{
    public class DailyHabitRow : INotifyPropertyChanged
    {
        public DailyHabit Habit { get; set; }
        public int Streak { get; set; }
        private bool _isCompleted { get; set; }
        public string Name => Habit.Name;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // sends a notification to the view that the property has changed
        }
    }
}
