using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public List<Paciente> ObtenerTodosPacientes() => _repository.ObtenerTodos();

        public Paciente? ObtenerPacientePorId(int id) => _repository.ObtenerPorId(id);
    }
}
