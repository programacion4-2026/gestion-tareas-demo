namespace Sistema_Gestion_Restaurante.API.DTOs;

public class PlatoCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public Guid CategoriaId { get; set; }
}
