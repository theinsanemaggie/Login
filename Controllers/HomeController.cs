using System.Diagnostics;
using LoginMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginMVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Mensaje = "¡Hola otra vez!";
            return View();
        }

    }
}
