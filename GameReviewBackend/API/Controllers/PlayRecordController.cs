using API.Models;
using BusinessLogic.Abstractions;
using Components.Models;
using Mapster;
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

        [HttpPost]
        [RequiredScope("gamereview-user")]
        public ActionResult CreatePlayRecord([FromBody]CreatePlayRecordDto playRecord)
        {
            var userId = User.GetObjectId();
            _playRecordService.CreatePlayRecord(playRecord, userId);
            return Ok();
        }

        [HttpPut("{playRecordId}")]
        public ActionResult UpdatePlayRecord(int playRecordId, [FromBody]UpdatePlayRecordJson playRecord)
        {
            var dto = new PlayRecordDto()
            {
                Id = playRecordId,
                CompletedFlag = playRecord.CompletedFlag,
                HoursPlayed = playRecord.HoursPlayed,
                Rating = playRecord.Rating,
                PlayDescription = playRecord.PlayDescription
            };

            _playRecordService.UpdatePlayRecord(dto);
            return Ok();
        }
    }
}