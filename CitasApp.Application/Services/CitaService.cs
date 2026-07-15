using CitasApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    /// <summary>
    /// Lógica de negocio relacionada con las citas médicas: consultarlas,
    /// validar a quién pertenecen y confirmarlas.
    /// </summary>
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        // Lista de observadores (por ejemplo: notificar por email o SMS) que se
        // avisan automáticamente cada vez que se confirma una cita.
        private readonly IEnumerable<ICitaObserver> _observers;

        public CitaService(ICitaRepository citaRepository,
                           IPacienteRepository pacienteRepository,
                           IMedicoRepository medicoRepository,
                           IEnumerable<ICitaObserver> observers)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
            _observers = observers;
        }

        /// <summary>Todas las citas registradas en el sistema.</summary>
        public List<Cita> ObtenerTodasCitas() => _citaRepository.ObtenerTodos();

        /// <summary>Citas asociadas a un paciente en particular.</summary>
        public List<Cita> ObtenerCitasPorPaciente(int pacienteId) => _citaRepository.ObtenerPorPaciente(pacienteId);

        /// <summary>Confirma que un paciente existe (o null si no).</summary>
        public Paciente? ValidarPaciente(int pacienteId) => _pacienteRepository.ObtenerPorId(pacienteId);

        /// <summary>Confirma que un médico existe (o null si no).</summary>
        public Medico? ValidarMedico(int medicoId) => _medicoRepository.ObtenerPorId(medicoId);

        /// <summary>
        /// Marca una cita como "Confirmada", guarda el cambio y notifica
        /// a todos los observadores registrados (email, SMS, etc.).
        /// </summary>
        /// <returns>true si la cita existía y se confirmó; false si no se encontró.</returns>
        public bool ConfirmarCita(int citaId)
        {
            var cita = _citaRepository.ObtenerTodos().FirstOrDefault(c => c.Id == citaId);
            if (cita == null) return false;

            cita.Estado = "Confirmada";
            _citaRepository.Actualizar(cita);

            foreach (var observer in _observers)
            {
                observer.Notificar(cita);
            }

            return true;
        }
    }
}
