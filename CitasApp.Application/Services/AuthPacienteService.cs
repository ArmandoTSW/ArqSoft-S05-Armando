using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitasApp.Application.Services
{
    public class AuthPacienteService
    {
        private readonly IPacienteRepository _pacienteRepo;
        private readonly ILoginPacienteRepository _loginRepo;

        public AuthPacienteService(IPacienteRepository pacienteRepo, ILoginPacienteRepository loginRepo)
        {
            _pacienteRepo = pacienteRepo;
            _loginRepo = loginRepo;
        }

        public Paciente Registrar(string nombre, string apellido, string email, string telefono, string password)
        {
            if (_loginRepo.ObtenerPorEmail(email) is not null)
                throw new InvalidOperationException("Ese email ya está registrado.");

            var paciente = _pacienteRepo.agregar(new Paciente
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono
            });

            _loginRepo.Agregar(new LoginPaciente
            {
                PacienteId = paciente.Id,
                Email = email.ToLower(),
                PasswordHash = PasswordHasher.Hash(password)
            });

            return paciente;
        }

        public Paciente Login(string email, string password)
        {
            var login = _loginRepo.ObtenerPorEmail(email)
                ?? throw new UnauthorizedAccessException("Credenciales inválidas.");

            if (!PasswordHasher.Verificar(password, login.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            return _pacienteRepo.ObtenerPorId(login.PacienteId)
                ?? throw new InvalidOperationException("Paciente no encontrado.");
        }
      }
  }
