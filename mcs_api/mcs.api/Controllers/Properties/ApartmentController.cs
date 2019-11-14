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
    public class ApartmentController : ControllerBase
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
            return Ok(propertyHelper.GetApartments());
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {
            var propertyHelper = CreatePropertyHelper();
            return Ok(propertyHelper.GetApartment(id));
        }

        /// 
        //Low Level API front end Data
        ///
        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetApartments()
        {
            var propertyHelper = CreatePropertyHelper();
            return Ok(propertyHelper.GetApartments(false));
        }

        [Route("[action]")]
        [Authorize(Roles = "API")]
        public IActionResult GetApartment(int id)
        {   // Get single Property where website statet is set to true
            var propertyHelper = CreatePropertyHelper();
            return Ok(propertyHelper.GetApartment(id, false));
        }


    }
}