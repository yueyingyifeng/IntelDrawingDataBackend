using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteTableController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DeleteTableController));

        [HttpDelete]
        public IActionResult DelTable(long fileID)
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);

            if (!uc.IsTokenCool())
                return new UnauthorizedResult();//401 Unauthorized
            UserInfo? userInfo = uc.userInfo;

            // TODO: 这不就可以瞎几把删了嘛 XD

            string? filePath = DBManager.GetFilePathByFileID(fileID);
            var manager = new TableManager(userInfo, filePath);

            if(!manager.DeleteTableFile(filePath) ||
                userInfo == null || filePath == null)
            {
                log.Error($"userInfo or filePath cannot be null on delete file");
                return BadRequest($"{fileID} cannot be found");
            }

            if (!DBManager.DeleteTable(fileID) || 
                userInfo == null || filePath == null)
            {
                log.Error($"userInfo or filePath cannot be null on delete DB record");
                return BadRequest($"{fileID} cannot be deleted");
            }

            log.Info($"uid:{uc.userInfo.id} delete a chart. path{filePath}");
            return StatusCode(204);
        }
    }
}
