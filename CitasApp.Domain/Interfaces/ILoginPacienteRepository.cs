using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ILoginPacienteRepository
    {
        LoginPaciente Agregar(LoginPaciente login);
        LoginPaciente? ObtenerPorEmail(string email);
    }
}
