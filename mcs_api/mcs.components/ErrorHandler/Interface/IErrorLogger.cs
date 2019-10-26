using System;

namespace mcs.components.Errorhandler.Interface
{
    public interface IErrorLogger
    {
        void LogError(Exception error);
    }
}