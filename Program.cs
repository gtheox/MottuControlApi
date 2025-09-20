using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MottuControlApi.Data;
using MottuControlApi.Repositories;
using MottuControlApi.Repositories.Contracts;
using MottuControlApi.Services;
using MottuControlApi.Services.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Conexão com Oracle via EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// 💉 Injeção de Dependência das Interfaces
// Repositories
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IPatioRepository, PatioRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IStatusMonitoramentoRepository, StatusMonitoramentoRepository>();
// Adicionar outros repositórios aqui se houver (ex: Imagem)

// Services
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<IPatioService, PatioService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IStatusMonitoramentoService, StatusMonitoramentoService>();
// Adicionar outros serviços aqui se houver (ex: Imagem)

// 🌐 Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("X-Pagination"); // Expor o cabeçalho de paginação
    });
});

// 🚀 Controllers
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// 📘 Swagger / OpenAPI com Documentação XML
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mottu Control API",
        Description = "API para gerenciamento de motos, pátios e sensores, construída com as melhores práticas de .NET."
    });

    // Habilita o uso dos comentários XML na UI do Swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// 🔎 Ativar Swagger em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🌐 Middleware CORS global
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 🧪 Seed inicial de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Inicializar(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu durante o seeding do banco de dados.");
    }
}

app.Run();