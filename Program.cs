using Microsoft.EntityFrameworkCore;
using MottuControlApi.Data;
using MottuControlApi.Services;

var builder = WebApplication.CreateBuilder(args);

//  Conexão com o Oracle via EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

//  Injeção de dependência dos serviços
builder.Services.AddScoped<PatioService>();
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<SensorService>();
builder.Services.AddScoped<ImagemService>();
builder.Services.AddScoped<StatusService>();

//  CORS (libera acesso externo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//  Documentação da API com Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Controllers
builder.Services.AddControllers();

var app = builder.Build();

//  Swagger somente no modo desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🌐 CORS global
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//  Seed de dados ao iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Inicializar(services);
}

app.Run();
