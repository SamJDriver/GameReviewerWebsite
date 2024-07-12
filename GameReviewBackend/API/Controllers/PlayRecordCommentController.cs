using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;


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
    }
}
