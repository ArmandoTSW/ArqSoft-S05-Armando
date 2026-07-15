using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    /// <summary>Contrato para guardar y consultar las credenciales de acceso de los médicos.</summary>
    public interface ILoginMedicoRepository
    {
        /// <summary>Guarda una nueva credencial de login.</summary>
        LoginMedico Agregar(LoginMedico login);

        /// <summary>Busca la credencial asociada a un email, o null si no existe.</summary>
        LoginMedico? ObtenerPorEmail(string email);
    }
}
