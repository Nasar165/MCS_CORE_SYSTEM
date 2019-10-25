using System;

namespace mcs.components.Interface
{
    public interface IErrorLogger
    {
        void LogError(Exception error);
    }
}