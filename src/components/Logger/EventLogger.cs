using System;
using System.Threading.Tasks;
using Components.Database;
using Components.Database.Interface;
using Components.Logger.Interface;
using Components.Interface;
using System.IO;

namespace Components.Logger
{
    public class EventLogger : ILogger
    {
        private IFileWriter FileWriter { get; }
        private readonly string DirectoryPath =
            Directory.GetCurrentDirectory();
        private bool LogAsJson { get; }

        public EventLogger(bool logAsJson)
        {
            LogAsJson = logAsJson;
            FileWriter = new FileWriter();
        }

        private void IsErrorLogFilePathValid()
        {
            var DirecortyPath = $"{DirectoryPath}/logs/error/";
            Validation.ValidateLogFile(DirecortyPath, "error.txt");
        }

        private void IsEventLogFilePathValid()
        {
            var DirecortyPath = $"{DirectoryPath}/logs/event/";
            Validation.ValidateLogFile(DirecortyPath, "event.txt");
        }

        public async void LogEventAsync(string text)
            => await Task.Run(() => { LogEvent(text); });

        public void LogEvent(string text)
        {
            IsEventLogFilePathValid();
            FileWriter.AppendTextToFile(text, $"{DirectoryPath}/logs/event/event.txt", FileMode.Append);
        }

        public async void LogEventAsync(Exception error)
            => await Task.Run(() => { LogEvent(error); });

        public void LogEvent(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            IsErrorLogFilePathValid();
            FileWriter.AppendTextToFile(
                exceptionHelper.GetFormatedErrorMessage(LogAsJson), $"{DirectoryPath}/logs/error/error.txt", FileMode.Append);
        }

        // Potentially create a new class for database logging.
        public void LogAuthentication<T>(ISqlHelper sql, T data)
        {
            var sqlCommand = new SqlCommandHelper<T>(data, "name");
            var query = "Insert into authactivity (username, date) Values(@username, Now());";
            sql.AlterDataQuery<T>(query, sqlCommand);
        }

        public string GetTextFromLogFile(string fileName)
        {
            IsErrorLogFilePathValid();
            return FileWriter.ReadTextFromFile($"{DirectoryPath}/logs/{fileName}");
        }
    }
}
