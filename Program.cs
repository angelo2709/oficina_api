using oficina_api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

// Adicione este using no topo

// ...

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Isso faz o serializador ignorar referências circulares globalmente
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<OficinaContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySQL(connectionString!);
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Oficina API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();