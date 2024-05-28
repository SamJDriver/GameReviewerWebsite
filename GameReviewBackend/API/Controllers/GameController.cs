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
        public async Task<IActionResult> GetAllGames(int pageIndex, int pageSize)
        {
            var pagedGames = await _gameService.GetAllGames(pageIndex, pageSize);
            return Ok(pagedGames);
        }

        [HttpGet("{gameId}")]
        public IActionResult GetGameById(int gameId)
        {
            var game = _gameService.GetGameById(gameId);
            return Ok(game);
        }
    }
}