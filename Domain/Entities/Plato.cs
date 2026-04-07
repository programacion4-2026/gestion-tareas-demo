using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Plato : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }

        // Relación N:1 con Categoria
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Relación 1:N con Detalle_Orden
        public ICollection<Detalle_orden> DetallesOrden { get; set; } = new List<Detalle_orden>();
    }
}