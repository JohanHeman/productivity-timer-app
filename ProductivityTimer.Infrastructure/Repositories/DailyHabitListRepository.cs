using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductivityTimer.Infrastructure.Data.Models;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class DailyHabitListRepository : IDailyHabitRepository
    {
        private readonly IDailyHabitRepository _dailyRepo;
        public DailyHabitListRepository(IDailyHabitRepository dailyrepo)
        {
            _dailyRepo = dailyrepo;
        }
        public Task AddDailyHabitAsync(DailyHabit dailyHabit)
        {
            throw new NotImplementedException();
        }

        public Task CheckOffDailyHabitAsync(DailyHabit dailyHabit)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DailyHabit>> GetAllDailyHabitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDailyHabitAsync(DailyHabit dailyHabit)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDailyHabitAsync(DailyHabit dailyHabit)
        {
            throw new NotImplementedException();
        }
    }
}
