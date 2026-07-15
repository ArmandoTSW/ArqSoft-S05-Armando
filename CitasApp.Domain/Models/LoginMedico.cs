namespace CitasApp.Domain.Models
{
    /// <summary>Credencial de acceso de un médico (email + contraseña hasheada).</summary>
    public class LoginMedico
    {
        public int Id { get; set; }

        /// <summary>Id del médico dueño de esta cuenta.</summary>
        public int MedicoId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
