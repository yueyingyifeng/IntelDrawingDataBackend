using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RegisterController));

        [HttpPost]
        public IActionResult Post([FromBody] RegisterPackage registerPackage)
        {
            UserCredential uc = new UserCredential(registerPackage);
            if (uc.isAlreadyHaveTheUser())
            {
                log.Error("Register failed.");
                return BadRequest("this email is occupied");
            }

            uc.GenerateToken();
            log.Info($"New user. GenerateToken to uid:{uc.userInfo.id}. token: {uc.token[0..12]}");
            return StatusCode(201, uc);
        }
    }
}
