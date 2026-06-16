using CitasApp.Infrastructure.Repositories;
using CitasApp.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar los servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- AQUÍ ESTÁ LA CORRECCIÓN ---

// Definimos la ruta base de la carpeta donde están tus JSON
string dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "data");

// Registramos los repositorios pasando la ruta mediante una función lambda
builder.Services.AddScoped<IMedicoRepository>(provider =>
    new JsonMedicoRepository(dataDirectory)
);

builder.Services.AddScoped<ICitaRepository>(provider =>
    new JsonCitaRepository(dataDirectory)
);

// Si tienes uno de pacientes, haz lo mismo:
// builder.Services.AddScoped<IPacienteRepository>(provider => 
//     new JsonPacienteRepository(dataDirectory)
// );

// --------------------------------

var app = builder.Build();

// Configuración del pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();