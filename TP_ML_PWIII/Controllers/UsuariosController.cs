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
      private readonly IPasswordService _passwordService;

        public UsuariosController(IUsuariosLogica usuariosLogica, IPasswordService passwordService)
        {
            _usuariosLogica = usuariosLogica;
            _passwordService = passwordService;
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

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult EditarPerfil()
        {
            var id = HttpContext.Session.GetInt32("IdUsuario");
            if(id == null) return RedirectToAction("Login");

            var usuario = _usuariosLogica.ObtenerUsuarioPorId(id.Value);
            if(usuario == null) return RedirectToAction("Login");

            var model = new EditarPerfilViewModel
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario ?? string.Empty,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditarPerfil(EditarPerfilViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _usuariosLogica.ObtenerUsuarioPorId(model.IdUsuario);
            if (usuario == null) return RedirectToAction("Login");

            usuario.NombreUsuario = model.NombreUsuario;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                usuario.TokenAcceso = _passwordService.HashPassword(model.Password);
            }

            _usuariosLogica.modificarUsuario(usuario);

            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario ?? "Usuario");

            TempData["MensajeExito"] = "Perfil actualizado correctamente.";

            return RedirectToAction("Perfil", "Home");
        }

    }

}
