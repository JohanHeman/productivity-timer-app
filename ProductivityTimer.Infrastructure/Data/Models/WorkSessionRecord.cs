using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace ProductivityTimer.Infrastructure.Data.Models
{
    public class WorkSessionRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
