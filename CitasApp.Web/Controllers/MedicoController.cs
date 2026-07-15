using CitasApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    /// <summary>Vista de administración de médicos. Solo médicos autenticados pueden entrar.</summary>
    [Authorize(Roles = "Medico")]
    public class MedicoController : Controller
    {
        private readonly MedicoService _service;
        private readonly ILogger<MedicoController> _logger;

        public MedicoController(MedicoService service, ILogger<MedicoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>Lista todos los médicos registrados.</summary>
        public IActionResult Index()
        {
            _logger.LogInformation("Listando todos los medicos");
            return View(_service.ObtenerTodosMedicos());
        }

        /// <summary>Muestra el detalle de un médico, o 404 si no existe.</summary>
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
