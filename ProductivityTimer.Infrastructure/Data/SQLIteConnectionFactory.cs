using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTimer.Infrastructure.Data
{
    public class SQLiteConnectionFactory
    {

        // Singleton design pattern class 
        private static readonly SQLiteConnectionFactory InstanceOfThisClass = new SQLiteConnectionFactory(); // creates an instance of this class 


        public static SQLiteConnectionFactory GetConnectionFactory()
        {
            return InstanceOfThisClass; // returns the instance to the class that needs it, and its allways the same instance that the other class uses (singleton)
        }

        private SQLiteConnectionFactory()
        {
            //private so we can prevent other instances from being created outside of this class 
        }


        private SQLiteAsyncConnection? _connection; 
        public SQLiteAsyncConnection CreateConnection()
        {
            if (_connection != null) // if connection exists return that one instead of creating a new one
                return _connection;

            // create the connection if it dont exist
            string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ProductivityTimer.db3");

            _connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create); // enables reading and writing to tables, creating table if it dosent exist 
            return _connection;
        }
    }
}
