using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProductivityTimer.Infrastructure.Data.Models
{
    internal class DailyHabitList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
