using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services
{
    public interface ICategoriaService
    {
        Task<IReadOnlyList<Categoria>> GetAllAsync(CancellationToken ct = default);
        Task<Categoria?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateAsync(string nombre, CancellationToken ct = default);

        Task<bool> UpdateAsync(Guid id, string nombre, CancellationToken ct = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}

