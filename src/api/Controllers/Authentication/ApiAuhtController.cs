using api.Security.Interface;
using Microsoft.AspNetCore.Mvc;
using xAuth;

namespace api.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private IAuthHandler Auth { get; }
        public ApiAuthController(IAuthHandler auth)
            => Auth = auth;

        [HttpPost]
        public ActionResult Post([FromBody] TokenKey accessKey)
        {
            if (ModelState.IsValid)
            {
                var result = Auth.TokenAuthentication(accessKey);
                if (result is null)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("RefreshToken")]
        public ActionResult RefreshToken([FromBody] RefreshToken token)
        {
            if (ModelState.IsValid)
            {
                var result = Auth.TokenRefreshToken(token.Token);
                if (result is null)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}