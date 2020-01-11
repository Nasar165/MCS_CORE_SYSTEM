using Microsoft.AspNetCore.Mvc;
using mcs.Components.Security;
using mcs.api.Database;

namespace mcs.api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
            => Ok("API Version: 3.2.5");
        

        [AuthorizeRoles("Admin")]
        [HttpPost]
        public ActionResult Post()
        {
            DatabaseHelper.Instance.GetClientDatabase(User);
            return Ok("Role Corerct");
        }
    }
}