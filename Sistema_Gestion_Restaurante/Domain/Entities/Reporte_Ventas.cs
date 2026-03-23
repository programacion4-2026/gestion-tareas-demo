using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Reporte_Ventas : EntityBase
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal IngresosTotales { get; set; }
        public int TotalOrdenes { get; set; }
    }
}