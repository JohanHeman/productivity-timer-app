using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProductivityTimer.Infrastructure.Data.Models.DbModels
{
    public class ToDoListRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
