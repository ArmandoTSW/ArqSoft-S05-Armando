using CitasApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    /// <summary>Muestra la agenda de citas. Requiere haber iniciado sesión.</summary>
    [Authorize]
    public class CitaController : Controller
    {
        private readonly CitaService _service;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly ILogger<CitaController> _logger;

        public CitaController(CitaService service,
                             PacienteService pacienteService,
                             MedicoService medicoService,
                             ILogger<CitaController> logger)
        {
            _service = service;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _logger = logger;
        }

        /// <summary>Lista todas las citas del sistema (vista global).</summary>
        public IActionResult Index()
        {
            _logger.LogInformation("Listando todas las citas");
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerTodasCitas());
        }

        /// <summary>Lista únicamente las citas de un paciente específico.</summary>
        public IActionResult PorPaciente(int pacienteId)
        {
            _logger.LogInformation("Listando citas del paciente {PacienteId}", pacienteId);
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerCitasPorPaciente(pacienteId));
        }
    }
}
