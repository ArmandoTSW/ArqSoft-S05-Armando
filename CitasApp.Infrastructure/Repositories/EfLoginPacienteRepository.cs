using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfLoginPacienteRepository : ILoginPacienteRepository
    {
        private readonly CitasAppDbContext _context;

        public EfLoginPacienteRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public LoginPaciente Agregar(LoginPaciente login)
        {
            _context.LoginPacientes.Add(login);
            _context.SaveChanges();
            return login;
        }

        public LoginPaciente? ObtenerPorEmail(string email)
        {
            return _context.LoginPacientes.FirstOrDefault(l => l.Email == email);
        }
    }
}
