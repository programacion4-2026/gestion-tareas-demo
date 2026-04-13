using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services
{
    public interface IDetalleOrdenService
    {
        Task<IReadOnlyList<Detalle_orden>> GetAllDetailsAsync(CancellationToken ct = default);

        Task<Detalle_orden?> GetDetailByIdAsync(Guid id, CancellationToken ct = default);

        Task<IReadOnlyList<Detalle_orden>> GetDetailsByOrderIdAsync(Guid ordenId, CancellationToken ct = default);

        Task<Guid> AddDetailsToOrderAsync(Guid ordenId, Guid platoId, int cantidad, decimal precioUnitario, CancellationToken ct = default);

        Task<bool> UpdateDetailAsync(Guid detailId, int newCantidad, decimal newPrecioUnitario, CancellationToken ct = default);

        Task<bool> RemoveDetailAsync(Guid detailId, CancellationToken ct = default);

        Task<decimal> CalculateSubtotalAsync(Guid detailId, CancellationToken ct = default);

        Task<decimal> CalculateTotalByOrderAsync(Guid ordenId, CancellationToken ct = default);

        Task<int> GetTotalItemsInOrderAsync(Guid ordenId, CancellationToken ct = default);
    }
}
