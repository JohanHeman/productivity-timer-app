using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProductivityTimer.Infrastructure.Data.Models
{
    public class DailyHabitListRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
