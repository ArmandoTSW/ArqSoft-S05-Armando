using Microsoft.AspNetCore.Mvc;
using CitasApp.Application.Services; // Asegúrate de que esté este using si marca error en los servicios

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitasController(CitaService citaService,
                            PacienteService pacienteService,
                            MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_citaService.ObtenerTodasCitas());

        [HttpGet("porpaciente/{pacienteId}")]
        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = _citaService.ObtenerCitasPorPaciente(pacienteId);
            return citas.Count == 0 ? NotFound() : Ok(citas);
        }

        // =========================================================
        // NUEVO ENDPOINT: POST /api/citas/confirmar/{citaId}
        // =========================================================
        [HttpPost("confirmar/{citaId}")]
        public IActionResult ConfirmarCita(int citaId)
        {
            // Ejecuta la lógica del servicio que modifica el estado y dispara los Observers
            var resultado = _citaService.ConfirmarCita(citaId);

            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la cita con ID {citaId}" });
            }

            // Retorna un estatus 200 de éxito al cliente de Swagger
            return Ok(new { mensaje = "Cita confirmada", id = citaId });
        }
    }
}