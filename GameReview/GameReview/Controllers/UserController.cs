using BusinessLogic.Models;
using GameReview.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


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