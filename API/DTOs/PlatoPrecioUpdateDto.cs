namespace Sistema_Gestion_Restaurante.API.DTOs;

public class PlatoPrecioUpdateDto
{
    // Solo incluimos el precio porque la HU3 dice "Debe permitir editar solo el precio"
    public decimal NuevoPrecio { get; set; }
}