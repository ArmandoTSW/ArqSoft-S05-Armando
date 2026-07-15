using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    /// <summary>Guarda y consulta citas usando Entity Framework / Postgres.</summary>
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

        /// <summary>
        /// Persiste los cambios hechos sobre una cita. Como EF Core ya rastrea
        /// la entidad desde que se consultó, basta con guardar los cambios.
        /// </summary>
        public void Actualizar(Cita cita)
        {
            _context.Citas.Update(cita);
            _context.SaveChanges();
        }
    }
}