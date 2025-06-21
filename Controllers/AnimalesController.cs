using Microsoft.AspNetCore.Mvc;

namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
