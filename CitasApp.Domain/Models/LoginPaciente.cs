namespace CitasApp.Domain.Models
{
    /// <summary>Credencial de acceso de un paciente (email + contraseña hasheada).</summary>
    public class LoginPaciente
    {
        public int Id { get; set; }

        /// <summary>Id del paciente dueño de esta cuenta.</summary>
        public int PacienteId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
