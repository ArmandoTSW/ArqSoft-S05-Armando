namespace CitasApp.Domain.Models
{
    /// <summary>Datos de un médico registrado en el sistema.</summary>
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string NumeroLicencia { get; set; } = string.Empty;
    }
}
