using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Facade
{
    public class WorkFacade : IWorkFacade
    {
        private DateTime? _StartedSessionAt; // keep track of the start time of the session
        private readonly ITimerService _timerService;

        private readonly IWorkSessionRepository _workSessionRepository;
        public WorkFacade(ITimerService timerService, IWorkSessionRepository workSessionRepository)
        {
            _timerService = timerService;
            _workSessionRepository = workSessionRepository;
            _timerService.TimeChanged += OnTimeChanged; // subscribe to the event 
        }
        public event Action<TimeSpan>? TimeChanged; // event set up to notify the classes that are subscribed to it and pass a timespan value 

        // set up methods with more readable names for the UI   
        public TimeSpan GetCurrentRemainingTime() => _timerService.GetRemainingTime();

        public async Task PauseAsync() => await _timerService.PauseTimerAsync();

        public async Task StartAsync()
        {
            _StartedSessionAt = DateTime.Now;
            await _timerService.StartTimerAsync();
        }

        public async Task StopAndSaveAsync()
        {
            await _timerService.StopTimerAsync();

            if (_StartedSessionAt == null)
                return;

            var endedAt = DateTime.Now;

            var session = CreateSession(_StartedSessionAt.Value, endedAt);
            await _workSessionRepository.SaveSessionAsync(session);
            _StartedSessionAt = null;
        }


        private WorkSession CreateSession(DateTime startedAt, DateTime endedAt)
        {
            return new WorkSession
            {
                StartedAt = startedAt,
                EndedAt = endedAt,
                Duration = endedAt - startedAt
            };
        }
        private void OnTimeChanged(TimeSpan time)
        {
            TimeChanged?.Invoke(time); // fires the event to the classes that are subscribed to it and passes the timespan value
        }

        public async Task ContinueAsync()
        {
            await _timerService.ContinueTimerAsync();
        }
    }
}
