using API.Models;
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
        public IActionResult CreateGame([FromBody] CreateGameJson gameJson)
        {
            //TODO implement file blob
            GameDto gameDto = new GameDto()
            {
                Title = gameJson.Title,
                ReleaseDate = gameJson.ReleaseDate,
                ImageFilePath = "PLACEHOLDER",
                Description = gameJson.Description
            };

            int newId = _gameService.CreateGame(gameDto);
            return Ok(newId);
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