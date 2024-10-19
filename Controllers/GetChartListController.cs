using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetChartListController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GetChartListController));

        [HttpGet]
        public IActionResult Get()
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if(!uc.IsTokenCool())
                return Unauthorized();
            var result = DBManager.GetChartListByUserID(uc.userInfo.id);
            if (result == null)
            {
                log.Error($"uid:{uc.userInfo.id} error on fetch the list");
                return BadRequest("error on fetch the list");
            }
            log.Info($"uid:{uc.userInfo.id} get chart list");
            return Ok(result);
        }
    }
}
