using Microsoft.AspNetCore.Mvc;
using xAuth;
using xAuth.Interface;

namespace api.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        private IAuth Auth { get; }
        public UserAuthController(UserAuth auth)
            => Auth = auth;

        [HttpPost]
        public IActionResult Post([FromBody] UserAccount userAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = Auth.Authentiacte(userAccount, "user", "localhost", null);
                    return Ok(result);
                }
                return Unauthorized();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("[controller]/RefreshToken")]
        public ActionResult RefreshToken([FromBody] RefreshToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        var result = Auth.RefreshToken(token.Token, "token", "localhost", null);
                        return Ok(result);
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}