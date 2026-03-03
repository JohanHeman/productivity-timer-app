using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        Task AddToListAsync(ToDoTask task);
        Task RemoveFromListAsync(ToDoTask task);
        Task CompleteTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
    }
}
