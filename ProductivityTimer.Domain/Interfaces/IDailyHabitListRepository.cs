using ProductivityTimer.Domain.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface IDailyHabitRepository
    {
        Task<IEnumerable<DailyHabit>> GetAllDailyHabitsAsync();
        Task AddDailyHabitAsync(DailyHabit dailyHabit);
        Task RemoveDailyHabitAsync(DailyHabit dailyHabit);
        Task UpdateDailyHabitAsync(DailyHabit dailyHabit);
        Task CheckOffDailyHabitAsync(DailyHabit dailyHabit);
    }
}
