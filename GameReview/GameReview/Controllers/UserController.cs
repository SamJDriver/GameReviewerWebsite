using BusinessLogic.Abstractions;
using Components.Models;
using GameReview.Models;
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
            string? error = default;

            UserDto userDto = new UserDto() 
            { 
                Username = userModel.Username,
                Email = userModel.Email,
                Password = userModel.Password
            };

            _userService.CreateUser(userDto, error);
            return string.IsNullOrEmpty(error) ? Ok() : BadRequest(error);
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }


    }
}