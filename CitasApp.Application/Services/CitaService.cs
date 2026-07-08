
using CitasApp.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        // 1. Agregamos la colección de observers de manera genérica
        private readonly IEnumerable<ICitaObserver> _observers;

        // 2. Modificamos el constructor para inyectar los observers
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

        public List<Cita> ObtenerTodasCitas() => _citaRepository.ObtenerTodos();

        public List<Cita> ObtenerCitasPorPaciente(int pacienteId) => _citaRepository.ObtenerPorPaciente(pacienteId);

        public Paciente? ValidarPaciente(int pacienteId) => _pacienteRepository.ObtenerPorId(pacienteId);

        public Medico? ValidarMedico(int medicoId) => _medicoRepository.ObtenerPorId(medicoId);

        // 3. NUEVO MÉTODO: Lógica para confirmar la cita y notificar
        public bool ConfirmarCita(int citaId)
        {
            // Buscamos la cita. Si tu ICitaRepository tiene un método "ObtenerPorId(citaId)", puedes usarlo.
            // Si no, la buscamos directamente filtrando en la lista de todas las citas:
            var cita = _citaRepository.ObtenerTodos().FirstOrDefault(c => c.Id == citaId);

            if (cita == null) return false;

            // Cambiamos el estado a Confirmada
            cita.Estado = "Confirmada";

            // Si tu repositorio tiene un método para guardar/actualizar los cambios en el JSON, llámalo aquí.
            // Por ejemplo: _citaRepository.Actualizar(cita);

            // 4. Disparamos la notificación a todos los observers que estén registrados
            foreach (var observer in _observers)
            {
                // Cambia lo que tengas en la línea 55 por esto:
                observer.Notificar(cita);
            }

            return true;
        }

        

    }
}