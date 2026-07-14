using CitasApp.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CitasApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthPacienteService _authPaciente;
        private readonly AuthMedicoService _authMedico;

        public AccountController(AuthPacienteService authPaciente, AuthMedicoService authMedico)
        {
            _authPaciente = authPaciente;
            _authMedico = authMedico;
        }

        // ---------- PACIENTE ----------

        [HttpGet]
        public IActionResult LoginPaciente() => View();

        [HttpPost]
        public async Task<IActionResult> LoginPaciente(string email, string password)
        {
            try
            {
                var paciente = _authPaciente.Login(email, password);
                await IniciarSesion(paciente.Email, paciente.Id, "Paciente");
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult RegistroPaciente() => View();

        [HttpPost]
        public IActionResult RegistroPaciente(string nombre, string apellido, string email, string telefono, string password)
        {
            try
            {
                _authPaciente.Registrar(nombre, apellido, email, telefono, password);
                return RedirectToAction("LoginPaciente");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // ---------- MEDICO ----------

        [HttpGet]
        public IActionResult LoginMedico() => View();

        [HttpPost]
        public async Task<IActionResult> LoginMedico(string email, string password)
        {
            try
            {
                var medico = _authMedico.Login(email, password);
                await IniciarSesion(email, medico.Id, "Medico");
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult RegistroMedico() => View();

        [HttpPost]
        public IActionResult RegistroMedico(string nombre, string apellido, string especialidad, string numeroLicencia, string email, string password)
        {
            try
            {
                _authMedico.Registrar(nombre, apellido, especialidad, numeroLicencia, email, password);
                return RedirectToAction("LoginMedico");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // ---------- COMÚN ----------

        private async Task IniciarSesion(string email, int id, string rol)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.NameIdentifier, id.ToString()),
                new(ClaimTypes.Role, rol)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}