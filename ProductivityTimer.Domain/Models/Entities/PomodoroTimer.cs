namespace ProductivityTimer.Domain.Models.Entities
{
    public class PomodoroTimer
    {
        public TimeSpan Time { get; set; }
        public bool IsRunning { get; set; }
        public TimeSpan ShortBreak { get; set; }
        public TimeSpan LongBreak { get; set; }
        public bool IsPaused { get; set; }
        public PomodoroPhase CurrentPhase { get; set; }
    }

    public enum PomodoroPhase
    {
        Work,
        ShortBreak,
        LongBreak
    }
}
