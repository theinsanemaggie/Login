using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
       
        //un action result para cada vista
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
        //-----------------------------------------------------------------
        public static List<AnimalEspecieModel> listaEspecies = new List<AnimalEspecieModel>();
        public IActionResult Especie(AnimalEspecieModel especie) //PRII 
        {
            if (ModelState.IsValid)
            {
                especie.idEspecie = listaEspecies.Count + 1;
                listaEspecies.Add(especie);

                return RedirectToAction("ListarEspecies");
            }

            // Si hay errores de validación, vuelve al formulario
            return View(especie);
        }
        public IActionResult ListarEspecies()
        {
            return View(listaEspecies);
        }
        [HttpGet]
        public IActionResult EditarEspecie(int id)
        {
            var especie = listaEspecies.FirstOrDefault(e => e.idEspecie == id);
            if (especie == null)
            {
                return NotFound();
            }

            return View(especie); // Esto manda los datos al formulario
        }

        [HttpPost]
        public IActionResult EditarEspecie(AnimalEspecieModel especie)
        {
            var existente = listaEspecies.FirstOrDefault(e => e.idEspecie == especie.idEspecie);
            if (existente != null)
            {
                existente.descripcion = especie.descripcion;
                return RedirectToAction("ListarEspecies");
            }

            return View(especie);
        }
        public IActionResult EliminarEspecie(int id)
        {
            var especie = listaEspecies.FirstOrDefault(e => e.idEspecie == id);
            if (especie != null)
            {
                listaEspecies.Remove(especie);
            }

            return RedirectToAction("ListarEspecies");
        }
        //-----------------------------------------------------------------
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
