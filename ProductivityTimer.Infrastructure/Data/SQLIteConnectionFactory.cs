using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Data
{
    public class SQLIteConnectionFactory
    {
        private SQLiteAsyncConnection? _connection; // storing the connection if it exists 
        public SQLiteAsyncConnection CreateConnection()
        {
            if (_connection != null) // if connection exists return that one instead of creating a new one
                return _connection;

            // create the connection if it dont exist
            string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ProductivityTimer.db3");

            _connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            return _connection;
        }
    }
}
