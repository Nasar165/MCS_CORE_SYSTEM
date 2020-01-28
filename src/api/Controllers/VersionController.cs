using Components.Security;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        [AuthorizeRoles("Root")]
        public ActionResult Get()
            => Ok("API Version: 3.48.9");
    }
}