using System.Runtime.CompilerServices;

namespace Logging
{
    public class Log
    {
        
        private const string _databaseName = "LogDatabase";
        private static ILogRecorder _logRecorder;

        public Log(RecorderType recorderType)
        {
            switch (recorderType)
            {
                case RecorderType.SQLLite:
                    _logRecorder = new SQLiteLogs(_databaseName);
                    return; 
                case RecorderType.PostGres:
                    _logRecorder = new PostGresLogs();
                    return;
            }
        }

        public static void Fatal(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
            _logRecorder.CreateLog(LogType.Fatal, filePath, function, message, payload);


        public static void Error(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
             _logRecorder.CreateLog(LogType.Error, filePath, function, message, payload);

        public static void Warn(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
             _logRecorder.CreateLog(LogType.Warn, filePath, function, message, payload);


        public static void Info(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
             _logRecorder.CreateLog(LogType.Info, filePath, function, message, payload);


        public static void Debug(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
             _logRecorder.CreateLog(LogType.Debug, filePath, function, message, payload);


        public static void Trace(string message, string payload = "", [CallerFilePath] string filePath = "", [CallerMemberName] string function = "") =>
             _logRecorder.CreateLog(LogType.Trace, filePath, function, message, payload);

        public static string GetClassName(string filePath)
        {
            int index = filePath.LastIndexOf("\\") + 1;
            return filePath.Substring(index, filePath.LastIndexOf(".") - index);
        }
    }

    public enum LogType
    {
        Fatal = 0,
        Error = 1,
        Warn = 2,
        Info = 3,
        Debug = 4,
        Trace = 5
    }
}
