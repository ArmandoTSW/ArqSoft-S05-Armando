using CitasApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Data
{
    /// <summary>Acceso a la base de datos Postgres: pacientes, médicos, citas y sus logins.</summary>
    public class CitasAppDbContext : DbContext
    {
        public CitasAppDbContext(DbContextOptions<CitasAppDbContext> options) : base(options) { }

        public DbSet<LoginPaciente> LoginPacientes { get; set; }
        public DbSet<LoginMedico> LoginMedicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }
}
