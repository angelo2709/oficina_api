using oficina_api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

// Adicione este using no topo


// Adicione esta linha para forçar a porta 7000 (HTTPS) e 7001 (HTTP)
builder.WebHost.UseUrls("https://localhost:7000;http://localhost:7001");

builder.Services.AddControllers();
// ... resto do código


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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();