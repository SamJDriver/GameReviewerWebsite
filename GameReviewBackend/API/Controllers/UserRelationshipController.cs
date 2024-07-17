using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/user-relationship")]
    public class UserRelationshipController : Controller
    {


        public UserRelationshipController()
        {
        }

        [HttpPost("friend")]
        public async Task<IActionResult> AddFriend()
        {
            return Ok();
        }

        [HttpPost("ignore")]
        public async Task<IActionResult> AddIgnore()
        {
            return Ok();
        }
    }
}