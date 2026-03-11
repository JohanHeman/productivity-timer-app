using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Application.Services.Enums;

namespace ProductivityTimer.Application.Interfaces
{
    public interface IStatisticService
    {
        Task<TimeSpan> GetTotalHoursAsync(DateTime date, TimeRange range);
    }
}
