using System;
using mcs.components.Errorhandler.Interface;
using mcs.components.Interface;

namespace mcs.components.Errorhandler
{
    public class ErrorLogger : IErrorLogger
    {
        IFileWriter _FileWriter { get; set; }
        private string DirectoryPath { get; set; }

        // Begin Singelton 
        private static Lazy<ErrorLogger> _Instance = new Lazy<ErrorLogger>();
        public static ErrorLogger Instance => _Instance != null ? _Instance.Value : new Lazy<ErrorLogger>().Value;
        public void SetWorkingDirectory(string directory)
        {
            DirectoryPath = $"{directory}/Logs/Error/";
        }

        public void LogError(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            var test = DirectoryPath;
            throw new NotImplementedException();
        }

    }
}
