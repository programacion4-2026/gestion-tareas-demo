namespace Sistema_Gestion_Restaurante.API.DTOs
{
    /// <summary>
    /// DTO para responder información de un detalle de orden
    /// </summary>
    public class DetalleOrdenDto
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public Guid PlatoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
