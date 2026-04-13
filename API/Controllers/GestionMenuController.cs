using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.API.DTOs;
using Sistema_Gestion_Restaurante.Application.Services;

namespace Sistema_Gestion_Restaurante.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GestionMenuController(IPlatoService platoService) : ControllerBase
{
    private readonly IPlatoService _platoService = platoService;

    [HttpPost]
    public async Task<IActionResult> RegistrarPlato([FromBody] PlatoCreateDto dto, CancellationToken ct)
    {
        var result = await _platoService.CreateAsync(dto.Nombre, dto.Precio, dto.CategoriaId, ct);

        // Cambiamos Error -> ErrorMessage y Value -> Data
        if (!result.IsSuccess)
            return BadRequest(new { mensaje = result.ErrorMessage });

        return CreatedAtAction(nameof(ObtenerPlatoPorId), new { id = result.Data }, new { id = result.Data });
    }

    [HttpPatch("{id}/precio")]
    public async Task<IActionResult> ActualizarPrecio(Guid id, [FromBody] PlatoPrecioUpdateDto dto, CancellationToken ct)
    {
        var result = await _platoService.UpdatePriceAsync(id, dto.NuevoPrecio, ct);

        if (!result.IsSuccess)
            return BadRequest(new { mensaje = result.ErrorMessage });

        return Ok(new { mensaje = "Precio actualizado correctamente." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPlatoPorId(Guid id, CancellationToken ct)
    {
        var result = await _platoService.GetByIdAsync(id, ct);

        if (!result.IsSuccess)
            return NotFound(new { mensaje = result.ErrorMessage });

        return Ok(result.Data);
    }
}