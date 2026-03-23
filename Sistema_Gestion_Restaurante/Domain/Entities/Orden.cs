using Sistema_Gestion_Restaurante.Domain.Common;

using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Orden : EntityBase
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public string NumeroMesa { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";
    }
}