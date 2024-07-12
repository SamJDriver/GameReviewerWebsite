using BusinessLogic.Abstractions;
using Components.Models;
using Microsoft.AspNetCore.Mvc;


namespace GameReview.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompanyById(int companyId)
        {
            var game = _companyService.GetCompanyById(companyId);
            return Ok(game);
        }
    }
}