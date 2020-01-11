using System;
using mcs.Components.DbConnection;
using mcs.Components.DbConnection.Interface;
using mcs.Components.Errorhandler.Interface;
using mcs.Components.Interface;

namespace mcs.Components.Errorhandler
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

        public void LogError(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            IsErrorLogFilePathValid();
            _FileWriter.AppendTextToFile(exceptionHelper.GetFormatedErrorMessage(), $"{DirectoryPath}error.txt");
        }


        public void LogAuthentication<T>(ISqlHelper sql, T data)
        {
            var sqlCommand = new SqlCommandHelper<T>(data, "name");
            var query = "Insert into authactivity (username, date) Values(@username, Now());";
            sql.AlterDataQuery<T>(query, sqlCommand);
        }

    }
}
