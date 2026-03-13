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

        private int _streak;
        public int Streak
        {
            get => _streak;
            set
            {
                if (_streak == value) return; // if streak = value, no need to udate anything 
                _streak = value;
                OnPropertyChanged(nameof(Streak));
            }
        }

        private bool _isCompleted;
        public string Name => Habit.Name;

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing == value) return;
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsEdting));
            }
        }

        public bool IsEdting
        {
            get => IsEditing;
            set => IsEditing = value;
        }

        private string _editableName = string.Empty;
        public string EditableName
        {
            get => _editableName;
            set
            {
                if (_editableName == value) return; // if editableName = value, no need to update anything
                _editableName = value;
                OnPropertyChanged(nameof(EditableName));
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return; // if isCompleted = value, no need to update anything
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
