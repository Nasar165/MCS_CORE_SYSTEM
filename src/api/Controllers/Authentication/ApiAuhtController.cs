using api.Security.AuthTemplate;
using api.Security.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class ApiAuthController : ControllerBase
    {
        private IAuthHelper Auth { get; }
        public ApiAuthController(IAuthHelper auth)
            => Auth = auth;


        [HttpPost]
        public ActionResult Post([FromBody] AccessKey accessKey)
        {
            if (ModelState.IsValid)
            {
                var result = Auth.AuthentiacteAPI(accessKey);
                if (result is bool)
                    return Unauthorized();
                return Ok(result);
            }
            return Unauthorized();
        }
    }
}