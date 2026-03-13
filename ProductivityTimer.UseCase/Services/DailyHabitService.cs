using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Services
{
    public class DailyHabitService : IDailyHabitService
    {

        private readonly IDailyHabitRepository _dailyHabitRepository;
        public DailyHabitService(IDailyHabitRepository dailyHabitRepository)
        {
            _dailyHabitRepository = dailyHabitRepository;
        }
        public async Task AddHabitAsync(DailyHabit habit)
        {
            try
            {
                await _dailyHabitRepository.AddDailyHabitAsync(habit);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add habit", ex);
            }
        }

        public async Task CheckHabitAsync(DailyHabit habit)
        {
            try
            {
                var completions = await _dailyHabitRepository.GetCompletionsForDailyHabitAsync(habit);
                // prevent multiple checks on the same day
                var alreadyCheckedToday = completions.Any(h => h.CompletedDate.Date == DateTime.Today);

                if (alreadyCheckedToday)
                    return;

                await _dailyHabitRepository.CheckOffDailyHabitAsync(habit);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to check off habit", ex);
            }
        }

        public async Task DeleteHabitAsync(DailyHabit habit)
        {
            try
            {
                await _dailyHabitRepository.RemoveDailyHabitAsync(habit);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete habit", ex);
            }
        }

        public async Task<IReadOnlyList<DailyHabit>> GetHabitsAsync()
        {
            try
            {
                var habits = await _dailyHabitRepository.GetAllDailyHabitsAsync();
                return habits.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get habits", ex);
            }
        }

        public async Task<int> GetHabitStreakAsync(DailyHabit habit)
        {
            var completions = await _dailyHabitRepository.GetCompletionsForDailyHabitAsync(habit); // gets history of completions

            var completedDates = completions.Select(h => h.CompletedDate.Date).Distinct(); // ensures there are no duplicates
            if (!completedDates.Any()) return 0; // check if streak is 0

            var completedDateLookup = new HashSet<DateTime>(completedDates); // creates hashet of completed dates for fast lookup avg (o(1))
            var currentDate = completedDates.Max(); // streaks start at the last completed date to get the latest date of the streak 

            int streak = 0;
            // goes one day at a time backwars until streak breaks, then returns the streak
            while (completedDateLookup.Contains(currentDate))
            {
                streak++;
                currentDate = currentDate.AddDays(-1);
            }
            return streak;
        }

        public async Task UpdateHabitAsync(DailyHabit habit)
        {
            try
            {
                await _dailyHabitRepository.UpdateDailyHabitAsync(habit);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update habit", ex);
            }
        }
    }
}
