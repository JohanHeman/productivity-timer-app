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

        public Task<double> GetTotalHoursForMonthAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetTotalHoursForWeekAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task SaveSessionAsync(WorkSession session)
        {
            throw new NotImplementedException();
        }
    }
}
