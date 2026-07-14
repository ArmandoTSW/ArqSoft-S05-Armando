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

// Infrastructure - Repositories (lo que ya tenías)
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new JsonPacienteRepository(Path.Combine(env.ContentRootPath, "data"));
});

builder.Services.AddScoped<IMedicoRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new JsonMedicoRepository(Path.Combine(env.ContentRootPath, "data"));
});

builder.Services.AddScoped<ICitaRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new JsonCitaRepository(Path.Combine(env.ContentRootPath, "data"));
});

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