using System;

namespace Logging
{ 
    public class PostGresLogs  : ILogRecorder
    {
        public void CreateLog(LogType logType, string classFilepath, string function, string message,
            string payload = "")
        {
        }

        public object Search(string query, params (string paramName, object value)[] parameters)
        {
            return null;
        }

        public void Delete(DateTime timeFrame, params LogType[] logType)
        {
        }

    } 
}