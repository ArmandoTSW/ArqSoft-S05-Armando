using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>
    /// Algo que quiere enterarse cuando una cita cambia de estado
    /// (patrón Observer). Cada implementación decide cómo avisar:
    /// por email, SMS, etc.
    /// </summary>
    public interface ICitaObserver
    {
        /// <summary>Se llama cada vez que una cita se confirma.</summary>
        void Notificar(Cita cita);
    }
}
