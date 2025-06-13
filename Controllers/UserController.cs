using System.Globalization;
using LoginMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LoginMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly string connectionString = @"Server=DESKTOP-C4T982S\SQLSERVERMS2022;Database=LogInUser;User Id=Maggie;Password=tatakae;Trusted_Connection=True;TrustServerCertificate=True;";

        //lista copiada del profe
        public ActionResult List()
        { 
            List<UserModel> user = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(connectionString)) // corregido
            {
                string query = "SELECT id, username, password, email, name, lastname, birthday from userTable";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.Add(new UserModel
                    {
                        id = reader.GetInt32(0),
                        username = reader.GetString(1),
                        password = reader.GetString(2),
                        email = reader.GetString(3),
                        name = reader.GetString(4),
                        lastname = reader.GetString(5),
                        birthday = Convert.ToDateTime(reader.GetString(6)),
                    });

                }
                return View(user);
            }
            //return View();
        }

        public ActionResult Eliminar(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "delete from userTable where id =  @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("List");
        }

        //código "for method" revisado y corregido, no tocar
        public ActionResult ForMethod(string username, string password, string email, string name, string lastname, DateTime birthday)
        {
            
            string connectionString = @"Server=DESKTOP-C4T982S\SQLSERVERMS2022;Database=LogInUser;User Id=Maggie;Password=tatakae;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO userTable (username, password, email, name, lastname, birthday) VALUES (@username, @password, @email, @name, @lastname, @birthday)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password); // ← corregido (estaba mal escrito como @passqword)
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@birthday", birthday);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ViewBag.Message = "Usuario insertado exitosamente";
                    connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al insertar el usuario " + username + ": " + ex.Message;
            }

            return View("Index");
        }
    }
}