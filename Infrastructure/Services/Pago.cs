using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Pago : EntityBase
    {
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = "Efectivo"; // Ejemplo: Efectivo, Tarjeta

        // Relación con la Orden (si la tienen)
        public Guid OrdenId { get; set; }
        public Orden? Orden { get; set; }
    }
}