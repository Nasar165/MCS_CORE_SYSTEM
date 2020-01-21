using System;
using System.Threading.Tasks;
using Components.DbConnection;
using Components.DbConnection.Interface;
using Components.Errorhandler.Interface;
using Components.Interface;

namespace Components.Errorhandler
{
    public class ErrorLogger : IErrorLogger
    {
        IFileWriter _FileWriter { get; set; }
        private string DirectoryPath { get; set; }

        public ErrorLogger()
        {
            _FileWriter = new FileWriter();
        }

        // Begin Singelton 
        private static Lazy<ErrorLogger> _Instance = new Lazy<ErrorLogger>();
        public static ErrorLogger Instance => _Instance != null ? _Instance.Value : new Lazy<ErrorLogger>().Value;
        // End Singelton

        public void IsErrorLogFilePathValid()
        {
            if (!_FileWriter.DirecortyPathExists(DirectoryPath))
                _FileWriter.CreateDirectoryPath(DirectoryPath);

            if (!_FileWriter.FilePathExists($"{DirectoryPath}error.txt"))
                _FileWriter.CreateFile($"{DirectoryPath}error.txt");
        }

        public void SetWorkingDirectory(string directory)
        {
            DirectoryPath = $"{directory}/logs/error/";
            IsErrorLogFilePathValid();
        }

        public async void LogErrorAsync(Exception error)
        {
            var task = Task.Run(() => {
                var exceptionHelper = new ExceptionHelper(error);
                IsErrorLogFilePathValid();
                _FileWriter.AppendTextToFile(exceptionHelper.GetFormatedErrorMessage(), $"{DirectoryPath}error.txt");
            });
            await task;
        }


        public void LogAuthentication<T>(ISqlHelper sql, T data)
        {
            var sqlCommand = new SqlCommandHelper<T>(data, "name");
            var query = "Insert into authactivity (username, date) Values(@username, Now());";
            sql.AlterDataQuery<T>(query, sqlCommand);
        }

    }
}
