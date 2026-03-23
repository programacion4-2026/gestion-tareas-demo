using Sistema_Gestion_Restaurante.Domain.Common; // 1. Agrega esto para encontrar EntityBase

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    // 2. Agrega ": EntityBase" aquí
    public class Plato : EntityBase
    {
        // 3. Agrega las propiedades de tu plato
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
    }
}