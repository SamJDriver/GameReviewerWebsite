using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/lookup")]
    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            var genres = _lookupService.GetGenreLookups();
            return Ok(genres);
        }

        [HttpGet("release-years")]
        public IActionResult GetReleaseYears()
        {
            var releaseYears = _lookupService.GetReleaseYears();
            return Ok(releaseYears);
        }
    }
}
