using System.Diagnostics;
using LoginLogoutDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginLogoutDemo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    // Only logged-in users can access this
    [Authorize]
    public IActionResult Dashboard()
    {
        return View();
    }

    // Only users with "Admin" role can access this
    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
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
