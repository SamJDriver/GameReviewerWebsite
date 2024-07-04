using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;


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
        [Authorize]
        [RequiredScope("gamereview-admin")]
        public async Task<IActionResult> CreateGame([FromBody] GameDto gameJson)
        {
            int newId = await _gameService.CreateGame(gameJson);
            return Ok(newId);
        }


        [HttpGet("{pageIndex}/{pageSize}")]
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