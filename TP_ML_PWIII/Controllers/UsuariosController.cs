using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP_ML_PWIII.Logica;
using TP_ML_PWIII.Data.Entidades;

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
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_usuariosLogica.validarCredenciales(email, password))
            {
                var usuario = _usuariosLogica.ObtenerUsuarioPorEmail(email);
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetString("Email", usuario.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Registro(string email, string password, string confirmarPassword)
        {
            if (!_usuariosLogica.passwordIguales(password, confirmarPassword))
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            bool exito = _usuariosLogica.Registrar(email, password);

            if (!exito)
            {
                ViewBag.Error = "El email ya está registrado";
                return View();
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
