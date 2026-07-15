using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfMedicoRepository : IMedicoRepository
    {
        private readonly CitasAppDbContext _context;

        public EfMedicoRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public List<Medico> ObtenerTodos()
        {
            return _context.Medicos.ToList();
        }

        public Medico? ObtenerPorId(int id)
        {
            return _context.Medicos.FirstOrDefault(m => m.Id == id);
        }

        public Medico Agregar(Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
            return medico;
        }
    }
}