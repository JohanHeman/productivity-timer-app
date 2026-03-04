namespace ProductivityTimer.Domain.Models.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int ToDoListId { get; set; }
        public ToDoList? ToDoList { get; set; }
    }
}
