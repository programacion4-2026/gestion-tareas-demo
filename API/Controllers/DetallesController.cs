using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.API.DTOs;
using Sistema_Gestion_Restaurante.Application.Services;

namespace Sistema_Gestion_Restaurante.API.Controllers
{
    [ApiController]
    [Route("api/ordenes/{ordenId}/[controller]")]
    public class DetallesController : ControllerBase
    {
        private readonly IDetalleOrdenService _detalleOrdenService;

        public DetallesController(IDetalleOrdenService detalleOrdenService)
        {
            _detalleOrdenService = detalleOrdenService;
        }

        /// <summary>
        /// HU5: Agregar un plato a la orden
        /// </summary>
        /// <param name="ordenId">Id de la orden</param>
        /// <param name="addDetalleDto">Datos del plato a agregar (platoId, cantidad, precio)</param>
        /// <returns>Id del detalle creado</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> AddDetailToOrder(Guid ordenId, [FromBody] AddDetalleOrdenDto addDetalleDto, CancellationToken ct = default)
        {
            try
            {
                var detalleId = await _detalleOrdenService.AddDetailsToOrderAsync(
                    ordenId,
                    addDetalleDto.PlatoId,
                    addDetalleDto.Cantidad,
                    addDetalleDto.PrecioUnitario,
                    ct);

                return CreatedAtAction(nameof(GetDetailById), new { ordenId = ordenId, id = detalleId }, new { id = detalleId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener todos los detalles de una orden
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DetalleOrdenDto>>> GetDetailsByOrder(Guid ordenId, CancellationToken ct = default)
        {
            try
            {
                var detalles = await _detalleOrdenService.GetDetailsByOrderIdAsync(ordenId, ct);
                var detallesDto = detalles.Select(d => new DetalleOrdenDto
                {
                    Id = d.Id,
                    OrdenId = d.OrdenId,
                    PlatoId = d.PlatoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Cantidad * d.PrecioUnitario
                }).ToList();

                return Ok(detallesDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener un detalle específico de la orden
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleOrdenDto>> GetDetailById(Guid ordenId, Guid id, CancellationToken ct = default)
        {
            try
            {
                var detalle = await _detalleOrdenService.GetDetailByIdAsync(id, ct);
                if (detalle == null || detalle.OrdenId != ordenId)
                    return NotFound(new { message = $"El detalle con ID {id} no fue encontrado en la orden {ordenId}." });

                var detalleDto = new DetalleOrdenDto
                {
                    Id = detalle.Id,
                    OrdenId = detalle.OrdenId,
                    PlatoId = detalle.PlatoId,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = detalle.Cantidad * detalle.PrecioUnitario
                };

                return Ok(detalleDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualizar cantidad y precio de un detalle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDetail(Guid ordenId, Guid id, [FromBody] AddDetalleOrdenDto updateDetalleDto, CancellationToken ct = default)
        {
            try
            {
                var detalle = await _detalleOrdenService.GetDetailByIdAsync(id, ct);
                if (detalle == null || detalle.OrdenId != ordenId)
                    return NotFound(new { message = $"El detalle con ID {id} no fue encontrado en la orden {ordenId}." });

                var result = await _detalleOrdenService.UpdateDetailAsync(id, updateDetalleDto.Cantidad, updateDetalleDto.PrecioUnitario, ct);
                if (!result)
                    return NotFound(new { message = $"El detalle no pudo ser actualizado." });

                return Ok(new { message = "El detalle fue actualizado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Eliminar un detalle de la orden
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDetail(Guid ordenId, Guid id, CancellationToken ct = default)
        {
            try
            {
                var detalle = await _detalleOrdenService.GetDetailByIdAsync(id, ct);
                if (detalle == null || detalle.OrdenId != ordenId)
                    return NotFound(new { message = $"El detalle con ID {id} no fue encontrado en la orden {ordenId}." });

                var result = await _detalleOrdenService.RemoveDetailAsync(id, ct);
                if (!result)
                    return NotFound(new { message = $"El detalle no pudo ser eliminado." });

                return Ok(new { message = "El detalle fue eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// HU6: Obtener el total de una orden (suma de todos los subtotales)
        /// </summary>
        [HttpGet("total")]
        public async Task<ActionResult<decimal>> GetOrderTotal(Guid ordenId, CancellationToken ct = default)
        {
            try
            {
                var total = await _detalleOrdenService.CalculateTotalByOrderAsync(ordenId, ct);
                return Ok(new { total = total });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener la cantidad total de items en una orden
        /// </summary>
        [HttpGet("items-count")]
        public async Task<ActionResult<int>> GetTotalItemsInOrder(Guid ordenId, CancellationToken ct = default)
        {
            try
            {
                var itemsCount = await _detalleOrdenService.GetTotalItemsInOrderAsync(ordenId, ct);
                return Ok(new { itemsCount = itemsCount });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
