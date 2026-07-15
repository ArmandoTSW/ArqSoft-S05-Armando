using System.Security.Cryptography;
using System.Text;

namespace CitasApp.Infrastructure.Security
{
    /// <summary>
    /// Convierte contraseñas en texto plano a un hash SHA-256 para no
    /// guardarlas directamente en la base de datos.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>Genera el hash de una contraseña.</summary>
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        /// <summary>Compara una contraseña en texto plano contra un hash ya guardado.</summary>
        public static bool Verificar(string password, string hash)
        {
            return Hash(password) == hash;
        }
    }
}
