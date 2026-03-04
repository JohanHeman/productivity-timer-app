using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using ProductivityTimer.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class WorkSessionRepository : IWorkSessionRepository
    {
        private readonly SQLIteConnectionFactory _connectionFactory;
        public WorkSessionRepository(SQLIteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public Task<double> GetTotalHoursForDayAsync(DateTime date)
        {
            throw new NotImplementedException();
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
