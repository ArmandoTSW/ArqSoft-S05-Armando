using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfPacienteRepository : IPacienteRepository
    {
        private readonly CitasAppDbContext _context;

        public EfPacienteRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _context.Pacientes.ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _context.Pacientes.FirstOrDefault(p => p.Id == id);
        }

        public Paciente agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return paciente;
        }
    }
}
