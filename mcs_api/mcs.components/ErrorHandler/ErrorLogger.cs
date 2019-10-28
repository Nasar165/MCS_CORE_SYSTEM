using System;
using mcs.components.Errorhandler.Interface;
using mcs.components.Interface;

namespace mcs.components.Errorhandler
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
            _FileWriter.AppendTextToFile("Hello World", $"{DirectoryPath}error.txt");
        }

    }
}
