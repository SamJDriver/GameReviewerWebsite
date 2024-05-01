using BusinessLogic.Infrastructure;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {

        public UserController()
        {
            
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            return Ok();
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }


    }
}