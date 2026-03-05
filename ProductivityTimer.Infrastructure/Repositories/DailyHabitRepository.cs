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

        public async Task CheckOffDailyHabitAsync(DailyHabit dailyHabit)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var record = new HabitCompletionRecord { Id = dailyHabit.Id, DailyHabitRecordId = dailyHabit.Id, CompletedDate = DateTime.Now };
                await database.InsertAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to check off daily habit");
                throw;
            }
        }

        public async Task<IEnumerable<DailyHabit>> GetAllDailyHabitsAsync()
        {
            try
            {

                var database = _connectionFactory.CreateConnection();
                var records = await database.Table<DailyHabitRecord>().ToListAsync();
                return records.Select(r => new DailyHabit { Id = r.Id, Name = r.Name, DailyHabitsListId = r.DailyHabitsListId }).ToList();

            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get all daily habits");
                throw;
            }
        }

        public async Task RemoveDailyHabitAsync(DailyHabit dailyHabit)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                await database.DeleteAsync(dailyHabit);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to remove daily habit");
                throw;
            }
        }
        public async Task UpdateDailyHabitAsync(DailyHabit dailyHabit)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                DailyHabitRecord record = new DailyHabitRecord()
                {
                    Id = dailyHabit.Id,
                    Name = dailyHabit.Name,
                    DailyHabitsListId = dailyHabit.DailyHabitsListId
                };
                await database.UpdateAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to update daily habit");
                throw;
            }
        }
        public async Task<IEnumerable<HabitCompletion>> GetCompletionsForDailyHabitAsync(DailyHabit dailyHabit)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var records = await database.Table<HabitCompletionRecord>()
                    .Where(r => r.DailyHabitRecordId == dailyHabit.Id)
                    .ToListAsync();

                return records.Select(r => new HabitCompletion
                {
                    Id = r.Id,
                    DailyHabitId = r.DailyHabitRecordId,
                    CompletedDate = r.CompletedDate
                }).ToList();
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get streaks for daily habit");
                throw;
            }
        }
    }
}
