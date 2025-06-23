using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly string connectionString = "Server=localhost;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;";

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

        //RAZA

        public IActionResult Raza()
        {
            //string connectionString = "Server=DESKTOP-TBEAQV2;Database=LogInUser;Trusted_Connection=True;TrustServerCertificate=True;";

            List<AnimalRazaModel> lista = new List<AnimalRazaModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT idRaza, descripcion FROM Raza";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new AnimalRazaModel
                    {
                        idRaza = (int)reader["idRaza"],
                        descripcion = reader["descripcion"].ToString()
                    });
                }
            }

            return View(lista);
        }

        // Agregar
        public IActionResult CrearRaza()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult CrearRaza(AnimalRazaModel raza)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Raza (descripcion) VALUES (@descripcion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@descripcion", raza.descripcion);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Raza");
        }

        // Editar
        public IActionResult EditarRaza(int id)
        {
            AnimalRazaModel raza = new AnimalRazaModel();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT idRaza, descripcion FROM Raza WHERE idRaza = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    raza.idRaza = (int)reader["idRaza"];
                    raza.descripcion = reader["descripcion"].ToString();
                }
            }

            return View(raza);
        }

      
        [HttpPost]
        public IActionResult EditarRaza(AnimalRazaModel raza)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Raza SET descripcion = @descripcion WHERE idRaza = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@descripcion", raza.descripcion);
                cmd.Parameters.AddWithValue("@id", raza.idRaza);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Raza");
        }

        // Eliminar
        public IActionResult EliminarRaza(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Raza WHERE idRaza = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Raza");
        }

        public IActionResult Tamaño()
        {
            return View();
        }
    }
}
