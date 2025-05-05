using LoginMVC.Models.BaseDeDatosFicticia;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace LoginMVC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // envía datos al servidor.
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var user = FakeUserDB.User.FirstOrDefault(u => u.User == username && u.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

                HttpContext.Session.SetString("User", user.User);

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("Cookies", principal); // 🔑 FIRMAR AL USUARIO

                return RedirectToAction("Index", "Home"); // ✅ Redirigir a página real del sistema
            }
            else
            {
                ViewBag.Error = "Credenciales Inválidas";
                return View("Index");
            }
        }


        public ActionResult Logout() 
                {
                    HttpContext.SignOutAsync("Cookies");
                    return RedirectToAction("Login");
                }
            }
    }
