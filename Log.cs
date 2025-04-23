using System.Data.SQLite;
using System.Runtime.CompilerServices;

namespace Logging
{
    public partial class Log
    {
        
        private const string _databaseName = "DiscordBotDatabase";
        private EmailSender _emailSender;
        private static ILogRecorder _logRecorder = new SQLiteLogs();

        public Log()
        {
            //_logRecorder = new SQLiteLogs(); 
            _logRecorder.StartUp();
            //_emailSender = new EmailSender();
            //_emailSender.SendEmail("test", "testifthisisworking", "dbotham16@outlook.com");
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

        private static string GetClassName(string filePath)
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
