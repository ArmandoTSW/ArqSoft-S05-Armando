using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>Contrato para guardar y consultar las credenciales de acceso de los pacientes.</summary>
    public interface ILoginPacienteRepository
    {
        /// <summary>Guarda una nueva credencial de login.</summary>
        LoginPaciente Agregar(LoginPaciente login);

        /// <summary>Busca la credencial asociada a un email, o null si no existe.</summary>
        LoginPaciente? ObtenerPorEmail(string email);
    }
}
