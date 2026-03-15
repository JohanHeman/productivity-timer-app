using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Application.Services.Enums;
using ProductivityTimer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IWorkSessionRepository _workSessionRepository;
        public StatisticService(IWorkSessionRepository workSessionRepository)
        {
            _workSessionRepository = workSessionRepository;
        }

        public async Task<TimeSpan> GetTotalHoursAsync(DateTime date, TimeRange range)
        {
            // switch expression for the different time ranges from the enum TimeRange
            return range switch
            {
                TimeRange.Day => await _workSessionRepository.GetTotalHoursForDayAsync(date),
                TimeRange.Week => await _workSessionRepository.GetTotalHoursForWeekAsync(date),
                TimeRange.Month => await _workSessionRepository.GetTotalHoursForMonthAsync(date),
                _ => throw new ArgumentOutOfRangeException(nameof(range), range, "Invalid time range")
            };
        }
    }
}
