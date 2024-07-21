using BusinessLogic.Abstractions;
using Components;
using Components.Exceptions;
using Components.Models;
using GameReview.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly GraphServiceClient _graphServiceClient;

        public UserController(IUserService userService, GraphServiceClient graphServiceClient)
        {
            _userService = userService;
            _graphServiceClient = graphServiceClient;
        }


        [HttpGet("me")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public async Task<IActionResult> GetMe()
        {

            var userId = User.GetObjectId();
            
            var result = await _graphServiceClient.Users[userId].GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = Components.Constants.MicrosoftGraph.GraphUserQueryParams;
            });

            if (result is null)
            {
                throw new DgcException("Could not authenticate user.", DgcExceptionType.Unauthorized);
            }

            var response = result.Adapt<UserDto>();
            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var result = await _graphServiceClient.Users.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = Components.Constants.MicrosoftGraph.GraphUserQueryParams;
            });

            if (result is null)
            {
                throw new DgcException("Could not authenticate user.", DgcExceptionType.Unauthorized);
            }

            var response = result.Value.Adapt<IEnumerable<UserDto>>();
            return Ok(response);
        }


    }
}