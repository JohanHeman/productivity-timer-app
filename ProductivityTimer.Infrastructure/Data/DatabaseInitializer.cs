using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductivityTimer.Infrastructure.Data.DbModels;

namespace ProductivityTimer.Infrastructure.Data
{
    public class DatabaseInitializer
    {
        // Responsible for initializing database and creating tables as needed
        private readonly SQLiteConnectionFactory _connectionFactory;
        public DatabaseInitializer()
        {
            _connectionFactory = SQLiteConnectionFactory.GetConnectionFactory();
        }
        //Creates tables for the database
        public async Task InitializeAsync()
        {
            var connection = _connectionFactory.CreateConnection();
            await connection.CreateTableAsync<DailyHabitRecord>();
            await connection.CreateTableAsync<HabitCompletionRecord>();
            await connection.CreateTableAsync<WorkSessionRecord>();
        }
    }
}
