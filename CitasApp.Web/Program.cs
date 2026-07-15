using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Application.Services;
using CitasApp.Infrastructure;
using CitasApp.Infrastructure.Data;
using CitasApp.Infrastructure.Observers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Conexión a la base de datos Postgres (pacientes, médicos, citas y logins)
builder.Services.AddDbContext<CitasAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CitasAppDb")));

// Autenticación por cookie: a dónde mandar al usuario si no está logueado
// o si intenta entrar a algo para lo que no tiene permiso.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/LoginPaciente";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Repositorios (Entity Framework / Postgres)
builder.Services.AddScoped<IPacienteRepository, EfPacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, EfMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, EfCitaRepository>();
builder.Services.AddScoped<ILoginPacienteRepository, EfLoginPacienteRepository>();
builder.Services.AddScoped<ILoginMedicoRepository, EfLoginMedicoRepository>();

// Observadores de citas: se notifican automáticamente al confirmar una cita.
builder.Services.AddScoped<ICitaObserver, EmailObserver>();
builder.Services.AddScoped<ICitaObserver, SmsObserver>();

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<AuthPacienteService>();
builder.Services.AddScoped<AuthMedicoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// UseAuthentication debe ir ANTES de UseAuthorization para que los [Authorize] funcionen.
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();