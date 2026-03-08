using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Interfaces
{
    public interface ITimerService
    {
        void StartTimer();
        void PauseTimer();
        void StopTimer();
        void ResetTimer();
        TimeSpan GetRemainingTime();
    }
}
