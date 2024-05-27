using API.Models;
using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Mvc;


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
        public ActionResult CreatePlayRecord([FromBody]CreatePlayRecordJson playRecord)
        {
            var dto = new PlayRecordDto()
            {
                UserId = playRecord.UserId,
                GameId = playRecord.GameId,
                CompletedFlag = playRecord.CompletedFlag,
                HoursPlayed = playRecord.HoursPlayed,
                Rating = playRecord.Rating,
                PlayDescription = playRecord.PlayDescription
            };

            _playRecordService.CreatePlayRecord(dto);
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