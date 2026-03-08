using ProductivityTimer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Application.Services.Enums;

namespace ProductivityTimer.Application.Services
{
    public class TimerService : ITimerService
    {
        private TimeSpan _remainingTime;
        private TimeStateEnum.TimeState _timerState = TimeStateEnum.TimeState.stop;
        public TimeSpan GetRemainingTime()
        {
            throw new NotImplementedException();
        }

        public void PauseTimer()
        {
            throw new NotImplementedException();
        }

        public void ResetTimer()
        {
            throw new NotImplementedException();
        }

        public void StartTimer()
        {
            _timerState = TimeStateEnum.TimeState.running;
        }

        public void StopTimer()
        {
            throw new NotImplementedException();
        }
    }
}
