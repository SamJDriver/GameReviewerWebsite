using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly ILookupService _lookupService;

        public GameController(IGameService gameService, ILookupService lookupService)
        {
            _gameService = gameService;
            _lookupService = lookupService;
        }

        //Probably admin only
        [HttpPost]
        [Authorize]
        [RequiredScope("gamereview-admin")]
        public async Task<IActionResult> CreateGame([FromBody] GameDto gameJson)
        {
            var userId = User.GetObjectId();
            int newId = await _gameService.CreateGame(gameJson, userId);
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

        [HttpGet("friend/{pageIndex}/{pageSize}")]
        [Authorize]
        [RequiredScope("gamereview-user")]
        public async Task<IActionResult> GetGamesPopularWithFriends(int pageIndex, int pageSize)
        {
            var pagedGames = await _gameService.GetGamesPopularWithFriends(pageIndex, pageSize, User.GetObjectId());
            return Ok(pagedGames);
        }

        [HttpGet("search/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> SearchGames(string? searchTerm, int? genreId, int? releaseYear, int pageIndex, int pageSize)
        {
            var games = await _gameService.SearchGames(searchTerm, genreId, releaseYear, pageIndex, pageSize);
            return Ok(games);
        }

        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            var genres = _lookupService.GetGenreLookups();
            return Ok(genres);
        }
    }
}
