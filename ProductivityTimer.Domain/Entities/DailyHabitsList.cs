namespace ProductivityTimer.Domain.Entities
{
    public class DailyHabitsList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<DailyHabit> Habits { get; set; } = new List<DailyHabit>();
    }
}
