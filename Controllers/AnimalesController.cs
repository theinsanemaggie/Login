using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace LoginMVC.Controllers
{
    [Authorize]
    public class AnimalesController : Controller
    {
        //un action result para cada vista
        public IActionResult Index()
        {
            return View();
        }
        string connectionString = "Server=CARLA;Database=LogInUser;User Id=sa;Password=1612;Trusted_Connection=True;TrustServerCertificate=True;";

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(string nombre, string idEspecie, string idTamaño, IFormFile imagen, string idRaza, string edad, string idEstado, string fechaIngreso)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Animal(nombre, idEspecie, idTamaño, imagen, idRaza, edad, idEstado, fechaIngreso)" +
                        "VALUES (@nombre, @idEspecie, @idTamaño, @imagen, @idRaza, @edad, @idEstado, @fechaIngreso) ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@idEspecie", idEspecie);
                    command.Parameters.AddWithValue("@idTamaño", idTamaño);                    
                    command.Parameters.AddWithValue("@idRaza", idRaza);
                    command.Parameters.AddWithValue("@edad", edad);
                    command.Parameters.AddWithValue("@idEstado", idEstado);
                    command.Parameters.AddWithValue("@fechaIngreso", fechaIngreso);

                    // Convertir la imagen a byte[] si fue subida
                    if (imagen != null && imagen.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await imagen.CopyToAsync(ms);
                            byte[] imagenBytes = ms.ToArray();
                            command.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = imagenBytes;
                        }
                    }
                    else
                    {
                        command.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = DBNull.Value;
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ViewBag.Mensaje = "Animal Creado Exitosamente!";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error - Animal no creado";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al insertar el animal " + nombre + ": " + ex.Message;
            }



            return View();
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
