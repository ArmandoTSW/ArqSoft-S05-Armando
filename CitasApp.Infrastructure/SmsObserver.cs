using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System;

namespace CitasApp.Infrastructure.Observers
{
    /// <summary>Notifica la confirmación de una cita simulando el envío de un SMS.</summary>
    public class SmsObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[SMS] Recordatorio enviado al paciente {cita.PacienteId} - cita el {cita.Fecha} a las {cita.Hora}");
        }
    }
}
