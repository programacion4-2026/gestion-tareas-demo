using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Infrastructure.Persistence
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
                    : base(options)
        {
        }

        // Aquí agregamos el resto de las tablas (DbSet)
        public DbSet<Plato> Platos => Set<Plato>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Orden> Ordenes => Set<Orden>();
        public DbSet<Detalle_orden> DetallesOrden => Set<Detalle_orden>();
        public DbSet<Reporte_Ventas> ReportesVentas => Set<Reporte_Ventas>();

        // Este método es para configurar detalles especiales (como en la guía del profe)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para que el precio funcione bien en SQLite
            modelBuilder.Entity<Plato>(entity => {
                entity.Property(p => p.Precio).HasConversion<double>();
            });

            modelBuilder.Entity<Orden>(entity => {
                entity.Property(o => o.Total).HasConversion<double>();
            });

            modelBuilder.Entity<Detalle_orden>(entity => {
                entity.Property(d => d.PrecioUnitario).HasConversion<double>();
            });
        }
    }
}