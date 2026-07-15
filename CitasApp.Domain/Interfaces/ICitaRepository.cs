using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>
    /// Contrato para acceder y modificar las citas médicas, sin importar
    /// si el almacenamiento real es un archivo JSON o una base de datos.
    /// </summary>
    public interface ICitaRepository
    {
        /// <summary>Devuelve todas las citas registradas.</summary>
        List<Cita> ObtenerTodos();

        /// <summary>Devuelve las citas asociadas a un paciente específico.</summary>
        List<Cita> ObtenerPorPaciente(int pacienteId);

        /// <summary>Guarda los cambios hechos sobre una cita ya existente (por ejemplo, su estado).</summary>
        void Actualizar(Cita cita);
    }
}
