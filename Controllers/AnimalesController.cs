using Microsoft.AspNetCore.Mvc;

namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
        //un action result para cada vista//COMENTARIO MAY
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create() // formulario alta
        {
            return View(); // vista con el formulario
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Especie()
        {
            return View();
        }
        public IActionResult Estado()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Raza()
        {
            return View();
        }
        public IActionResult Tamaño()
        {
            return View();
        }
    }
}
