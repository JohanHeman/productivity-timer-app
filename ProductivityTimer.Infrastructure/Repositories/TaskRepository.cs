using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using ProductivityTimer.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SQLIteConnectionFactory _connectionFactory;
        public TaskRepository(SQLIteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public Task AddToListAsync(ToDoTask task)
        {
            throw new NotImplementedException();
        }

        public Task CompleteTaskAsync(ToDoTask task)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromListAsync(ToDoTask task)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(ToDoTask task)
        {
            throw new NotImplementedException();
        }
    }
}
