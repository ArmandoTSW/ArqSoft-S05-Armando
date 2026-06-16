using CitasApp.Application.Services; // Asegúrate de tener este using
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar los servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- REGISTRO DE REPOSITORIOS (Acceso directo a datos) ---
// Definimos la ruta base de la carpeta donde están tus JSON
string dataDirectory = Path.Combine(builder.Environment.ContentRootPath, "data");

// Registro de repositorios con la ruta a los JSON
builder.Services.AddScoped<IMedicoRepository>(provider =>
    new JsonMedicoRepository(dataDirectory)
);
builder.Services.AddScoped<ICitaRepository>(provider =>
    new JsonCitaRepository(dataDirectory)
);
builder.Services.AddScoped<IPacienteRepository>(provider =>
    new JsonPacienteRepository(dataDirectory)
);

// --- REGISTRO DE SERVICIOS (AGREGADO PARA SOLUCIONAR EL ERROR 500) ---
// Registramos los servicios para que el contenedor pueda inyectarlos en los controladores
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<ICalculadoraService, CalculadoraService>();

// --------------------------------------------------------

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