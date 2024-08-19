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
    [Route("api/user-relationship")]
    public class UserRelationshipController : Controller
    {

        private readonly IUserRelationshipService _userRelationshipService;

        public UserRelationshipController(IUserRelationshipService userRelationshipService)
        {
            _userRelationshipService = userRelationshipService;
        }

        [Authorize]
        [RequiredScope("gamereview-user")]
        [HttpPut]
        public async Task<IActionResult> AddUserRelationship([FromBody] UserRelationship_Create_Dto userRelationshipCreateDto)
        {
            var requestorUserId = User.GetObjectId();
            await _userRelationshipService.CreateUserRelationship(requestorUserId, userRelationshipCreateDto.ChildUserId, userRelationshipCreateDto.UserRelationshipTypeLookupId);
            return Ok();
        }

        [Authorize]
        [RequiredScope("gamereview-user")]
        [HttpDelete("{userRelationshipId}")]
        public IActionResult DeleteUserRelationship(int userRelationshipId)
        {
            var requestorUserId = User.GetObjectId();
            _userRelationshipService.DeleteUserRelationship(userRelationshipId, requestorUserId);
            return Ok();
        }
    }
}
