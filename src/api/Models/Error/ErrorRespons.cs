using Microsoft.AspNetCore.Mvc;

namespace api.Models.Error
{
    public class ErrorRespons : JsonResult
    {
        public ErrorRespons(string message, int statusCode) : base(new CustomError(message))
        {
            StatusCode = statusCode;
        }
    }
}