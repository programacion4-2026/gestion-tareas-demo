namespace Sistema_Gestion_Restaurante.Domain.Common
{
    public abstract class EntityBase
    {
        // La propiedad DEBE llamarse "Id" (mayúscula la I)
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}