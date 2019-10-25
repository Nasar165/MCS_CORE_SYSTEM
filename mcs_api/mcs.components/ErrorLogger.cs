using System;
using mcs.components.Interface;

namespace mcs.components
{
    public class ErroLogger : IErrorLogger
    {
        IFileWriter _FileWriter { get; set; }
        public ErroLogger()
        {

        }

        public void LogError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}
