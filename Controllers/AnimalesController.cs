using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly string connectionString = @"Server=DESKTOP-LVLM1TP;Database=LoginUser;User Id=sa;Password=1201;TrustServerCertificate=True;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(); // vista con el formulario de alta
        }

        public IActionResult Editar(AnimalModel Animal)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            {
                string query = @"UPDATE Animal SET
                                nombre = @nombre,
                                idEspecie = @idEspecie,
                                idTamaño = @idTamaño,
                                idRaza = @idRaza,   
                                edad = @edad,
                                idEstado = @idEstado,
                                fechaIngreso = @fechaIngreso
                                WHERE idAnimal = @idAnimal";    

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", Animal.nombre);
                command.Parameters.AddWithValue("@idEspecie", Animal.idEspecie);
                command.Parameters.AddWithValue("@idTamaño", Animal.idTamaño);
                command.Parameters.AddWithValue("@idRaza", Animal.idRaza);
                command.Parameters.AddWithValue("@edad", Animal.edad);
                command.Parameters.AddWithValue("@idEstado", Animal.idEstado);
                command.Parameters.AddWithValue("@fechaIngreso", Animal.fechaIngreso);

                connection.Open();
                command.ExecuteNonQuery();


                return RedirectToAction("Lista");
            }
        }

        //y este se encarga de ir a bucar los datos del usuario específico
        //Para mostrar los datos y permitirme en el front modificarlos

        public IActionResult Editar(int id)
        {
            AnimalModel Animal = new AnimalModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idAnimal, nombre, idEspecie, idTamaño, idRaza, edad, idEstado, fechaIngreso from Animal where id = " + id.ToString();

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Animal.idAnimal = Convert.ToInt32(reader["idAnimal"]);
                    Animal.nombre = reader["nombre"].ToString();
                    Animal.idEspecie = Convert.ToInt32(reader["idEspecie"]);
                    Animal.idTamaño = Convert.ToInt32(reader["idTamaño"]);
                    Animal.idRaza = Convert.ToInt32(reader["idRaza"]);
                    Animal.edad = Convert.ToInt32(reader["edad"]);
                    Animal.idEstado = Convert.ToInt32(reader["idEstado"]);
                    Animal.fechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);

                }

                return View(Animal);

            }
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