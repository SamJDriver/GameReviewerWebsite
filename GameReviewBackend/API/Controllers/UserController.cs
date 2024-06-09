using BusinessLogic.Abstractions;
using Components.Models;
using GameReview.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            return Ok(newId) ;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {

            return Ok();
        }

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }


    }
}