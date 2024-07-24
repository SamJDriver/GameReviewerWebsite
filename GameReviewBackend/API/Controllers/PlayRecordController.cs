using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;


namespace GameReview.Controllers
{
    [Authorize]
    [RequiredScope("gamereview-user")]
    [ApiController]
    [Route("api/play-record")]
    public class PlayRecordController : Controller
    {
        private readonly IPlayRecordService _playRecordService;

        public PlayRecordController(IPlayRecordService playRecordService)
        {
            _playRecordService = playRecordService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPlayRecords()
        {
            var userId = User.GetObjectId();
            _playRecordService.GetPlayRecords(userId);
            return Ok();
        }

        [HttpPost]
        public ActionResult CreatePlayRecord([FromBody]CreatePlayRecordDto playRecord)
        {
            var userId = User.GetObjectId();
            _playRecordService.CreatePlayRecord(playRecord, userId);
            return Ok();
        }

        [HttpPut("{playRecordId}")]

        public ActionResult UpdatePlayRecord(int playRecordId, [FromBody]UpdatePlayRecordDto playRecord)
        {
            _playRecordService.UpdatePlayRecord(playRecordId, playRecord, User.GetObjectId());
            return Ok();
        }
    }
}