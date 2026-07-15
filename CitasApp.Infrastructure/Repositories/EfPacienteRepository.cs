using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    /// <summary>Guarda y consulta pacientes usando Entity Framework / Postgres.</summary>
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

        public Paciente Agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return paciente;
        }
    }
}
