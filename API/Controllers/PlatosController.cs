using Microsoft.AspNetCore.Mvc;// Importa herramientas para crear controladores API (HTTP, rutas, respuestas)
using Sistema_Gestion_Restaurante.Application.Services; //// Importa los servicios de la capa de aplicación
using Sistema_Gestion_Restaurante.API.DTOs; //// Importa los DTOs (objetos para enviar/recibir datos)

namespace Sistema_Gestion_Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatosController(IPlatoService platoService) : ControllerBase
{
    private readonly IPlatoService _platoService = platoService;

    // POST: api/platos (HU1 y HU2)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlatoCreateDto dto)
    {
        // Llama al servicio para crear un plato con los datos recibidos
        var result = await _platoService.CreateAsync(dto.Nombre, dto.Precio, dto.CategoriaId);
        // Si ocurre un error, devuelve un 400 (BadRequest) con el mensaje
        if (!result.IsSuccess)
            return BadRequest(new { mensaje = result.ErrorMessage });

        return Ok(new { data = result.Data, mensaje = "Plato creado exitosamente" });
    }

    // PATCH: api/platos/{id}/precio (HU3)
    [HttpPatch("{id}/precio")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] PlatoPrecioUpdateDto dto)
    {
        var result = await _platoService.UpdatePriceAsync(id, dto.NuevoPrecio);

        if (!result.IsSuccess)
            return BadRequest(new { mensaje = result.ErrorMessage });

        return Ok(new { mensaje = "Precio actualizado correctamente" });
    }

    // GET: api/platos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _platoService.GetAllAsync();
        return Ok(result.Data);
    }
}