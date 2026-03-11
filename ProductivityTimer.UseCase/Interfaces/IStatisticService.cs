using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Application.Enums;

namespace ProductivityTimer.Application.Interfaces
{
    public interface IStatisticService
    {
        Task<double> GetTotalHoursAsync(DateTime date, TimeRange range);
    }
}
