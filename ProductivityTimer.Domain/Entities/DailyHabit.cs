namespace ProductivityTimer.Domain.Entities
{
    public class DailyHabit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
    }
}
