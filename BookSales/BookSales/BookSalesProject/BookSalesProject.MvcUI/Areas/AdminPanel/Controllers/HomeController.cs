using BookSalesProject.MvcUI.Areas.AdminPanel.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookSalesProject.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class HomeController : Controller
    {
        [LogAspect]
        public IActionResult Index()
        {
            return View();
        }


    }
}
