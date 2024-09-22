using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(LoginPackage login_info)
        {
            UserCredential uc = new UserCredential(login_info);
            if (!uc.isPass() && uc.userInfo == null)
                return BadRequest("ID and Password is not match");

            uc.GenerateToken();
            return Ok(uc);
        }
    }
}
