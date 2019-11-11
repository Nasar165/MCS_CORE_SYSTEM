using mcs.api.Database;
using mcs.api.Security;
using mcs.api.Security.Interface;
using mcs.domain;
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

        private PropertyHelper CreatePropertyHelper()
        {
            var SqlConnection = DatabaseHelper.Instance.GetClientDatabase(CreateClaimsHelper());
            return new PropertyHelper(SqlConnection);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {   // Get all Property no mather the website state
            var propertyHelper = CreatePropertyHelper();
            return Ok(propertyHelper.GetCondos());
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
            var propertyHelper = CreatePropertyHelper();
            return Ok("Hello Admin");
        }

        /// 
        //Low Level API front end Data
        ///
        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetCondos()
        {
            var propertyHelper = CreatePropertyHelper();
            return Ok(propertyHelper.GetCondos(false));
        }

        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetCondo(int id)
        {   // Get single Property where website statet is set to true
            var propertyHelper = CreatePropertyHelper();
            throw new System.Exception("Not Ready Yet");
        }


    }
}