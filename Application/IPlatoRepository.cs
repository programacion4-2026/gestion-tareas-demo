using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Repositories
{
    public interface IPlatoRepository
    {
        Task<IReadOnlyList<Plato>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default);

        Task<Plato?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);
        Task<Plato?> GetByIdByUpdateAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);

        Task AddAsync(Plato plato, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);

        Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default);

        Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default);
    }
}