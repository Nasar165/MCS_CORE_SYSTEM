using Microsoft.AspNetCore.Mvc;
using mcs.Components.Security;

namespace mcs.api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("API Version: 2.2.4");
        }

        [AuthorizeRoles("Admin")]
        [HttpPost]
        public ActionResult Post()
        {
            return Ok("Role Corerct");
        }
    }
}