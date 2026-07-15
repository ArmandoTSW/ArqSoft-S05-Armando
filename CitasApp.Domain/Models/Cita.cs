namespace CitasApp.Domain.Models
{
    /// <summary>Una cita médica entre un paciente y un médico.</summary>
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Motivo { get; set; } = string.Empty;

        /// <summary>Estado de la cita: "Pendiente" o "Confirmada".</summary>
        public string Estado { get; set; } = "Pendiente";
    }
}
