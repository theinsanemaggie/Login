using LoginMVC.Models;
using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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
        public async Task<ActionResult> Create(string nombre, string idEspecie, string idTamaño, string idRaza, string edad, string idEstado, string fechaIngreso)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //saque imagen
                    string query = "INSERT INTO Animal(nombre, idEspecie, idTamaño, idRaza, edad, idEstado, fechaIngreso)" +
                        "VALUES (@nombre, @idEspecie, @idTamaño, @idRaza, @edad, @idEstado, @fechaIngreso) ";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@idEspecie", idEspecie);
                    command.Parameters.AddWithValue("@idTamaño", idTamaño);
                    command.Parameters.AddWithValue("@idRaza", idRaza);
                    command.Parameters.AddWithValue("@edad", edad);
                    command.Parameters.AddWithValue("@idEstado", idEstado);
                    command.Parameters.AddWithValue("@fechaIngreso", fechaIngreso);

                    // Convertir la imagen a byte[] si fue subida

                    /*
                     
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
                     */

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

        //------------------------------------------------------------------------------------------------------------------
        //public static List<AnimalEspecieModel> listaEspecies = new List<AnimalEspecieModel>();
        // GET para mostrar el formulario de agregar especie
        [HttpGet]
        public IActionResult Especie()
        {
            return View();
        }
        public IActionResult Especie(AnimalEspecieModel especie) //PRII 
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Especie (descripcion) VALUES (@descripcion)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descripcion", especie.descripcion);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return RedirectToAction("ListarEspecies");
            }

            return View(especie);
            /*
             
            if (ModelState.IsValid)
            {
                especie.idEspecie = listaEspecies.Count + 1;
                listaEspecies.Add(especie);

                return RedirectToAction("ListarEspecies");
            }

            // Si hay errores de validación, vuelve al formulario
            return View(especie);
             */
        }
        public IActionResult ListarEspecies()
        {
            List<AnimalEspecieModel> especies = new List<AnimalEspecieModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idEspecie, descripcion FROM Especie";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    especies.Add(new AnimalEspecieModel
                    {
                        idEspecie = Convert.ToInt32(reader["idEspecie"]),
                        descripcion = reader["descripcion"].ToString()
                    });
                }

                connection.Close();
            }
            return View(especies);
        }
        [HttpGet]
        [HttpGet]
        public IActionResult EditarEspecie(int id)
        {
            AnimalEspecieModel especie = new AnimalEspecieModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idEspecie, descripcion FROM Especie WHERE idEspecie = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    especie.idEspecie = Convert.ToInt32(reader["idEspecie"]);
                    especie.descripcion = reader["descripcion"].ToString();
                }
                connection.Close();
            }

            return View(especie);
        }


        /*

        var especie = listaEspecies.FirstOrDefault(e => e.idEspecie == id);
        if (especie == null)
        {
            return NotFound();
        }

        return View(especie); // Esto manda los datos al formulario
         */

        [HttpPost]
        [HttpPost]
        public IActionResult EditarEspecie(AnimalEspecieModel especie)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Especie SET descripcion = @descripcion WHERE idEspecie = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@descripcion", especie.descripcion);
                    command.Parameters.AddWithValue("@id", especie.idEspecie);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return RedirectToAction("ListarEspecies");
            }

            return View(especie);
        }

        public IActionResult EliminarEspecie(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Especie WHERE idEspecie = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            /*
            var especie = listaEspecies.FirstOrDefault(e => e.idEspecie == id);
            if (especie != null)
            {
                listaEspecies.Remove(especie);
            }
             */

            return RedirectToAction("ListarEspecies");
        }

        [HttpGet]
        //-----------------------------------------------------------------

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

                if (reader.Read())
                {
                    estado.idEstado = Convert.ToInt32(reader["idEstado"]);
                    estado.descripcion = reader["descripcion"].ToString();

                }
                ;
                connection.Close(); //agregado
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
        //código de eve-----------------------------------------------------------------------------+++++++++++++++++++++++++
        public IActionResult Tamaño()
        {

            List<AnimalTamañoModel> listaTamaños = new List<AnimalTamañoModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
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

        [HttpGet]
        public IActionResult AgregarTamaño()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarTamaño(AnimalTamañoModel nuevoTamaño)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Tamaño (descripcion) VALUES (@descripcion)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@descripcion", nuevoTamaño.descripcion);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Tamaño");
            }
            return View(nuevoTamaño);
        }

    }
}