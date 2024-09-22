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
                    return BadRequest("this email is occupied");

                uc.GenerateToken();
                return StatusCode(201, uc);
            }
        }
}
