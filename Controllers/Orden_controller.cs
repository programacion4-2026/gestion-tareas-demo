using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;// permite usar dbcontext
using Sistema_Gestion_Restaurante.Domain.Entities; // permite usar clases del dominio 

namespace Sistema_Gestion_Restaurante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Orden_Controller : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public Orden_Controller(RestauranteDbContext context)
        {
            _context = context;
        }

        // 1. Crear orden
        [HttpPost]
        public async Task<IActionResult> CrearOrden(Orden orden)// IAction devuelve respuestas HTTP
        {
            _context.Ordenes.Add(orden); // agrega la orden a la memoria no a la bd
            await _context.SaveChangesAsync(); // guarda en la base de datos
            return Ok(orden);
        }

        // 2. Agregar plato a la orden
        [HttpPost("agregar-plato")]
        public async Task<IActionResult> AgregarPlato(Guid ordenId, Guid platoId, int cantidad) // RECIBE ESTAS ORDENES C 
        {
            var plato = await _context.Platos.FindAsync(platoId);// BUSCA PLATO EN LA BD

            if (plato == null)
                return NotFound("Plato no encontrado");

            var detalle = new Detalle_orden
            {
                OrdenId = ordenId,
                PlatoId = platoId,
                Cantidad = cantidad,
                PrecioUnitario = plato.Precio
            }; // CREA EL DETALLE DE LA ORDEN

            _context.DetallesOrden.Add(detalle);//GUARDA EL DETALLE

            // Calcular total
            var orden = await _context.Ordenes.FindAsync(ordenId);
            orden.Total += plato.Precio * cantidad; // CUMPLEK HISTORIA DE USUARIO Y HACE CALCULOS AUTOMATICOS DEL TOTAL

            await _context.SaveChangesAsync();

            return Ok(detalle);
        }

        // 3. Cerrar orden
        [HttpPut("cerrar/{id}")] // viene en la URL 
        public async Task<IActionResult> CerrarOrden(Guid id)
        {
            var orden = await _context.Ordenes.FindAsync(id); //BUSCA LA ORDEN

            if (orden == null)
                return NotFound();

            orden.Estado = "Cerrada";
            await _context.SaveChangesAsync();// GUARFA TODO

            return Ok(orden);
        }
    }
}