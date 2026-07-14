namespace CitasApp.Domain.Models
{
    public class LoginMedico
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public Medico? Medico { get; set; }
    }
}
