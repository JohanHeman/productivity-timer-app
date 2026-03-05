using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductivityTimer.Infrastructure.Data.Models;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System.Data.Common;
using Microsoft.Extensions.Logging;
namespace ProductivityTimer.Infrastructure.Repositories
{
    public class DailyHabitRepository : IDailyHabitRepository
    {
        // Mapping each record to the domain model
        private readonly ILogger<DailyHabitRepository> _logger;
        private readonly SQLIteConnectionFactory _connectionFactory;
        public DailyHabitRepository(SQLIteConnectionFactory connectionFactory, ILogger<DailyHabitRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task AddDailyHabitAsync(DailyHabit dailyHabit)
        {
            try
            {

                var database = _connectionFactory.CreateConnection();
                var record = new DailyHabitRecord { Id = dailyHabit.Id, Name = dailyHabit.Name, DailyHabitsListId = dailyHabit.DailyHabitsListId };
                await database.InsertAsync(record);

            }
            catch (SQLite.SQLiteException ex)
            {
                // logs the error
                _logger.LogError(ex, "Failed to add daily habit");
                // throws the exception to the caller
                throw;
            }
        }

        public Task CheckOffDailyHabitAsync(DailyHabit dailyHabit)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DailyHabit>> GetAllDailyHabitsAsync()
        {
            var database = _connectionFactory.CreateConnection();
            var records = await database.Table<DailyHabitRecord>().ToListAsync();
            return records.Select(r => new DailyHabit { Id = r.Id, Name = r.Name, DailyHabitsListId = r.DailyHabitsListId }).ToList();
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
