using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
