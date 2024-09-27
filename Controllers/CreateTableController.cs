using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTableController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CreateTablePackage package)
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);

            if(uc.IsTokenCool())
                return new UnauthorizedResult();//401 Unauthorized

            UserInfo? userInfo = uc.userInfo;

            TableManager tm = new TableManager(userInfo,package.fileName);
            if(!tm.CreateTable(package.data)){
                return BadRequest("Create table file failed");
            }

            return StatusCode(201);
        }
    }
}
