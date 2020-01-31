using System;
using System.Threading.Tasks;
using Components.DbConnection;
using Components.DbConnection.Interface;
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
            if (!Validation.DirecortyPathExists($"{DirectoryPath}logs/error/"))
                FileWriter.CreateDirectoryPath($"{DirectoryPath}/logs/error/");

            if (!Validation.FilePathExists($"{DirectoryPath}/logs/error/error.txt"))
                FileWriter.CreateFile($"{DirectoryPath}/logs/error/error.txt");
        }

        public async void LogEventAsync(string text)
        {
            await Task.Run(() => { LogEvent(text); });
        }

        public void LogEvent(string text)
        {
            FileWriter.AppendTextToFile(text, $"{DirectoryPath}/logs/error/error.txt");
        }

        public async void LogEventAsync(Exception error)
        {
            await Task.Run(() => { LogEvent(error); });
        }

        public void LogEvent(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            IsErrorLogFilePathValid();
            FileWriter.AppendTextToFile(
                exceptionHelper.GetFormatedErrorMessage(LogAsJson), $"{DirectoryPath}/logs/error/error.txt");
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
