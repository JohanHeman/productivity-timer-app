namespace ProductivityTimer.Domain.Models.Entities
{
    public class HabitCompletion
    {
        public int Id { get; set; }
        public DateTime CompletedDate { get; set; }
        public int DailyHabitId { get; set; }
        public DailyHabit? DailyHabit { get; set; }
    }
}
