using api.Models;
using Components.Logger.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [HttpLogExport]
    [Route("[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private ILogger Logger {get;}
        public LogController(ILogger logger)
            => Logger = logger;

        public ActionResult GetErrorLog()
        {
            return Ok(Logger.GetTextFromLogFile("error/error.txt"));
        }
    }
}