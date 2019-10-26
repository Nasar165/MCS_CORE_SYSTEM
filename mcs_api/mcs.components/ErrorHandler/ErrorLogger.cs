using System;
using mcs.components.Errorhandler.Interface;
using mcs.components.Interface;

namespace mcs.components.Errorhandler
{
    public class ErroLogger : IErrorLogger
    {
        IFileWriter _FileWriter { get; set; }

        public ErroLogger()
        {

        }

        public void LogError(Exception error)
        {
            var exceptionHelper = new ExceptionHelper(error);
            throw new NotImplementedException();
        }
    }
}
