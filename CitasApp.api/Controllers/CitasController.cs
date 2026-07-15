using Microsoft.AspNetCore.Mvc;
using CitasApp.Application.Services;

namespace CitasApp.Api.Controllers
{
    /// <summary>Endpoints REST para consultar y confirmar citas médicas.</summary>
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

        /// <summary>Marca una cita como confirmada y dispara las notificaciones (email/SMS).</summary>
        [HttpPost("confirmar/{citaId}")]
        public IActionResult ConfirmarCita(int citaId)
        {
            var resultado = _citaService.ConfirmarCita(citaId);

            if (!resultado)
            {
                return NotFound(new { mensaje = $"No se encontró la cita con ID {citaId}" });
            }

            return Ok(new { mensaje = "Cita confirmada", id = citaId });
        }
    }
}
