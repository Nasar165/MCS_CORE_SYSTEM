using System;
using System.Diagnostics;
using Components.Errorhandler.Interface;

namespace Components.Errorhandler
{
    public class ExceptionHelper : IExceptionHelper
    {
        private Exception _Error { get; set; }

        public ExceptionHelper(Exception error)
            => _Error = error;

        private StackFrame GetStackFrame(StackTrace stack, int index)
        {
            if (!Validation.ObjectIsNull(stack) && Validation.ValueIsGreateherThan(index, -1))
                return stack.GetFrame(index);
            else
                throw new Exception("Error:StackFrame validation failed: Class=ExceptionHelper Function=GetStackFrame Row=14");
        }

        private StackTrace GetStackTrace(bool NeedFileInfo = true)
            => new StackTrace(_Error, NeedFileInfo);

        private StackFrame GetErrorFrame()
        {
            var stackTrace = GetStackTrace();
            var StackFrame = GetStackFrame(stackTrace, 0);
            return StackFrame;
        }

        public int GetRowThatTrewException()
            => GetErrorFrame().GetFileLineNumber();

        public string GetMethodThatTrewException()
            => GetErrorFrame().GetMethod().Name;

        public string GetClassThatTrewException()
            => GetErrorFrame().GetMethod().ReflectedType.Name;
        public string GetMessage()
            => _Error.Message;

        public string GetFormatedErrorMessage()
        {
            var newError = string.Format("Date:{0}; ErrorClass:{1}; Method:{2}; Row:{3} Message:{4}",
             DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"), GetClassThatTrewException(),
             GetMethodThatTrewException(), GetRowThatTrewException(), GetMessage());
            return newError;
        }

        public IError GetFormatedErrorObject()
        {
            return new Error
            {
                E_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm tt"),
                E_Class = GetClassThatTrewException(),
                E_Method = GetMethodThatTrewException(),
                E_Message = GetMessage()
            };
        }
    }
}