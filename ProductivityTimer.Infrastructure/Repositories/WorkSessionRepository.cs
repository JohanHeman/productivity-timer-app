using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        private readonly ILogger<WorkSessionRepository> _logger;
        private readonly SQLIteConnectionFactory _connectionFactory;
        public WorkSessionRepository(SQLIteConnectionFactory connectionFactory, ILogger<WorkSessionRepository> logger)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }


        public async Task<double> GetTotalHoursForDayAsync(DateTime date)
        {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);
            try
            {
                var database = _connectionFactory.CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= dayStart && r.StartedAt < dayEnd).ToListAsync(); // gets the sessions for one day 
                return sessions.Sum(r => r.Duration.TotalHours); // sums the duration of the sessions for the day
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for day");
                throw;
            }
        }

        public async Task<double> GetTotalHoursForMonthAsync(DateTime date)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1);
            var monthEnd = monthStart.AddMonths(1);
            try
            {
                var database = _connectionFactory.CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= monthStart && r.StartedAt < monthEnd).ToListAsync();
                return sessions.Sum(r => r.Duration.TotalHours);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for month");
                throw;
            }
        }

        public async Task<double> GetTotalHoursForWeekAsync(DateTime date)
        {
            var day = date.Date; // todays date 
            var daysSinceMonday = ((int)day.DayOfWeek + 6) % 7; // converts dayofweek enum into an int, adds 6 to it so we can run % 7 to see the difference between today and monday example sunday + 6 = 6 % 7 = 6
            var weekStart = day.AddDays(-daysSinceMonday);
            var weekEnd = weekStart.AddDays(7);
            try
            {
                var database = _connectionFactory.CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= weekStart && r.StartedAt < weekEnd).ToListAsync();
                return sessions.Sum(r => r.Duration.TotalHours);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for week");
                throw;
            }
        }

        public async Task SaveSessionAsync(WorkSession session)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                WorkSessionRecord record = new WorkSessionRecord { Duration = session.Duration, StartedAt = session.StartedAt, EndedAt = session.EndedAt };
                await database.InsertAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to save session");
                throw;
            }
        }
    }
}
