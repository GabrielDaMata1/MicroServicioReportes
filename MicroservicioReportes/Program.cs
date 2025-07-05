using Application.Query;
using Application.Services;
using Domain.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1",
        Description = "Documentaci�n de mi API usando Swagger"
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddHttpClient<UsuarioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/api/usuarios/");
});

builder.Services.AddHttpClient<SubastaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5003/api/usuarios/");
});

/*builder.Services.AddHttpClient<PujaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5004/api/Pujas/");
});*/


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ReporteSubastasRealizadasQuery).Assembly));


builder.Services.AddScoped<IUsuarioService, UsuarioService>();
//builder.Services.AddScoped<IPujaService, PujaService>();
builder.Services.AddScoped<ISubastaService, SubastaService>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    });
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
