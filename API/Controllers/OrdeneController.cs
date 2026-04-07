using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.API.DTOs;
using Sistema_Gestion_Restaurante.Application.Services;

namespace Sistema_Gestion_Restaurante.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        private readonly IDetalleOrdenService _detalleOrdenService;

        public OrdenesController(
            IOrdenService ordenService,
            IDetalleOrdenService detalleOrdenService)
        {
            _ordenService = ordenService;
            _detalleOrdenService = detalleOrdenService;
        }

        /// <summary>
        /// HU4: Crear una nueva orden
        /// </summary>
        /// <param name="createOrdenDto">Datos para crear la orden (número de mesa)</param>
        /// <returns>Id de la orden creada</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrdenDto createOrdenDto, CancellationToken ct = default)
        {
            try
            {
                var ordenId = await _ordenService.CreateOrderAsync(createOrdenDto.NumeroMesa, ct);
                return CreatedAtAction(nameof(GetOrderById), new { id = ordenId }, new { id = ordenId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener una orden por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenDto>> GetOrderById(Guid id, CancellationToken ct = default)
        {
            try
            {
                var orden = await _ordenService.GetOrderByIdAsync(id, ct);
                if (orden == null)
                    return NotFound(new { message = $"La orden con ID {id} no fue encontrada." });

                var ordenDto = new OrdenDto
                {
                    Id = orden.Id,
                    Fecha = orden.Fecha,
                    Total = orden.Total,
                    NumeroMesa = orden.NumeroMesa,
                    Estado = orden.Estado
                };

                return Ok(ordenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener todas las órdenes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrdenDto>>> GetAllOrders(CancellationToken ct = default)
        {
            try
            {
                var ordenes = await _ordenService.GetAllOrdersAsync(ct);
                var ordenesDto = ordenes.Select(o => new OrdenDto
                {
                    Id = o.Id,
                    Fecha = o.Fecha,
                    Total = o.Total,
                    NumeroMesa = o.NumeroMesa,
                    Estado = o.Estado
                }).ToList();

                return Ok(ordenesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// HU7: Cerrar una orden (cambiar estado a Completada)
        /// </summary>
        [HttpPost("{id}/completar")]
        public async Task<ActionResult> CompleteOrder(Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await _ordenService.CompleteOrderAsync(id, ct);
                if (!result)
                    return NotFound(new { message = $"La orden con ID {id} no fue encontrada." });

                return Ok(new { message = "La orden fue completada exitosamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cancelar una orden
        /// </summary>
        [HttpPost("{id}/cancelar")]
        public async Task<ActionResult> CancelOrder(Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await _ordenService.CancelOrderAsync(id, ct);
                if (!result)
                    return NotFound(new { message = $"La orden con ID {id} no fue encontrada." });

                return Ok(new { message = "La orden fue cancelada exitosamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// HU6: Obtener el total de una orden
        /// </summary>
        [HttpGet("{id}/total")]
        public async Task<ActionResult<decimal>> GetOrderTotal(Guid id, CancellationToken ct = default)
        {
            try
            {
                var total = await _detalleOrdenService.CalculateTotalByOrderAsync(id, ct);
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
        /// Actualizar el total de una orden manualmente
        /// </summary>
        [HttpPut("{id}/total")]
        public async Task<ActionResult> UpdateOrderTotal(Guid id, [FromBody] UpdateOrdenTotalDto updateDto, CancellationToken ct = default)
        {
            try
            {
                var result = await _ordenService.UpdateOrderTotalAsync(id, updateDto.Total, ct);
                if (!result)
                    return NotFound(new { message = $"La orden con ID {id} no fue encontrada." });

                return Ok(new { message = "El total fue actualizado exitosamente." });
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
        /// Obtener órdenes por estado
        /// </summary>
        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IReadOnlyList<OrdenDto>>> GetOrdersByStatus(string estado, CancellationToken ct = default)
        {
            try
            {
                var ordenes = await _ordenService.GetOrdersByStatusAsync(estado, ct);
                var ordenesDto = ordenes.Select(o => new OrdenDto
                {
                    Id = o.Id,
                    Fecha = o.Fecha,
                    Total = o.Total,
                    NumeroMesa = o.NumeroMesa,
                    Estado = o.Estado
                }).ToList();

                return Ok(ordenesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
