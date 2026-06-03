using Citas_App.Interfaces;
using Citas_App.Models;
using System.Text.Json;

namespace Citas_App.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCitaRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "citas.json");
        }

        public List<Cita> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);

            // Si el JSON se mapea directo a Cita como en Medicos y Pacientes:
            return JsonSerializer.Deserialize<List<Cita>>(json, _options) ?? new();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();
    }
}