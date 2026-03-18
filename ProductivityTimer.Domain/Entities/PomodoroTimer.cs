namespace ProductivityTimer.Domain.Entities
{
    public class PomodoroTimer
    {
        public TimeSpan Time { get; set; }
        public bool IsRunning { get; set; }
        public TimeSpan ShortBreak { get; set; }
        public TimeSpan LongBreak { get; set; }
        public bool IsPaused { get; set; }
    }
}
