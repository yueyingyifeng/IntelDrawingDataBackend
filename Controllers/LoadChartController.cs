using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadChartController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(long fileID)
        {
            if(fileID == 0)
                return BadRequest("bad argument");

            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if (!uc.IsTokenCool())
                return Unauthorized();

            var filePath = DBManager.GetFilePathByFileID(fileID);
            if (filePath == null)
                return BadRequest("file doesn't exist");

            var csv = new CSVManager();
            try
            {
                csv.LoadFromFile(filePath);
            }
            catch(FileNotFoundException)
            {
                DBManager.DeleteTable(fileID);
                return BadRequest("file record corrupted");
            }
            var fileInfo = Tools.GetFileNameAndTypeFromPath(filePath);

            return Ok(new
            {
                fileName = fileInfo.fileName,
                fileType = fileInfo.fileType,
                data = csv.GetAllData()
            });
        }
    }
}
