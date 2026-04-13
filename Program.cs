using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Application.Services;
using Sistema_Gestion_Restaurante.Infrastructure.Repositories;
using Sistema_Gestion_Restaurante.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 0. Configurar la ruta base para buscar appsettings en la carpeta Config/
var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
builder.Configuration.SetBasePath(configPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

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
builder.Services.AddScoped<IPlatoService, Plato_Service>(); // Servicio para tus HU1, HU2 y HU3

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestauranteDbContext>();
    try
    {
        Console.WriteLine("\n=== DIAGNÓSTICO DE BASE DE DATOS ===");
        Console.WriteLine($"📍 Current Directory: {Directory.GetCurrentDirectory()}");
        Console.WriteLine($"📍 Connection String: {connectionString}");

        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "Restaurante.db");
        Console.WriteLine($"📍 BD esperada en: {dbPath}");

        if (app.Environment.IsDevelopment())
        {
            // En desarrollo: elimina la BD y la recrea desde cero (Útil para pruebas limpias)
            try
            {
                Console.WriteLine("Eliminando base de datos existente...");
                dbContext.Database.EnsureDeleted();
                Console.WriteLine("✓ Base de datos eliminada");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Error eliminando BD: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Creando nueva base de datos...");
                dbContext.Database.EnsureCreated();
                Console.WriteLine("✓ Base de datos creada exitosamente");

                if (File.Exists(dbPath))
                {
                    var fileInfo = new FileInfo(dbPath);
                    Console.WriteLine($"✓ Archivo verificado: {dbPath}");
                    Console.WriteLine($"📊 Tamaño: {fileInfo.Length} bytes");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error creando BD: {ex.Message}");
                throw;
            }
        }
        else
        {
            dbContext.Database.Migrate();
            Console.WriteLine("✓ Base de datos migrada exitosamente");
        }

        Console.WriteLine("===================================\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error al preparar la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();