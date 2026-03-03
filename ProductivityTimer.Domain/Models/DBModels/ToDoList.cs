namespace ProductivityTimer.Domain.Models.DBModels
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ToDoTask> Tasks { get; set; } = new List<ToDoTask>();
    }
}
