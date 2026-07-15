using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Application.Services;
using CitasApp.Infrastructure;
using CitasApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// --- NUEVO: DbContext para login ---
builder.Services.AddDbContext<CitasAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CitasAppDb")));

// --- NUEVO: Cookie Authentication ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/LoginPaciente";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Infrastructure - Repositories (Entity Framework / Postgres)
builder.Services.AddScoped<IPacienteRepository, EfPacienteRepository>();

builder.Services.AddScoped<IMedicoRepository, EfMedicoRepository>();

builder.Services.AddScoped<ICitaRepository, EfCitaRepository>();

// --- NUEVO: repos de login (EF/Postgres) ---
builder.Services.AddScoped<ILoginPacienteRepository, EfLoginPacienteRepository>();
builder.Services.AddScoped<ILoginMedicoRepository, EfLoginMedicoRepository>();

// Application - Services
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<AuthPacienteService>(); // NUEVO
builder.Services.AddScoped<AuthMedicoService>();   // NUEVO

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // NUEVO — debe ir ANTES de UseAuthorization
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();