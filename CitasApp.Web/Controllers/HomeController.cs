using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Accediendo a Home/Index");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Accediendo a Home/Privacy");
            return View();
        }
    }
}
