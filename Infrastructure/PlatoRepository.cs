using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;

namespace Sistema_Gestion_Restaurante.Infrastructure.Repositories
{
    public class PlatoRepository(RestauranteDbContext context) : IPlatoRepository
    {
        private readonly RestauranteDbContext _context = context;

        public async Task<IReadOnlyList<Plato>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Platos.ToListAsync(ct);
        }

        public async Task<Plato?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
        {
              
            return await _context.Platos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<Plato?> GetByIdByUpdateAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Platos.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(Plato plato, CancellationToken ct = default)
        {
            await _context.Platos.AddAsync(plato, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var plato = await GetByIdAsync(id, false, ct);
            if (plato == null) return false;
            // Aquí podrías marcar una propiedad "IsDeleted" si la tuvieras
            return true;
        }

        public async Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var plato = await GetByIdAsync(id, false, ct);
            if (plato == null) return false;
            _context.Platos.Remove(plato);
            return true;
        }
    }
}