using api.Models;
using Microsoft.AspNetCore.Mvc;
using xEventLogger.Interface;

namespace api.Controllers
{
    [HttpLogExport]
    [Route("[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private IEventLogger Logger { get; }
        public LogController(IEventLogger logger)
            => Logger = logger;

        public ActionResult GetErrorLog()
            => Ok(Logger.GetLogFile("error.json"));

        public ActionResult GetEventLog()
            => Ok(Logger.GetLogFile("event.json"));
    }
}