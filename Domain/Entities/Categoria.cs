using Sistema_Gestion_Restaurante.Domain.Common;

namespace Sistema_Gestion_Restaurante.Domain.Entities
{
    public class Categoria : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;

        // Relación 1:N con Plato
        public ICollection<Plato> Platos { get; set; } = new List<Plato>();
    }
}