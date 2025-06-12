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


        public ActionResult ForMethod(string username, string password, string email, string name, string lastname, string birthday)
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