using mcs.api.Database;
using mcs.api.Security;
using mcs.api.Security.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers.Properties
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CondoController : ControllerBase
    {
        private IClaimHelper CreateClaimsHelper()
            => new ClaimsHelper(User.Claims);

        //[Authorize(Roles = "Admin")]
        public IActionResult Get()
        {   // Get all Property no mather the website state
            var SqlConnection = DatabaseHelper.Instance.GetClientDatabase(CreateClaimsHelper());
            return Ok("Hello World");
        }

        /*[Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {   // Get Singel property
            throw new System.Exception("Not Ready Yet");
        }*/

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post()
        {
            return Ok("Hello Admin");
        }

        /// 
        //Low Level API front end Data
        ///
        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetCondos()
        {   // Get Property where website statet is set to true
            throw new System.Exception("Not Ready Yet");
        }

        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetCondo(int id)
        {   // Get single Property where website statet is set to true
            throw new System.Exception("Not Ready Yet");
        }


    }
}