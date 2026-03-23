using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;

namespace Sistema_Gestion_Restaurante.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly RestauranteDbContext _context;

        public CategoriaRepository(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Categoria>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Categorias.ToListAsync(ct);
        }

        public async Task<Categoria?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(Categoria categoria, CancellationToken ct = default)
        {
            await _context.Categorias.AddAsync(categoria, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var categoria = await GetByIdAsync(id, false, ct);
            if (categoria == null) return false;
            return true;
        }

        public async Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var categoria = await GetByIdAsync(id, false, ct);
            if (categoria == null) return false;
            _context.Categorias.Remove(categoria);
            return true;
        }
    }
}
