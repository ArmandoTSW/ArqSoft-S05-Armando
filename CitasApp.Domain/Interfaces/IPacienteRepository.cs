using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>Contrato para acceder y guardar pacientes, sin importar el almacenamiento real.</summary>
    public interface IPacienteRepository
    {
        /// <summary>Devuelve todos los pacientes registrados.</summary>
        List<Paciente> ObtenerTodos();

        /// <summary>Busca un paciente por su Id, o null si no existe.</summary>
        Paciente? ObtenerPorId(int id);

        /// <summary>Guarda un nuevo paciente y devuelve la versión guardada (con su Id asignado).</summary>
        Paciente Agregar(Paciente paciente);
    }
}
