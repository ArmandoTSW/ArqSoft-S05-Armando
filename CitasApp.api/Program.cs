using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Observers;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Esta API usa los repositorios en JSON (independiente de CitasApp.Web, que usa Postgres).
string dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "data");
if (!Directory.Exists(dataDirectory))
{
    Directory.CreateDirectory(dataDirectory);
}

builder.Services.AddScoped<IMedicoRepository>(provider => new JsonMedicoRepository(dataDirectory));
builder.Services.AddScoped<ICitaRepository>(provider => new JsonCitaRepository(dataDirectory));
builder.Services.AddScoped<IPacienteRepository>(provider => new JsonPacienteRepository(dataDirectory));

// Observadores de citas: se notifican automáticamente al confirmar una cita.
builder.Services.AddScoped<ICitaObserver, EmailObserver>();
builder.Services.AddScoped<ICitaObserver, SmsObserver>();

builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<PacienteService>();

// Permite que un frontend en React (localhost:3000) consuma esta API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();
