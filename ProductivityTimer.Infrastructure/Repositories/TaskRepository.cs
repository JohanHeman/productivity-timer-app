using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Domain.Models.Entities;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly SQLIteConnectionFactory _connectionFactory;
        public TaskRepository(SQLIteConnectionFactory connectionFactory, ILogger<TaskRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async Task AddToListAsync(ToDoTask task)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var record = new ToDoTaskRecord { TaskName = task.TaskName, ToDoListRecordId = task.ToDoListId };
                await database.InsertAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to add task to list");
                throw;
            }
        }

        public async Task CompleteTaskAsync(ToDoTask task)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var record = await database.Table<ToDoTaskRecord>().Where(r => r.Id == task.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new KeyNotFoundException("Task not found");
                }
                record.IsCompleted = true;
                await database.UpdateAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to complete task");
                throw;
            }
        }

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var records = await database.Table<ToDoTaskRecord>().ToListAsync();
                return records.Select(r => new ToDoTask { Id = r.Id, TaskName = r.TaskName, ToDoListId = r.ToDoListRecordId, IsCompleted = r.IsCompleted }).ToList();
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to get all tasks");
                throw;
            }
        }

        public async Task RemoveFromListAsync(ToDoTask task)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var record = await database.Table<ToDoTaskRecord>().Where(r => r.Id == task.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new KeyNotFoundException("Task not found");
                }
                await database.DeleteAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to remove task from list");
                throw;
            }
        }

        public async Task UpdateTaskAsync(ToDoTask task)
        {
            try
            {
                var database = _connectionFactory.CreateConnection();
                var record = await database.Table<ToDoTaskRecord>().Where(r => r.Id == task.Id).FirstOrDefaultAsync();
                if (record == null)
                {
                    throw new KeyNotFoundException("Task not found");
                }
                record.TaskName = task.TaskName;
                await database.UpdateAsync(record);
            }
            catch (SQLite.SQLiteException ex)
            {
                _logger.LogError(ex, "Failed to update task");
                throw;
            }
        }
    }
}
