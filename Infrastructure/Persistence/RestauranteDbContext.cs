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
        public DbSet<Pago> Pagos { get; set; }

        // Este método es para configurar detalles especiales (como en la guía del profe)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Plato -> Categoria (N:1)
            modelBuilder.Entity<Plato>(entity =>
            {
                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Platos)
                    .HasForeignKey(p => p.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Precio).HasConversion<double>();
            });

            // Configuración de Orden -> Detalle_Orden (1:N)
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasMany(o => o.DetallesOrden)
                    .WithOne(d => d.Orden)
                    .HasForeignKey(d => d.OrdenId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(o => o.Total).HasConversion<double>();
            });

            // Configuración de Detalle_Orden
            modelBuilder.Entity<Detalle_orden>(entity =>
            {
                entity.HasOne(d => d.Plato)
                    .WithMany(p => p.DetallesOrden)
                    .HasForeignKey(d => d.PlatoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(d => d.PrecioUnitario).HasConversion<double>();
            });
        }
    }
}