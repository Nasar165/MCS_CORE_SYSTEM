using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CondoController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, AdminAPI")]
        public ActionResult Post()
        {
            return Ok("Hello Admin");
        }
    }
}