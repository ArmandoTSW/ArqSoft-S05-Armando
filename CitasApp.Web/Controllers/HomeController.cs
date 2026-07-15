using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    /// <summary>Página de inicio (portada) y aviso de privacidad. Acceso público.</summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>Portada pública del sistema.</summary>
        public IActionResult Index()
        {
            _logger.LogInformation("Accediendo a Home/Index");
            return View();
        }

        /// <summary>Aviso de privacidad.</summary>
        public IActionResult Privacy()
        {
            _logger.LogInformation("Accediendo a Home/Privacy");
            return View();
        }
    }
}
