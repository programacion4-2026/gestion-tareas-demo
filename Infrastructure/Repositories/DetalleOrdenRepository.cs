using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;

namespace Sistema_Gestion_Restaurante.Infrastructure.Repositories
{
    public class DetalleOrdenRepository : IDetalleOrdenRepository
    {
        private readonly RestauranteDbContext _context;

        public DetalleOrdenRepository(RestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Detalle_orden>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.DetallesOrden.ToListAsync(ct);
        }

        public async Task<Detalle_orden?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
        {
            return await _context.DetallesOrden.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(Detalle_orden detalle, CancellationToken ct = default)
        {
            await _context.DetallesOrden.AddAsync(detalle, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var detalle = await GetByIdAsync(id, false, ct);
            if (detalle == null) return false;
            return true;
        }

        public async Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default)
        {
            var detalle = await GetByIdAsync(id, false, ct);
            if (detalle == null) return false;
            _context.DetallesOrden.Remove(detalle);
            return true;
        }
    }
}
