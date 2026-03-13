using ProductivityTimer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTimer.Application.Services.Enums;

namespace ProductivityTimer.Application.Services
{
    public class TimerService : ITimerService
    {
        private bool _isTicking;
        private TimeSpan _remainingTime;
        private TimeSpan _defaultTime = TimeSpan.FromMinutes(50);
        private TimeStateEnum.TimeState _timerState = TimeStateEnum.TimeState.stopped;
        public async Task ContinueTimerAsync()
        {
            if (_remainingTime <= TimeSpan.Zero)
                _remainingTime = _defaultTime;
            await SetState(TimeStateEnum.TimeState.running);
        }
        public TimeSpan GetRemainingTime() => _remainingTime;
        public async Task PauseTimerAsync() => await SetState(TimeStateEnum.TimeState.paused);
        public async Task ResetTimerAsync() => await SetState(TimeStateEnum.TimeState.stopped);
        public async Task StartTimerAsync()
        {
            _remainingTime = _defaultTime;
            TimeChanged?.Invoke(_remainingTime); // it starts at given time and not one second later ex 50:00 wont start at 49:59s
            await SetState(TimeStateEnum.TimeState.running);
        }
        public async Task StopTimerAsync() => await SetState(TimeStateEnum.TimeState.stopped);

        public event Action<TimeSpan>? TimeChanged;

        private async Task SetState(TimeStateEnum.TimeState state)
        {
            switch (state)
            {
                case TimeStateEnum.TimeState.running:
                    _timerState = TimeStateEnum.TimeState.running;
                    if (!_isTicking)
                        await StartTickingAsync();
                    break;
                case TimeStateEnum.TimeState.paused:
                    _timerState = TimeStateEnum.TimeState.paused;
                    break;
                case TimeStateEnum.TimeState.stopped:
                    _timerState = TimeStateEnum.TimeState.stopped;
                    break;
                case TimeStateEnum.TimeState.onBreak:
                    _timerState = TimeStateEnum.TimeState.onBreak;
                    if (!_isTicking)
                        await StartTickingAsync();
                    break;
                default:
                    throw new ArgumentException("Invalid state");
            }

        }

        private async Task StartTickingAsync()
        {
            _isTicking = true;
            try
            {
                while (_isTicking && (_timerState == TimeStateEnum.TimeState.running
                || _timerState == TimeStateEnum.TimeState.onBreak) && _remainingTime > TimeSpan.Zero)
                {
                    await Task.Delay(1000);
                    if (_timerState != TimeStateEnum.TimeState.running && _timerState != TimeStateEnum.TimeState.onBreak) break;

                    _remainingTime -= TimeSpan.FromSeconds(1);
                    TimeChanged?.Invoke(_remainingTime); // firing the event and sends the remainingtime value to the facade class 
                }
            }
            finally
            {
                _isTicking = false; // if loop ends stop ticking 
            }
        }

        public async Task BreakTimerAsync()
        {
            _remainingTime = TimeSpan.FromMinutes(10);
            TimeChanged?.Invoke(_remainingTime);
            await SetState(TimeStateEnum.TimeState.onBreak);
        }
    }
}
