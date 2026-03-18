using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public CategoriaController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            return Ok(_context.Categorias.ToList());
        }
    }
}