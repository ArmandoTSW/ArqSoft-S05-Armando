using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System;

namespace CitasApp.Infrastructure.Observers
{
    public class SmsObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            // Nota: Cambia 'Fecha' y 'Hora' por los nombres exactos que tengan en tu modelo Cita
            Console.WriteLine($"[SMS] Recordatorio enviado al paciente {cita.PacienteId} - cita el {cita.Fecha} a las {cita.Hora}");
        }
    }
}