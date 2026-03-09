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
        public async Task ContinueTimerAsync() => await SetState(TimeStateEnum.TimeState.running);
        public TimeSpan GetRemainingTime() => _remainingTime;
        public async Task PauseTimerAsync() => await SetState(TimeStateEnum.TimeState.paused);
        public async Task ResetTimerAsync() => await SetState(TimeStateEnum.TimeState.stopped);
        public async Task StartTimerAsync() => await SetState(TimeStateEnum.TimeState.running);
        public async Task StopTimerAsync() => await SetState(TimeStateEnum.TimeState.stopped);

        public event Action<TimeSpan>? TimeChanged; // creates an event that sends a TimeSpan value

        private async Task SetState(TimeStateEnum.TimeState state)
        {
            switch (state)
            {
                case TimeStateEnum.TimeState.running:
                    _timerState = TimeStateEnum.TimeState.running;
                    await StartTicking(5000);
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

        private async Task StartTicking(int time)
        {
            _remainingTime = TimeSpan.FromSeconds(time);
            while (_timerState == TimeStateEnum.TimeState.running && _remainingTime > TimeSpan.Zero)
            {
                await Task.Delay(1000);
                if (_timerState != TimeStateEnum.TimeState.running) break;

                _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
                TimeChanged?.Invoke(_remainingTime); // firing the event to notify that the time is changed 
            }
        }
    }
}
