using Microsoft.AspNetCore.Mvc;
using SoloCapstone.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SoloCapstone.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

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