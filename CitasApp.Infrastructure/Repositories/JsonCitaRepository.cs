using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    /// <summary>Guarda y consulta citas en un archivo citas.json (usado por CitasApp.api).</summary>
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCitaRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "citas.json");
        }

        public List<Cita> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Cita>>(json, _options) ?? new();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();

        /// <summary>
        /// Persiste los cambios de una cita reescribiendo el archivo JSON completo,
        /// ya que este repositorio no tiene una base de datos que lo haga por él.
        /// </summary>
        public void Actualizar(Cita cita)
        {
            var citas = ObtenerTodos();
            var index = citas.FindIndex(c => c.Id == cita.Id);
            if (index == -1) return;

            citas[index] = cita;
            File.WriteAllText(_path, JsonSerializer.Serialize(citas, _options));
        }
    }
}
