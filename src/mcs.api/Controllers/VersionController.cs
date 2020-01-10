using Microsoft.AspNetCore.Mvc;
using mcs.Components.Security;

namespace mcs.api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("API Version: 3.2.5");
        }

        [AuthorizeRoles("Admin")]
        [HttpPost]
        public ActionResult Post()
        {
            return Ok("Role Corerct");
        }
    }
}