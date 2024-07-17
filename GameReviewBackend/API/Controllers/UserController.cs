using BusinessLogic.Abstractions;
using Components.Models;
using GameReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

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


        [HttpPost]
        public IActionResult CreateUser([FromBody] UserJson userModel)
        {
            UserDto userDto = new UserDto()
            {
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password
            };

            var newId = _userService.CreateUser(userDto);
            return Ok(newId);
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var result = await _graphServiceClient.Users.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "givenName", "postalCode", "identities" };
            });

            var response = result.Value.Select(u => new {u.Id, u.DisplayName, u.Photo});
            return Ok(response);
        }


    }
}