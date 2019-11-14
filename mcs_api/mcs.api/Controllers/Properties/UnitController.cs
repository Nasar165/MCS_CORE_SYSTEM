using mcs.api.Database;
using mcs.api.Security;
using mcs.api.Security.Interface;
using mcs.domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[Controller]")]
    public class UnitController : ControllerBase
    {
        private IClaimHelper CreateClaimsHelper()
           => new ClaimsHelper(User.Claims);

        private PropertyHelper CreatePropertyHelper()
        {
            var SqlConnection = DatabaseHelper.Instance.GetClientDatabase(CreateClaimsHelper());
            return new PropertyHelper(SqlConnection);
        }

        [HttpPost]
        public ActionResult Create()
        {
            return Created("", "A unit was successfuly created");
        }

        [HttpPut]
        public ActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok();
        }

    }
}