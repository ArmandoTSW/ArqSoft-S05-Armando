using CitasApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Data
{
    public class CitasAppDbContext : DbContext
    {
        public CitasAppDbContext(DbContextOptions<CitasAppDbContext> options) : base(options) { }

        public DbSet<LoginPaciente> LoginPacientes { get; set; }
        public DbSet<LoginMedico> LoginMedicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
    }
}
