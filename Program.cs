using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Application.Services;
using Sistema_Gestion_Restaurante.Infrastructure.Repositories;
using Sistema_Gestion_Restaurante.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 0. Configurar la ruta base para buscar appsettings en la carpeta Config/
var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
builder.Configuration.SetBasePath(configPath);

// 1. Leer la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registrar nuestro RestauranteDbContext
builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseSqlite(connectionString));

// 3. REGISTRAR LOS REPOSITORIOS
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IPlatoRepository, PlatoRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IDetalleOrdenRepository, DetalleOrdenRepository>();

// 4. REGISTRAR LOS SERVICIOS
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IDetalleOrdenService, DetalleOrdenService>();
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

