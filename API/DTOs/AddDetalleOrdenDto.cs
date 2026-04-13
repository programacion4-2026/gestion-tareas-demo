namespace Sistema_Gestion_Restaurante.API.DTOs
{
    /// <summary>
    /// DTO para agregar un detalle (plato) a una orden
    /// </summary>
    public class AddDetalleOrdenDto
    {
        public Guid PlatoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
