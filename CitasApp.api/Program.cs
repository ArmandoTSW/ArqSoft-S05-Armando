using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using System;
using Microsoft.AspNetCore.Builder;
using System.IO;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 1. Agregar los servicios al contenedor
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // --- REGISTRO DE REPOSITORIOS ---
    string dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "data");

    // SOLUCIÓN AL SOSPECHOSO 1:
    if (!Directory.Exists(dataDirectory))
    {
        Directory.CreateDirectory(dataDirectory);
    }

    builder.Services.AddScoped<IMedicoRepository>(provider => new JsonMedicoRepository(dataDirectory));
    builder.Services.AddScoped<ICitaRepository>(provider => new JsonCitaRepository(dataDirectory));
    builder.Services.AddScoped<IPacienteRepository>(provider => new JsonPacienteRepository(dataDirectory));

    // --- REGISTRO DE SERVICIOS ---
    builder.Services.AddScoped<MedicoService>();
    builder.Services.AddScoped<CitaService>();
    builder.Services.AddScoped<PacienteService>();

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

    // Configuración del pipeline de HTTP
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
}
catch (Exception ex)
{
    // Esto mantendrá la consola abierta y te dirá la verdad del error
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\n=======================================================");
    Console.WriteLine(" 🔥 ¡LA API SE HA CAÍDO AL INICIAR! 🔥 ");
    Console.WriteLine("=======================================================\n");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"EXCEPCIÓN: {ex.GetType().Name}");
    Console.WriteLine($"MENSAJE: {ex.Message}\n");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"DETALLES:\n{ex.StackTrace}");
    Console.ResetColor();

    Console.WriteLine("\nPresiona cualquier tecla para cerrar...");
    Console.ReadKey();

}