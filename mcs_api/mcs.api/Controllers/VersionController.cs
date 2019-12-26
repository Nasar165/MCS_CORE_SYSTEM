using mcs.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace mcs.api.Controllers
{
    [Route("[Controller]")]
    public class VersionController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("API Version: 2.0.0");
        }

        [HttpPost]
        public IActionResult Post(){
            var encrypter = new AesEncrypter(AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","SymmetricKey"));
            var text = "itsme";
            var enc = encrypter.EncryptData(text);
            var response = new {
                Origianal = text,
                encrypted = enc,
                decrypted = encrypter.DecryptyData(enc)
            };
            return Ok(response);
        }
    }
}