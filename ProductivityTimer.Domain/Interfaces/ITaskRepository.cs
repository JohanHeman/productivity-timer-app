using ProductivityTimer.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync(); // getting them into a readable list 
        Task AddToListAsync(ToDoTask task);
        Task RemoveFromListAsync(ToDoTask task);
        Task CompleteTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
    }
}
