using mcs.api.Security;
using mcs.api.Security.AuthTemplate;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] AccessKey accessKey)
        {
            if (ModelState.IsValid)
            {
                var authHelper = new AuthHelper();
                return Ok(authHelper.AuthentiacteAPI(accessKey));
            }
            return Unauthorized();
        }
    }
}