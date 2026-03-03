using ProductivityTimer.Domain.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface IWorkSessionRepository
    {
        Task SaveSessionAsync(WorkSession session); // save the entire work block to the database 
        Task<double> GetTotalHoursForDayAsync(DateTime date);
        Task<double> GetTotalHoursForWeekAsync(DateTime date);
        Task<double> GetTotalHoursForMonthAsync(DateTime date);
    }
}
