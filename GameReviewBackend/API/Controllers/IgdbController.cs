using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("igdb")]
    public class IgdbController : Controller
    {
        private readonly ILogger<IgdbController> _logger;
        private readonly IIgdbApiService _igdbApiService;

        public IgdbController(ILogger<IgdbController> logger, IIgdbApiService igdbApiService)
        {
            _igdbApiService = igdbApiService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAllRecords()
        {
            await _igdbApiService.QueryApi();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetOneGame()
        {
            string gameJson = await _igdbApiService.GetOneGame();
            return Ok(gameJson);
        }
    }
}