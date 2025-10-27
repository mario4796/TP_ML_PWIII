using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_ML_PWIII.Models;

namespace TP_ML_PWIII.Controllers
{
    public class HomeController : Controller
    {

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
            return View();
        }
    }
}
