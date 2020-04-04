using Microsoft.AspNetCore.Mvc;
using xAuth;
using xAuth.Interface;

namespace api.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private IAuth Auth { get; }
        public ApiAuthController(TokenAuth auth)
            => Auth = auth;

        [HttpPost]
        public ActionResult Post([FromBody] TokenKey accessKey)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        var result = Auth.Authentiacte(accessKey, "token", "localhost", null);
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