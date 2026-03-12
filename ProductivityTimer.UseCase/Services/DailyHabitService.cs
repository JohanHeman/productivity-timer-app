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

        public Task CheckHabitAsync(DailyHabit habit)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHabitAsync(DailyHabit habit)
        {
            throw new NotImplementedException();
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

        public Task UpdateHabitAsync(DailyHabit habit)
        {
            throw new NotImplementedException();
        }
    }
}
