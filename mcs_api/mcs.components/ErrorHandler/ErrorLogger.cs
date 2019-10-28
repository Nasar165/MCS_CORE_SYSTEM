using System;
using mcs.components.Errorhandler.Interface;
using mcs.components.Interface;

namespace mcs.components.Errorhandler
{
    public class ErroLogger : IErrorLogger
    {
        IFileWriter _FileWriter { get; set; }
        private string DirectoryPath { get; set; }

        // Begin Singelton 
        private static Lazy<ErroLogger> _Instance = new Lazy<ErroLogger>();
        public static ErroLogger Instance => _Instance != null ? _Instance.Value : new Lazy<ErroLogger>().Value;
        public void SetWorkingDirectory(string directory)
        {
            DirectoryPath = $"{directory}/Logs/Error";
        }

        public void LogError(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            throw new NotImplementedException();
        }

    }
}
