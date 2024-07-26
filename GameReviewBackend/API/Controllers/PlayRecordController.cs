using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;


namespace GameReview.Controllers
{
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
        [Authorize]
        [RequiredScope("gamereview-user")]
        public async Task<ActionResult> GetPlayRecords()
        {
            var userId = User.GetObjectId();
            var playRecords = await _playRecordService.GetSelfPlayRecords(userId);
            return Ok(playRecords);
        }

        [HttpGet("{playRecordId}")]
        public async Task<ActionResult> GetPlayRecordById(int playRecordId)
        {
            var userId = User.GetObjectId();
            var playRecord = await _playRecordService.GetPlayRecordById(playRecordId, userId);
            return Ok(playRecord);
        }

        [HttpPost]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult CreatePlayRecord([FromBody] CreatePlayRecordDto playRecord)
        {
            var userId = User.GetObjectId();
            _playRecordService.CreatePlayRecord(playRecord, userId);
            return Ok();
        }

        [HttpPut("{playRecordId}")]
        [Authorize]
        [RequiredScope("gamereview-user")]

        public ActionResult UpdatePlayRecord(int playRecordId, [FromBody] UpdatePlayRecordDto playRecord)
        {
            _playRecordService.UpdatePlayRecord(playRecordId, playRecord, User.GetObjectId());
            return Ok();
        }
    }
}