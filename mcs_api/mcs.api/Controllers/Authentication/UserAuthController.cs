using mcs.api.Security;
using mcs.api.Security.AuthTemplate;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var auth = new AuthHelper();
                userAccount.EncryptPassword();
                var result = auth.AuthenticateUser(userAccount);
                if (result is bool)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}