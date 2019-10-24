using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CondoController : ControllerBase
    {
        [Authorize]
        public ActionResult Get()
        {
            return Ok("Hello World");
        }

    }
}