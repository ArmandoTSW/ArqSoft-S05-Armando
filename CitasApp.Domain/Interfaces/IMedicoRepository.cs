using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>Contrato para acceder y guardar médicos, sin importar el almacenamiento real.</summary>
    public interface IMedicoRepository
    {
        /// <summary>Devuelve todos los médicos registrados.</summary>
        List<Medico> ObtenerTodos();

        /// <summary>Busca un médico por su Id, o null si no existe.</summary>
        Medico? ObtenerPorId(int id);

        /// <summary>Guarda un nuevo médico y devuelve la versión guardada (con su Id asignado).</summary>
        Medico Agregar(Medico medico);
    }
}
