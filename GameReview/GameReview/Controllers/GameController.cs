using BusinessLogic.Abstractions;
using BusinessLogic.Infrastructure;
using BusinessLogic.Models;
using Components.Models;
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
        public IActionResult CreateGame([FromBody] GameDto game)
        {
            string? error = default;
            int newId = _gameService.CreateGame(game, out error);
            return string.IsNullOrEmpty(error) ? Ok(newId) : BadRequest(error);
        }


        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _gameService.GetGames();
            return Ok(games);
        }


    }
}