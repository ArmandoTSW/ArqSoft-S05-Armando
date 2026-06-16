using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class CitaController : Controller
    {
        private readonly CitaService _service;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitaController(CitaService service,
                             PacienteService pacienteService,
                             MedicoService medicoService)
        {
            _service = service;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerTodasCitas());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteService.ObtenerTodosPacientes();
            ViewBag.Medicos = _medicoService.ObtenerTodosMedicos();
            return View(_service.ObtenerCitasPorPaciente(pacienteId));
        }
    }
}
