using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Detalle_orden : EntityBase
    {
        public Guid OrdenId { get; set; }
        public Guid PlatoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Relaciones N:1
        public Orden? Orden { get; set; }
        public Plato? Plato { get; set; }
    }
}