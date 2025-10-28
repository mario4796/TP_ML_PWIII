using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_ML_PWIII.Logica;
using TP_ML_PWIII.Data.Entidades;
using TP_ML_PWIII.Web.Models.ViewModels;

namespace TP_ML_PWIII.Web.Controllers
{
    public class UsuariosController : Controller
    {
      private readonly IUsuariosLogica _usuariosLogica;
        public UsuariosController(IUsuariosLogica usuariosLogica)
        {
            _usuariosLogica = usuariosLogica;
        }

        [HttpGet]
        public IActionResult Login()
        {

            if(HttpContext.Session.GetInt32("IdUsuario") != null)
            {
                return RedirectToAction("Explorar", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var usuario = _usuariosLogica.Login(model.Email, model.Password);
            if(usuario != null)
            {
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario ?? "Usuario");
                return RedirectToAction("Explorar", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Credenciales inválidas.");
                return View(model);
            }   

        }

        [HttpPost]
        public IActionResult Registro(RegistroViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(_usuariosLogica.ObtenerUsuarioPorEmail(model.Email) != null)
            {
                ModelState.AddModelError("Email", "El email ya está registrado.");
                return View(model);
            }

            bool registrado = _usuariosLogica.Registrar(model.Email, model.NombreUsuario, model.Password);
            if(registrado)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Error al registrar el usuario.");
                return View(model);
            }

            return RedirectToAction("Login");

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
