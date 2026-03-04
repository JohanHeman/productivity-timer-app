using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Data
{
    public class SQLIteConnectionFactory
    {
        public SQLiteAsyncConnection CreateConnection()
        {
            string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ProductivityTimer.db3");
            return new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }
}
