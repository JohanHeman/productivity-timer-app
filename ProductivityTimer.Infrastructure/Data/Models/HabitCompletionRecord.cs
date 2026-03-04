using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProductivityTimer.Infrastructure.Data.Models
{
    internal class HabitCompletionRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime CompletedDate { get; set; }
        public int DailyHabitRecordId { get; set; }

    }
}
