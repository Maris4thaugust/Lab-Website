using Microsoft.AspNetCore.Mvc;

namespace WebSEnR.Controllers
{
    public class Activities : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
