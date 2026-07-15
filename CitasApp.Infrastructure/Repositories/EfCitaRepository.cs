using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfCitaRepository : ICitaRepository
    {
        private readonly CitasAppDbContext _context;

        public EfCitaRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Cita> ObtenerTodos()
        {
            return _context.Citas.ToList();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _context.Citas.Where(c => c.PacienteId == pacienteId).ToList();
        }
    }
}