using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

            List<AnimalTamañoModel> listaTamaños = new List<AnimalTamañoModel>();

            using (SqlConnection conn = new SqlConnection("Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                string query = "SELECT * FROM Tamaño";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaTamaños.Add(new AnimalTamañoModel
                    {
                        idTamaño = Convert.ToInt32(reader["idTamaño"]),
                        descripcion = reader["descripcion"].ToString()
                    });
                }
            }

            return View(listaTamaños);
        }

        // Mostrar formulario para agregar tamaño
        [HttpGet]
        public IActionResult AgregarTamaño()
        {
            return View();
        }

        // Recibir datos del formulario y guardar tamaño
        [HttpPost]
        public IActionResult AgregarTamaño(AnimalTamañoModel nuevoTamaño)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection("Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;"))
                {
                    conn.Open();
                    string query = "INSERT INTO Tamaño (descripcion) VALUES (@descripcion)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@descripcion", nuevoTamaño.descripcion);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Tamaño");
            }
            return View(nuevoTamaño);


        }
        [HttpGet]
        public IActionResult EliminarTamaño(int id)
        {
            using (SqlConnection conn = new SqlConnection("Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                string query = "DELETE FROM Tamaño WHERE idTamaño = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Tamaño");
        }

        [HttpGet]
        public IActionResult EditarTamaño(int id)
        {
            AnimalTamañoModel tamaño = new AnimalTamañoModel();

            using (SqlConnection conn = new SqlConnection("Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                string query = "SELECT * FROM Tamaño WHERE idTamaño = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tamaño.idTamaño = Convert.ToInt32(reader["idTamaño"]);
                    tamaño.descripcion = reader["descripcion"].ToString();
                }
            }

            return View(tamaño);
        }

        [HttpPost]
        public IActionResult EditarTamaño(AnimalTamañoModel tamañoEditado)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection("Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;"))
                {
                    conn.Open();
                    string query = "UPDATE Tamaño SET descripcion = @descripcion WHERE idTamaño = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@descripcion", tamañoEditado.descripcion);
                    cmd.Parameters.AddWithValue("@id", tamañoEditado.idTamaño);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Tamaño");
            }

            return View(tamañoEditado);
        }

    }
}

