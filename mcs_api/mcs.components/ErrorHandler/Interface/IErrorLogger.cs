using System;

namespace mcs.Components.Errorhandler.Interface
{
    public interface IErrorLogger
    {
        void LogError(Exception error);
    }
}