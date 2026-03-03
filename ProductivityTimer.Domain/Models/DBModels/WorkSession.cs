using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Models.DBModels
{
    public class WorkSession
    {
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
