namespace ProductivityTimer.Domain.Models.Entities
{
    public class DailyHabit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DailyHabitsListId { get; set; }
        public DailyHabitsList? DailyHabitsList { get; set; }
        public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
    }
}
