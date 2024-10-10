using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpDateChartController : ControllerBase
    {
        [HttpPatch]
        public IActionResult UpdateChart(UpdateChartPackage package)
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if(!uc.IsTokenCool() || uc.userInfo == null)
                return Unauthorized();

            var newPath = Tools.GenerateChartPath(uc.userInfo.id, package.fileName, package.fileType);

            string? oldFilePath = DBManager.GetFilePathByFileID(package.fileId);
            if (oldFilePath == null)
                return BadRequest("file doesn't exist");

            var tbm = new TableManager(uc.userInfo, $"{package.fileName}_{package.fileType}");
            tbm.DeleteTableFile(oldFilePath);
            tbm.CreateTable(package.data);
            DBManager.UpdateChartPathByFileID(package.fileId, newPath);

            return StatusCode(202);
        }
    }
}
