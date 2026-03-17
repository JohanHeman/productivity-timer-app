using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Entities;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        private readonly ILogger<WorkSessionRepository> _logger;
        public WorkSessionRepository(ILogger<WorkSessionRepository> logger)
        {
            _logger = logger;
        }


        public async Task<TimeSpan> GetTotalHoursForDayAsync(DateTime date)
        {
            var dayStart = date.Date; // starts at first hour of the day 
            var dayEnd = dayStart.AddDays(1); // 24 hours later
            try
            {
                var database = SQLiteConnectionFactory.GetConnectionFactory().CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= dayStart && r.StartedAt < dayEnd).ToListAsync(); // gets the sessions for one day 
                return TimeSpan.FromHours(sessions.Sum(r => r.Duration.TotalHours)); // Sum all session durations, and convert them into a timespan
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for day");
                throw;// Throws the error to the caller
            }
        }

        public async Task<TimeSpan> GetTotalHoursForWeekAsync(DateTime date)
        {
            var day = date.Date; // todays date 
            var daysSinceMonday = ((int)day.DayOfWeek + 6) % 7; // converts dayofweek enum into an int, adds 6 to it so we can run % 7 to see the difference between today and monday example sunday + 6 = 6 % 7 = 6 or tuesday = 2 2 + 6 = 8 % 7 = 1
            var weekStart = day.AddDays(-daysSinceMonday); // gives the start of the week
            var weekEnd = weekStart.AddDays(7);
            try
            {
                var database = SQLiteConnectionFactory.GetConnectionFactory().CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= weekStart && r.StartedAt < weekEnd).ToListAsync();
                return TimeSpan.FromHours(sessions.Sum(r => r.Duration.TotalHours)); // converts the total hours into a timespan
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for week");
                throw;
            }
        }
        public async Task<TimeSpan> GetTotalHoursForMonthAsync(DateTime date)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1); // starts with the first of date of the month 
            var monthEnd = monthStart.AddMonths(1); // ends at the start of the next month
            try
            {
                var database = SQLiteConnectionFactory.GetConnectionFactory().CreateConnection();
                var sessions = await database.Table<WorkSessionRecord>().Where(r => r.StartedAt >= monthStart && r.StartedAt < monthEnd).ToListAsync();
                return TimeSpan.FromHours(sessions.Sum(r => r.Duration.TotalHours)); // converts the total hours into a timespan
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get total hours for month");
                throw;
            }
        }


        public async Task SaveSessionAsync(WorkSession session)
        {
            try
            {
                var database = SQLiteConnectionFactory.GetConnectionFactory().CreateConnection();
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
