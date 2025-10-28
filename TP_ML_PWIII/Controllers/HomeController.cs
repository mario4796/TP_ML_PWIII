using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP_ML_PWIII.Logica;
using TP_ML_PWIII.Models;
using TP_ML_PWIII.Web.Models.ViewModels;

namespace TP_ML_PWIII.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUsuariosLogica _usuariosLogica;

        public HomeController(IUsuariosLogica usuariosLogica)
        {
            _usuariosLogica = usuariosLogica;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Acerca()
        {
            return View();
        }

        public IActionResult Explorar()
        {
            return View();
        }

        public IActionResult Perfil()
        {

            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var usuario = _usuariosLogica.ObtenerUsuarioPorId(idUsuario.Value);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            var viewModel = new PerfilViewModel
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario ?? "Usuario",
                Email = usuario.Email ?? "",
                FechaRegistro = usuario.FechaRegistro ?? DateTime.Now,
        
            };

            ViewBag.Mensaje = TempData["MensajeExito"];

            return View(viewModel);
        }
    }
}
