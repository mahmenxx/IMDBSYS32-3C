using System.Diagnostics;
using IMDBSYS32_3C.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMDBSYS32_3C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Bsit3cdbContext bsit3CdbContext;

        public HomeController(ILogger<HomeController> logger, Bsit3cdbContext context)
        {
            _logger = logger;
            bsit3CdbContext = context;
        }

        public IActionResult Index()
        {
            var user = bsit3CdbContext.Users.FirstOrDefault();
            return View(user);
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
