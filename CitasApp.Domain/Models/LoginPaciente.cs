namespace CitasApp.Domain.Models
{
    public class LoginPaciente
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public Paciente? Paciente { get; set; }
    }
}
