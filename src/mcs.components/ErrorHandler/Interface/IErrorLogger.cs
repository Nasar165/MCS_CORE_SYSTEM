using System;
using mcs.Components.DbConnection.Interface;

namespace mcs.Components.Errorhandler.Interface
{
    public interface IErrorLogger
    {
        void LogError(Exception error);
        void SetWorkingDirectory(string directory);
        void LogAuthentication<T>(ISqlHelper sql, T data);
    }
}