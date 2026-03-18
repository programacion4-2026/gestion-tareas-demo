using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //URL BASE DEL CONTROLADOR 
    public class Categoria_Controller : ControllerBase
    {
        private readonly RestauranteDbContext _context; //READONLY SOLO SE PUEDE ASIGNAR UNA VEZ

        public Categoria_Controller(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpPost] //metodo1. crear categoria RESPONDE A UNA PETICION POST 
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria); // PARA ACCEDER A LAS TABLAS DE CATEGORIAS 
            await _context.SaveChangesAsync();//GUARDA CAMBIOS EN LA BASE DE DATOS 
            return Ok(categoria);
        }

        [HttpGet] // METODO2.obtener categorias 
        public async Task<IActionResult> GetCategorias() //metodo que devuelve todad las categorias
        {
            return Ok(_context.Categorias.ToList());
        }
    }
}
