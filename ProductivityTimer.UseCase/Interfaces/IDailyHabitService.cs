using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Interfaces
{
    public interface IDailyHabitService
    {
        Task<IReadOnlyList<DailyHabit>> GetHabitsAsync();
        Task AddHabitAsync(DailyHabit habit);
        Task CheckHabitAsync(DailyHabit habit);
        Task UpdateHabitAsync(DailyHabit habit);
        Task RemoveHabitAsync(DailyHabit habit);
        Task<int> GetHabitStreakAsync(DailyHabit habit);
        Task UnCheckDailyHabitAsync(DailyHabit habit);
    }
}
