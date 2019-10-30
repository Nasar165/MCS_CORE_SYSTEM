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
                var auth = new AuthHelper();
                var result = auth.AuthentiacteAPI(accessKey);
                if (result is bool)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}