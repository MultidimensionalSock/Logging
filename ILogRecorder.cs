using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    interface ILogRecorder
    {
        /// <summary>
        /// Called to run start up proceedures for the class handling logging. 
        /// </summary>
        public void StartUp();

        /// <summary>
        /// Adds the created log to storage
        /// </summary>
        public void CreateLog(LogType logType, string classFilepath, string function, string message, string payload = "");

        /// <summary>
        /// Search for logs
        /// </summary>
        public object Search(string query, params (string paramName, object value)[] parameters);

        /// <summary>
        /// delete log type/s within a set time frame
        /// </summary>
        public void Delete(DateTime timeFrame, params LogType[] logType);
    }
}
