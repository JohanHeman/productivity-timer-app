using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace ProductivityTimer.Infrastructure.Data.Models
{
    public class ToDoTaskRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int ToDoListRecordId { get; set; }
    }
}
