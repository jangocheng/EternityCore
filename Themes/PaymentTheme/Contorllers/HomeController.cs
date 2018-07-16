using Microsoft.AspNetCore.Mvc;

namespace PaymentTheme.Contorllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

