using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Security;

namespace CitasApp.Application.Services
{
    public class AuthMedicoService
    {
        private readonly IMedicoRepository _medicoRepo;
        private readonly ILoginMedicoRepository _loginRepo;

        public AuthMedicoService(IMedicoRepository medicoRepo, ILoginMedicoRepository loginRepo)
        {
            _medicoRepo = medicoRepo;
            _loginRepo = loginRepo;
        }

        public Medico Registrar(string nombre, string apellido, string especialidad, string numeroLicencia, string email, string password)
        {
            if (_loginRepo.ObtenerPorEmail(email) is not null)
                throw new InvalidOperationException("Ese email ya está registrado.");

            var medico = _medicoRepo.Agregar(new Medico
            {
                Nombre = nombre,
                Apellido = apellido,
                Especialidad = especialidad,
                NumeroLicencia = numeroLicencia
            });

            _loginRepo.Agregar(new LoginMedico
            {
                MedicoId = medico.Id,
                Email = email.ToLower(),
                PasswordHash = PasswordHasher.Hash(password)
            });

            return medico;
        }

        public Medico Login(string email, string password)
        {
            var login = _loginRepo.ObtenerPorEmail(email)
                ?? throw new UnauthorizedAccessException("Credenciales inválidas.");

            if (!PasswordHasher.Verificar(password, login.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            return _medicoRepo.ObtenerPorId(login.MedicoId)
                ?? throw new InvalidOperationException("Médico no encontrado.");
        }
    }
}