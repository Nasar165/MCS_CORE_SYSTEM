using System;
using Components.Database.Interface;

namespace Components.Logger.Interface
{
    public interface ILogger
    {
        void LogEvent(string text);
        void LogEvent(Exception error);
        void LogEventAsync(string text);
        void LogEventAsync(Exception error);
        void LogAuthentication<T>(ISqlHelper sql, T data);
        string GetTextFromLogFile(string fileName);
    }
}