using ProductivityTimer.Application.Facade;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application
{
    internal class WorkFacade : IWorkFacade
    {
        private DateTime? _StartedSessionAt;
        private readonly ITimerService _timerService;

        private readonly IWorkSessionRepository _workSessionRepository;
        public WorkFacade(ITimerService timerService, IWorkSessionRepository workSessionRepository)
        {
            _timerService = timerService;
            _workSessionRepository = workSessionRepository;
        }
        public event Action<TimeSpan>? TimeChanged;

        public TimeSpan GetRemainingTime() => _timerService.GetRemainingTime();

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
            var session = new WorkSession
            {
                StartedAt = _StartedSessionAt.Value,
                EndedAt = endedAt,
                Duration = endedAt - _StartedSessionAt.Value
            };

            await _workSessionRepository.SaveSessionAsync(session);
            _StartedSessionAt = null;
        }
    }
}
