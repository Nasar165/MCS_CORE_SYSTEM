using api.Security.AuthTemplate;
using api.Security.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        private IAuthHelper Auth { get; }
        public UserAuthController(IAuthHelper auth)
            => Auth = auth;

        [HttpPost]
        public IActionResult Post([FromBody] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                var result = Auth.AuthenticateUser(userAccount, userAccount.EncryptInfo);
                if (result is bool)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}