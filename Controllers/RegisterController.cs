using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] RegisterPackage registerPackage)
        {
            UserCredential uc = new UserCredential(registerPackage);
            if (uc.isAlreadyHaveTheUser())
                return BadRequest("There already has a same user");

            string token = TokenGenerator.GetToken();
            uc.token = token;

            return Ok(uc.userInfo);
        }
    }
}
