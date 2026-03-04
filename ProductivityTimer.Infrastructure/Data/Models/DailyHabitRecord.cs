using SQLite;

namespace ProductivityTimer.Infrastructure.Data.Models
{
    internal class DailyHabitRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DailyHabitsListId { get; set; }
    }
}
