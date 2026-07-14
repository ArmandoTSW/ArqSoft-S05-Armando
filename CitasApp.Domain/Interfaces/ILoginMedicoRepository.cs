using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ILoginMedicoRepository
    {
        LoginMedico Agregar(LoginMedico login);
        LoginMedico? ObtenerPorEmail(string email);
    }
}
