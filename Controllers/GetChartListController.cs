using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntelDrawingDataBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetChartListController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            UserCredential uc = new UserCredential(Request.Headers.Authorization);
            if(!uc.IsTokenCool())
                return Unauthorized();
            var result = DBManager.GetChartListByUserID(uc.userInfo.id);
            if (result == null)
                return BadRequest("error in fetch the list");
            return Ok(result);
        }
    }
}
