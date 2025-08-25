using Microsoft.AspNetCore.Mvc;

namespace NumberToWordsApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Convert");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Demo()
        {
            return View();
        }
    }
}
