using Microsoft.AspNetCore.Mvc;

namespace BookSalesProject.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}