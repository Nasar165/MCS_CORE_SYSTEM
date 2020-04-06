using Microsoft.AspNetCore.Mvc;
using xAuth;
using api.Security.Interface;

namespace api.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        private IAuthHandler Auth { get; }
        public UserAuthController(IAuthHandler auth)
            => Auth = auth;

        [HttpPost]
        public IActionResult Post([FromBody] UserAccount userAccount)
        {

            if (ModelState.IsValid)
            {
                var result = Auth.UserAuthentication(userAccount);
                if (result is null)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();


        }

        [HttpPost]
        [Route("[controller]/RefreshToken")]
        public ActionResult RefreshToken([FromBody] RefreshToken token)
        {
            if (ModelState.IsValid)
            {
                var result = Auth.UserRefreshToken(token.Token);
                if (result is null)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}