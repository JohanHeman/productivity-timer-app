using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Interfaces
{
    public interface ITimerService
    {
        event Action<TimeSpan>? TimeChanged;
        Task StartTimerAsync();
        Task PauseTimerAsync();
        Task StopTimerAsync();
        Task ResetTimerAsync();
        TimeSpan GetRemainingTime();
        Task ContinueTimerAsync();
        Task BreakTimerAsync();
    }
}
