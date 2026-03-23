using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Repositories
{
    public interface IOrdenRepository
    {
        Task<IReadOnlyList<Orden>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default);

        Task<Orden?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);

        Task AddAsync(Orden orden, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct = default);

        Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default);

        Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default);
    }
}
