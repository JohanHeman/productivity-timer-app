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
        private TimeStateEnum.TimeState _timerState = TimeStateEnum.TimeState.stopped;
        public void ContinueTimer() => SetState(TimeStateEnum.TimeState.running);
        public TimeSpan GetRemainingTime() => _remainingTime;
        public void PauseTimer() => SetState(TimeStateEnum.TimeState.paused);
        public void ResetTimer() => SetState(TimeStateEnum.TimeState.stopped);
        public void StartTimer() => SetState(TimeStateEnum.TimeState.running);
        public void StopTimer() => SetState(TimeStateEnum.TimeState.stopped);

        private void SetState(TimeStateEnum.TimeState state)
        {
            switch (state)
            {
                case TimeStateEnum.TimeState.running:
                    _timerState = TimeStateEnum.TimeState.running;
                    // make timer run 
                    break;
                case TimeStateEnum.TimeState.paused:
                    _timerState = TimeStateEnum.TimeState.paused;
                    // make timer pause
                    break;
                case TimeStateEnum.TimeState.stopped:
                    _timerState = TimeStateEnum.TimeState.stopped;
                    // make timer stop
                    break;
                default:
                    throw new ArgumentException("Invalid state");
            }

        }


    }
}
