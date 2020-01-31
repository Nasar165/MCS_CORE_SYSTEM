using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
            => Ok("API Version: 3.50.1");
    }
}