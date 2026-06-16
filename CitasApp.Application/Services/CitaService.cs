using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        public CitaService(ICitaRepository citaRepository,
                          IPacienteRepository pacienteRepository,
                          IMedicoRepository medicoRepository)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public List<Cita> ObtenerTodasCitas() => _citaRepository.ObtenerTodos();

        public List<Cita> ObtenerCitasPorPaciente(int pacienteId) => _citaRepository.ObtenerPorPaciente(pacienteId);

        public Paciente? ValidarPaciente(int pacienteId) => _pacienteRepository.ObtenerPorId(pacienteId);

        public Medico? ValidarMedico(int medicoId) => _medicoRepository.ObtenerPorId(medicoId);
    }
}
