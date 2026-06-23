using CitasApp.Domain.Interfaces;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(string environmentName, object env)
        {
            var dataPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "data"
            );

            return new JsonPacienteRepository(dataPath);
        }

        public static IMedicoRepository CrearMedicoRepository(object env)
        {
            var dataPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "data"
            );

            return new JsonMedicoRepository(dataPath);
        }

        public static ICitaRepository CrearCitaRepository(object env)
        {
            var dataPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "data"
            );

            return new JsonCitaRepository(dataPath);
        }
    }
}