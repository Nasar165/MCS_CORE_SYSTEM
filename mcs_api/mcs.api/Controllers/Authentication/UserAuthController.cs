using mcs.api.Security;
using mcs.api.Security.AuthTemplate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        [Authorize(Roles = "AdminAPI")]
        public ActionResult Get()
        {
            return Ok("Hello API");
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var auth = new AuthHelper();
                return Ok(auth.AuthenticateUser(userAccount));
            }
            return Unauthorized();
        }
    }
}