using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Repositories
{
    public interface IDetalleOrdenRepository
    {
        Task<IReadOnlyList<Detalle_orden>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default);

        Task<Detalle_orden?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);

        Task AddAsync(Detalle_orden detalle, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);

        Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default);

        Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default);
    }
}
