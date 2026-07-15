using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    /// <summary>Lógica de negocio para consultar médicos.</summary>
    public class MedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>Todos los médicos registrados.</summary>
        public List<Medico> ObtenerTodosMedicos() => _repository.ObtenerTodos();

        /// <summary>Un médico por su Id, o null si no existe.</summary>
        public Medico? ObtenerMedicoPorId(int id) => _repository.ObtenerPorId(id);
    }
}
