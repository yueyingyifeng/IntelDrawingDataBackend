using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTableController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] CreateTablePackage package)
        {
            string? token = Request.Headers.Authorization;

            if(token == null || token.Length < 16)
                return new UnauthorizedResult();//401 Unauthorized

            token = token.Substring(6);

            UserInfo userInfo = TokenGenerator.AnalysisToken(token);
            if(userInfo == null)
                return new UnauthorizedResult();//401 Unauthorized

            TableManager tm = new TableManager(userInfo,package.fileName);
            if(!tm.CreateTable(package.data)){
                return BadRequest("Create table file failed");
            }

            return StatusCode(201);
        }
    }
}
