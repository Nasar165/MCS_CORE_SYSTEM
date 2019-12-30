using mcs.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("API Version: 2.2.3");
        }
    }
}