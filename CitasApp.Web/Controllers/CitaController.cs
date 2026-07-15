using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
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

        public IActionResult Index()
        {
            _logger.LogInformation("Listando todas las citas");
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerTodasCitas());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            _logger.LogInformation("Listando citas del paciente {PacienteId}", pacienteId);
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerCitasPorPaciente(pacienteId));
        }
    }
}
