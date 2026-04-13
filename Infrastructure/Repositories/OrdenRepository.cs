using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;

namespace Sistema_Gestion_Restaurante.Infrastructure.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly RestauranteDbContext _context;

        public OrdenRepository(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Orden>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Ordenes.ToListAsync(ct);
        }

        public async Task<Orden?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.Ordenes.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(Orden orden, CancellationToken ct = default)
        {
            await _context.Ordenes.AddAsync(orden, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var orden = await GetByIdAsync(id, false, ct);
            if (orden == null) return false;
            // Aquí podrías marcar una propiedad "IsDeleted" si la tuvieras
            return true;
        }

        public async Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var orden = await GetByIdAsync(id, false, ct);
            if (orden == null) return false;
            _context.Ordenes.Remove(orden);
            return true;
        }
    }
}
