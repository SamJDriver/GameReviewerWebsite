using GameReview.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Route("error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IWebHostEnvironment hostEnvironment){
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            //Gets the last exception
            var exceptionHandlerFeature = 
                HttpContext.Features.Get<IExceptionHandlerFeature>();

                return Problem(detail: exceptionHandlerFeature.Error.StackTrace,
                    title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() => Problem();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}