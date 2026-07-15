using CitasApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    [Authorize(Roles = "Medico")]
    public class PacienteController : Controller
    {
        private readonly PacienteService _service;
        private readonly ILogger<PacienteController> _logger;

        public PacienteController(PacienteService service, ILogger<PacienteController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Listando todos los pacientes");
            return View(_service.ObtenerTodosPacientes());
        }

        public IActionResult Detalle(int id)
        {
            _logger.LogInformation("Buscando detalle del paciente {PacienteId}", id);
            var paciente = _service.ObtenerPacientePorId(id);

            if (paciente == null)
            {
                _logger.LogWarning("Paciente {PacienteId} no encontrado", id);
                return NotFound();
            }

            return View(paciente);
        }
    }
}
