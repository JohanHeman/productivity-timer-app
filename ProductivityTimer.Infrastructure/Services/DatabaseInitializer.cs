using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Data.Models;

namespace ProductivityTimer.Infrastructure.Services
{
    public class DatabaseInitializer
    {
        // Responsible for initializing database and creating tables as needed
        private readonly SQLIteConnectionFactory _connectionFactory;

        public DatabaseInitializer(SQLIteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            var connection = _connectionFactory.CreateConnection();
            await connection.CreateTableAsync<DailyHabitRecord>();
        }
    }
}
