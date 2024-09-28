using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteTableController : ControllerBase
    {
        [HttpDelete]
        public IActionResult DelTable(long fileID)
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            Console.WriteLine(uc);

            if (!uc.IsTokenCool())
                return new UnauthorizedResult();//401 Unauthorized
            UserInfo? userInfo = uc.userInfo;

            // TODO: 这不就可以瞎几把删了嘛 XD

            string? filePath = DBManager.GetFilePathByFileID(fileID);
            var manager = new TableManager(userInfo, filePath);

            if(!manager.DeleteTableFile(filePath) ||
                userInfo == null || filePath == null)
                return BadRequest($"{fileID} cannot be found");

            if (!DBManager.DeleteTable(fileID) || 
                userInfo == null || filePath == null)
                return BadRequest($"{fileID} cannot be deleted");
            

            return StatusCode(204);
        }
    }
}
