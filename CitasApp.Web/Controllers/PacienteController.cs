using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _service;

        public PacienteController(PacienteService service)
        {
            _service = service;
        }

        public IActionResult Index() => View(_service.ObtenerTodosPacientes());

        public IActionResult Detalle(int id)
        {
            var paciente = _service.ObtenerPacientePorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }
    }
}
