using Microsoft.AspNetCore.Mvc;

namespace api
{
    
    public class CustomError
    {
        public string Error { get; }

        public CustomError(string message)
        {
            Error = message;
        }
    }

    public class ErrorRespons : JsonResult
    {
        public ErrorRespons(string message, int statusCode) : base(new CustomError(message))
        {
            StatusCode = statusCode;
        }
    }
}