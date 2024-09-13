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
            //Console.WriteLine(login_info.ToString());
            UserCredential uc = new UserCredential(login_info);
            if (!uc.isPass())
                return BadRequest("ID and Password is not match");

            string token = TokenGenerator.GetToken();
            uc.token = token;

            return Ok(uc.userInfo);
        }
    }
}
