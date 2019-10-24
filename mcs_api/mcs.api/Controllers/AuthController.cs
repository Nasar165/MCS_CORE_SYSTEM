using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mcs.api.Models;
using mcs.api.Security;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] AccessKey accessKey)
        {
            if (ModelState.IsValid)
            {
                var claim = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "Nasar"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var jwt = new JwtAuthenticator();

                return Ok(null);
            }
            return Unauthorized();
        }
    }
}