using BusinessLogic.Abstractions;
using BusinessLogic.Infrastructure;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        //Probably admin only
        [HttpPost]
        public IActionResult CreateGame([FromBody] UserDto user)
        {
            return Ok();
        }


        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _gameService.GetGames();
            return Ok(games);
        }


    }
}