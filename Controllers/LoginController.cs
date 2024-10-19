using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));

        [HttpPost]
        public ActionResult Post(LoginPackage login_info)
        {

            UserCredential uc = new UserCredential(login_info);
            if (!uc.isPass() && uc.userInfo == null)
            {
                log.Error("Login failed.");
                return BadRequest("ID and Password is not match");
            }

            uc.GenerateToken();
            log.Info($"uid:{uc.userInfo.id} login.");
            return Ok(uc);
        }
    }
}
