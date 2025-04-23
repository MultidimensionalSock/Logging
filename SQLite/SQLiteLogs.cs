using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public partial class Log
    {
        public class SQLiteLogs : ILogRecorder
        {
            private static SQLiteConnection _connection;
            /// <summary>
            /// Called to run start up proceedures for the class handling logging. 
            /// </summary>
            public void StartUp() 
            {
                _connection = ConnectToDatabase("BotDatabase");
                CreateLogTable();
            }

            private SQLiteConnection ConnectToDatabase(string databaseName)
            {
                SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName}");

                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection Successful");
                    return connection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return null;
            }

            private void CreateLogTable()
            {
                string LogTable = @"
                    CREATE TABLE IF NOT EXISTS Logs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Level INTEGER NOT NULL,
                    Class TEXT NOT NULL, 
                    Function TEXT NOT NULL,
                    Message TEXT NOT NULL, 
                    Payload TEXT, 
                    Time TEXT NOT NULL
                );";

                try
                {
                    SQLiteCommand command = new SQLiteCommand(LogTable, _connection);
                    command.ExecuteNonQuery();
                    Log.Debug("Database Created");
                    Console.WriteLine("Database table created");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }

            /// <summary>
            /// Adds the created log to storage
            /// </summary>
            public void CreateLog(LogType logType, string classFilepath, string function, string message, string payload = "") 
            {
                string query = @"
                    INSERT INTO Logs (Level, Class, Function, Message, Payload, Time)
                    VALUES (@Level, @Class, @Function, @Message, @Payload, datetime());";

                using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Level", (int)logType);
                    command.Parameters.AddWithValue("@Class", GetClassName(classFilepath));
                    command.Parameters.AddWithValue("@Function", function);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Payload", payload);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            /// <summary>
            /// Search for logs
            /// </summary>
            public object Search(string query, params (string paramName, object value)[] parameters) 
            {
                SQLiteDataReader reader = null;
                using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.paramName, param.value);
                        }
                    }
                    try
                    {
                        reader = command.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
                    }
                }
                return reader;
            }

            /// <summary>
            /// delete log type/s within a set time frame
            /// </summary>
            public void Delete(DateTime timeFrame, params LogType[] logType) { }
        }
    }
}
