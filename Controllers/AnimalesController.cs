//<<<<<<< HEAD
﻿using LoginMVC.Models;
using LoginMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//=======
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
// origin/Rocio

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
        string connectionString = @"Server=DESKTOP-C4T982S\SQLSERVERMS2022;Database=LogInUser;User Id=Maggie;Password=tatakae;Trusted_Connection=True;TrustServerCertificate=True;";

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
                    connection.Close(); //agregado
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


        //Conexión a BDD Rochi
        //string connectionString = @"Server=RocioBalent;Database=LogInUser;Integrated Security=True;TrustServerCertificate=True;";
        public IActionResult Estado()
        {
            //AnimalEstadoModel es el nombre del modelo
            List<AnimalEstadoModel> estados = new List<AnimalEstadoModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idEstado, descripcion FROM Estado";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); //ejecuta el SELECT y guarda los resultados

                while (reader.Read())
                {
                    estados.Add(new AnimalEstadoModel
                    {
                        idEstado = Convert.ToInt32(reader["idEstado"]),
                        descripcion = reader["descripcion"].ToString(),
                    });

                }
                connection.Close(); //agregado

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
        public IActionResult AgregarEstado(AnimalEstadoModel nuevoEstado)
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
                    connection.Close(); //agregado
                }
                return RedirectToAction("Estado");
            }
            return View(nuevoEstado);

        }

        //Eliminar estado
        [HttpGet]
        public IActionResult EliminarEstado(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Estado WHERE idEstado = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close(); //agregado
            }
            return RedirectToAction("Estado");
        }

        //Editar estado
        [HttpGet]
        public IActionResult EditarEstado(int id)
        {
            AnimalEstadoModel estado = new AnimalEstadoModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select * FROM Estado WHERE idEstado = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(); // ejecuta el SELECT y guarda los resultados
                connection.Close(); //agregado

                if (reader.Read())
                {
                    estado.idEstado = Convert.ToInt32(reader["idEstado"]);
                    estado.descripcion = reader["descripcion"].ToString();

                }
                ;
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
                    connection.Close(); //agregado
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
        [HttpGet]
        //código de eve
        public IActionResult Tamaño()
        {
            return View();
        }
        public IActionResult EliminarTamaño(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Tamaño WHERE idTamaño = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                connection.Close(); //agregado
            }

            return RedirectToAction("Tamaño");
        }

        [HttpGet]
        public IActionResult EditarTamaño(int id)
        {
            AnimalTamañoModel tamaño = new AnimalTamañoModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Tamaño WHERE idTamaño = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    tamaño.idTamaño = Convert.ToInt32(reader["idTamaño"]);
                    tamaño.descripcion = reader["descripcion"].ToString();

                }
                connection.Close(); //agregado
            }

            return View(tamaño);
        }

        [HttpPost]
        public IActionResult EditarTamaño(AnimalTamañoModel tamañoEditado)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Tamaño SET descripcion = @descripcion WHERE idTamaño = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@descripcion", tamañoEditado.descripcion);
                    cmd.Parameters.AddWithValue("@id", tamañoEditado.idTamaño);
                    cmd.ExecuteNonQuery();
                    connection.Close(); //agregado
                }

                return RedirectToAction("Tamaño");
            }

            return View(tamañoEditado);
        }
    }
}