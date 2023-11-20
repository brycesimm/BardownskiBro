using BardownskiBro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BardownskiBro.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;

        public StatsController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.NHLAPIURL = _configuration.GetValue<string>("NHLAPI:BaseURL");
            return View();
        }

        public IActionResult TeamStats(string TeamName)
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
