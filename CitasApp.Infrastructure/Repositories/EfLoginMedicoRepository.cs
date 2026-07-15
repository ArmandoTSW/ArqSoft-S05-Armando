using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    /// <summary>Guarda y consulta credenciales de médico usando Entity Framework / Postgres.</summary>
    public class EfLoginMedicoRepository : ILoginMedicoRepository
    {
        private readonly CitasAppDbContext _context;

        public EfLoginMedicoRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public LoginMedico Agregar(LoginMedico login)
        {
            _context.LoginMedicos.Add(login);
            _context.SaveChanges();
            return login;
        }

        public LoginMedico? ObtenerPorEmail(string email)
        {
            return _context.LoginMedicos.FirstOrDefault(l => l.Email == email);
        }
    }
}
