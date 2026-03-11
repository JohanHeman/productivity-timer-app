using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface IWorkSessionRepository
    {
        Task SaveSessionAsync(WorkSession session); // save the entire work block to the database 
        Task<TimeSpan> GetTotalHoursForDayAsync(DateTime date);
        Task<TimeSpan> GetTotalHoursForWeekAsync(DateTime date);
        Task<TimeSpan> GetTotalHoursForMonthAsync(DateTime date);
    }
}
