using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadChartController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoadChartController));

        [HttpGet]
        public IActionResult Get(long fileID)
        {
            if(fileID == 0)
            {
                log.Error($"load chart argument {fileID} not properly");
                return BadRequest("bad argument");
            }

            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if (!uc.IsTokenCool())
                return Unauthorized();

            var filePath = DBManager.GetFilePathByFileID(fileID);
            if (filePath == null)
            {
                log.Error($"uid:{uc.userInfo.id} cannot load chart {fileID}, file doesn't exist");
                return BadRequest("file doesn't exist");
            }
                

            var csv = new CSVManager();
            try
            {
                csv.LoadFromFile(filePath);
            }
            catch(FileNotFoundException)
            {
                log.Fatal($"uid:{uc.userInfo.id} try to delete a unexit file. ");
                DBManager.DeleteTable(fileID);
                return BadRequest("file record corrupted");
            }
            var fileInfo = Tools.GetFileNameAndTypeFromPath(filePath);
            log.Info($"uid:{uc.userInfo.id} load a chart");
            return Ok(new
            {
                fileName = fileInfo.fileName,
                fileType = fileInfo.fileType,
                data = csv.GetAllData()
            });
        }
    }
}
