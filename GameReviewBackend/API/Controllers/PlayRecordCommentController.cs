using API.Models;
using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/review/comment")]
    public class PlayRecordCommentController : Controller
    {
        private readonly IPlayRecordCommentService _playRecordCommentService;

        public PlayRecordCommentController(IPlayRecordCommentService playRecordCommentService)
        {
            _playRecordCommentService = playRecordCommentService;
        }

        [HttpPost]
        public ActionResult CreatePlayRecordComment([FromBody]CreatePlayRecordCommentJson playRecordComment)
        {
            var dto = new PlayRecordCommentDto()
            {
                UserId = playRecordComment.UserId,
                PlayRecordId = playRecordComment.PlayRecordId,
                CommentText = playRecordComment.CommentText,
            };

            _playRecordCommentService.CreatePlayRecordComment(dto);
            return Ok();
        }

        [HttpPut("{playRecordCommentId}")]
        public ActionResult UpdatePlayRecordComment(int playRecordCommentId, [FromBody]UpdatePlayRecordCommentJson playRecordComment)
        {
            // TODO authorize that only the user who created the comment can update it
            var dto = new PlayRecordCommentDto()
            {
                Id = playRecordCommentId,
                CommentText = playRecordComment.CommentText,
            };

            _playRecordCommentService.UpdatePlayRecordComment(dto);
            return Ok();
        }
    }
}