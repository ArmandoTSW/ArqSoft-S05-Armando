using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System;

namespace CitasApp.Infrastructure.Observers
{
    /// <summary>Notifica la confirmación de una cita simulando el envío de un correo.</summary>
    public class EmailObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[EMAIL] Confirmación enviada al paciente {cita.PacienteId} - motivo: {cita.Motivo} - estado: {cita.Estado}");
        }
    }
}
