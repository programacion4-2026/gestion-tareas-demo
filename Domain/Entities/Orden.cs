using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Orden : EntityBase
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string NumeroMesa { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";

        // Relación 1:N con Detalle_Orden
        public ICollection<Detalle_orden> DetallesOrden { get; set; } = new List<Detalle_orden>();
    }
}