using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    /// <summary>Lógica de negocio para consultar pacientes.</summary>
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        /// <summary>Todos los pacientes registrados.</summary>
        public List<Paciente> ObtenerTodosPacientes() => _repository.ObtenerTodos();

        /// <summary>Un paciente por su Id, o null si no existe.</summary>
        public Paciente? ObtenerPacientePorId(int id) => _repository.ObtenerPorId(id);
    }
}
