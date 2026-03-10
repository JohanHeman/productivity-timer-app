using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Application.Facade
{
    public interface IWorkFacade
    {
        event Action<TimeSpan>? TimeChanged;
        Task StartAsync();
        Task PauseAsync();
        Task StopAndSaveAsync();

        TimeSpan GetRemainingTime();
    }
}
