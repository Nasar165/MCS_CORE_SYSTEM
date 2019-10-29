using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class CondoController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Get()
        {   // Get all Property no mather the website state
            return Ok("Hello World");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Get(int id)
        {   // Get Singel property
            throw new System.Exception("Not Ready Yet");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Post()
        {
            return Ok("Hello Admin");
        }

        /// 
        //Low Level API front end Data
        ///
        [Route("[action]")]
        //[Authorize(Roles = "API")]
        [AllowAnonymous]
        public ActionResult GetCondos()
        {   // Get Property where website statet is set to true
            throw new System.Exception("Not Ready Yet");
        }

        [Route("[action]")]
        [Authorize(Roles = "API")]
        public ActionResult GetCondo(int id)
        {   // Get single Property where website statet is set to true
            throw new System.Exception("Not Ready Yet");
        }


    }
}