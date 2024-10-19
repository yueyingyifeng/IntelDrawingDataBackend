using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTableController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTableController));
        [HttpPost]
        public IActionResult Post([FromBody] CreateTablePackage package)
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if (!uc.IsTokenCool())
                return new UnauthorizedResult();//401 Unauthorized

            UserInfo? userInfo = uc.userInfo;

            TableManager tm = new TableManager(userInfo,package.fileName);
            if(!tm.CreateTable(package.data)){
                log.Error($"save file fail, uid: {userInfo.id}");
                return BadRequest("Create table file failed");
            }
            long fileID = DBManager.CreateTable(userInfo.id, tm.getFilePath());
            log.Info($"uid:{userInfo.id} create chart: {fileID}");
            return StatusCode(201);
        }
    }
}
