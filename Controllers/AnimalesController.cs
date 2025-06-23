using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LoginMVC.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly string connectionString = @"Server=DESKTOP-JO06JGK;Database=LoginUser;User Id=sa;Password=tiger;TrustServerCertificate=True;";
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

            List<AnimalModel> animales = new List<AnimalModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    /*para que no traiga el ID sino la descricón comento esta linea*/
                    /*string query = "SELECT idAnimal, nombre, idEspecie, idTamaño, imagen, idRaza, edad, idEstado, fechaIngreso FROM Animal";*/
                    string query = @"
                                    SELECT 
                                        a.idAnimal, 
                                        a.nombre, 
                                        e.descripcion AS especie,
                                        t.descripcion AS tamaño,
                                        r.descripcion AS raza,
                                        a.edad,
                                        es.descripcion AS estado,
                                        a.fechaIngreso
                                    FROM Animal a
                                    JOIN Especie e ON a.idEspecie = e.idEspecie
                                    JOIN Tamaño t ON a.idTamaño = t.idTamaño
                                    JOIN Raza r ON a.idRaza = r.idRaza
                                    JOIN Estado es ON a.idEstado = es.idEstado";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        /*AnimalModel animal = new AnimalModel --SE REEMPLAZA PARA QUE LEA LA DESCRIPCIÓN Y NO EL ID
                        {
                            idAnimal = Convert.ToInt32(reader["idAnimal"]),
                            nombre = reader["nombre"].ToString(),
                            idEspecie = Convert.ToInt32(reader["idEspecie"]),
                            idTamaño = Convert.ToInt32(reader["idTamaño"]),
                            imagen = reader["imagen"] == DBNull.Value ? null : (byte[])reader["imagen"],
                            idRaza = Convert.ToInt32(reader["idRaza"]),
                            edad = Convert.ToInt32(reader["edad"]),
                            idEstado = Convert.ToInt32(reader["idEstado"]),
                            fechaIngreso = Convert.ToDateTime(reader["fechaIngreso"])*/

                        AnimalModel animal = new AnimalModel
                        {
                            idAnimal = Convert.ToInt32(reader["idAnimal"]),
                            nombre = reader["nombre"].ToString(),
                            especie = reader["especie"].ToString(),
                            tamaño = reader["tamaño"].ToString(),
                            raza = reader["raza"].ToString(),
                            edad = Convert.ToInt32(reader["edad"]),
                            estado = reader["estado"].ToString(),
                            fechaIngreso = Convert.ToDateTime(reader["fechaIngreso"])
                        };

                        animales.Add(animal);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener los animales: " + ex.Message;
            }

            return View(animales);
        }
    }
}
        
