using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);
// 1. Leer la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registrar nuestro RestauranteDbContext
builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseSqlite(connectionString));
// Add services to the container.

// --- 3. REGISTRAR EL REPOSITORIO () ---

builder.Services.AddScoped<IPlatoRepository, PlatoRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

