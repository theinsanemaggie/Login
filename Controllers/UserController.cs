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
        public IActionResult Create() // formulario alta
        {
            return View(); // vista con el formulario
        }

        private readonly string connectionString = @"Server=DESKTOP-C4T982S\SQLSERVERMS2022;Database=LogInUser;User Id=Maggie;Password=tatakae;Trusted_Connection=True;TrustServerCertificate=True;";

        //lista copiada del profe -- corregida, no tocar
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
        }

        public ActionResult Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM userTable WHERE id = @id";
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
                    command.Parameters.AddWithValue("@password", password);
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

        //código del profe con modificaciones -- revisado, no tocar
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE userTable SET
                                username = @username,
                                password = @password,
                                email = @email,
                                name = @name,
                                lastname = @lastname,
                                birthday = @birthday
                                WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@password", user.password);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@name", user.name);
                command.Parameters.AddWithValue("@lastname", user.lastname);
                command.Parameters.AddWithValue("@birthday", user.birthday);
                command.Parameters.AddWithValue("@id", user.id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("List");
            }
        }

        public IActionResult Edit(int id)
        {
            UserModel user = new UserModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id, username, password, email, name, lastname, birthday FROM userTable WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user.id = reader.GetInt32(0);
                    user.username = reader.GetString(1);
                    user.password = reader.GetString(2);
                    user.email = reader.GetString(3);
                    user.name = reader.GetString(4);
                    user.lastname = reader.GetString(5);
                    user.birthday = Convert.ToDateTime(reader.GetString(6)); // ← corregido
                }

                return View(user);
            }
        }
    }
}
