using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoService _service;
        private readonly ILogger<MedicoController> _logger;

        public MedicoController(MedicoService service, ILogger<MedicoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Listando todos los medicos");
            return View(_service.ObtenerTodosMedicos());
        }

        public IActionResult Detalle(int id)
        {
            _logger.LogInformation("Buscando detalle del medico {MedicoId}", id);
            var medico = _service.ObtenerMedicoPorId(id);

            if (medico == null)
            {
                _logger.LogWarning("Medico {MedicoId} no encontrado", id);
                return NotFound();
            }

            return View(medico);
        }
    }
}
