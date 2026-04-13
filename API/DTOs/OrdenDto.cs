namespace Sistema_Gestion_Restaurante.API.DTOs
{
    /// <summary>
    /// DTO para responder información de una orden
    /// </summary>
    public class OrdenDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string NumeroMesa { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
