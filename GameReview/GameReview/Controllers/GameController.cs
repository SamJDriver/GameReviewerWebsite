using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private IGameService _gameService;
        private IIgdbApiService _igdbApiService;

        public GameController(IGameService gameService, IIgdbApiService igdbApiService)
        {
            _gameService = gameService;
            _igdbApiService = igdbApiService;
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

        [HttpGet("/igdb")]
        public async Task<IActionResult> GetAccessToken()
        {
            var accessToken = await _igdbApiService.QueryApi();
            return Ok();
        }

    }
}