using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoService _service;

        public MedicoController(MedicoService service)
        {
            _service = service;
        }

        public IActionResult Index() => View(_service.ObtenerTodosMedicos());

        public IActionResult Detalle(int id)
        {
            var medico = _service.ObtenerMedicoPorId(id);
            return medico == null ? NotFound() : View(medico);
        }
    }
}
