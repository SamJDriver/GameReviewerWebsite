using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/play-record/comment")]
    public class PlayRecordCommentController : Controller
    {
        private readonly IPlayRecordCommentService _playRecordCommentService;

        public PlayRecordCommentController(IPlayRecordCommentService playRecordCommentService)
        {
            _playRecordCommentService = playRecordCommentService;
        }

        [HttpPost]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult CreatePlayRecordComment([FromBody]CreatePlayRecordCommentDto playRecordComment)
        {
            _playRecordCommentService.CreatePlayRecordComment(playRecordComment, User.GetObjectId());
            return Ok();
        }

        [HttpPut("{playRecordCommentId}")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult UpdatePlayRecordComment(int playRecordCommentId, [FromBody]UpdatePlayRecordCommentDto playRecordComment)
        {
            _playRecordCommentService.UpdatePlayRecordComment(playRecordCommentId, playRecordComment, User.GetObjectId());
            return Ok();
        }

        [HttpPost("{playRecordCommentId}/upvote")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult Upvote(int playRecordCommentId)
        {
            _playRecordCommentService.Upvote(playRecordCommentId, User.GetObjectId());
            return Ok();
        }

        [HttpPost("{playRecordCommentId}/downvote")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult Downvote(int playRecordCommentId)
        {
            _playRecordCommentService.Downvote(playRecordCommentId, User.GetObjectId());
            return Ok();
        }

        [HttpDelete("{playRecordCommentId}")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public ActionResult DeletePlayRecordComment(int playRecordCommentId)
        {
            _playRecordCommentService.DeletePlayRecordComment(playRecordCommentId, User.GetObjectId());
            return Ok();
        }

    }
}
