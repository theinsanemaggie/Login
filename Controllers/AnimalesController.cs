using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;
using LoginMVC.Models;

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


        //Conexión a BDD Rochi
        string connectionString = @"Server=RocioBalent;Database=LogInUser;Integrated Security=True;TrustServerCertificate=True;";
        public IActionResult Estado()
        {
            //AnimalEstadoModel es el nombre del modelo
            List <AnimalEstadoModel> estados = new List<AnimalEstadoModel> ();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idEstado, descripcion FROM Estado";
                SqlCommand command = new SqlCommand (query, connection);
                connection.Open ();
                SqlDataReader reader = command.ExecuteReader (); //ejecuta el SELECT y guarda los resultados

                while (reader.Read())
                {
                    estados.Add(new AnimalEstadoModel
                    {
                        idEstado = Convert.ToInt32 (reader["idEstado"]),
                        descripcion = reader["descripcion"].ToString(),
                    });

                }


            }
            return View(estados);
        }

        // Mostrar formulario para agregar estado
        [HttpGet]
        public IActionResult AgregarEstado()
        {
            return View();
        }

        //Recibir formulario
        [HttpPost]
        public IActionResult AgregarEstado (AnimalEstadoModel nuevoEstado)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Estado (descripcion) VALUES (@descripcion)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descripcion", nuevoEstado.descripcion);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return RedirectToAction("Estado");
            }
            return View(nuevoEstado); 

        }

        //Eliminar estado
        [HttpGet]
        public IActionResult EliminarEstado (int id)
        {
            using (SqlConnection connection = new SqlConnection (connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Estado WHERE idEstado = @id";
                SqlCommand command = new SqlCommand (query, connection);    
                command.Parameters.AddWithValue ("@id", id);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Estado");
        }

        //Editar estado
        [HttpGet]
        public IActionResult EditarEstado (int id)
        {
            AnimalEstadoModel estado = new AnimalEstadoModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select * FROM Estado WHERE idEstado = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(); // ejecuta el SELECT y guarda los resultados

                if (reader.Read())
                {
                    estado.idEstado = Convert.ToInt32(reader["idEstado"]);
                    estado.descripcion = reader["descripcion"].ToString();

                };
            }
            return View(estado);
        }

        // Procesar edición 
        [HttpPost]
        public IActionResult EditarEstado(AnimalEstadoModel estadoEditado)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Estado SET descripcion = @descripcion WHERE idEstado = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@descripcion", estadoEditado.descripcion);
                    cmd.Parameters.AddWithValue("@id", estadoEditado.idEstado);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Estado");
            }

            return View(estadoEditado);
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
