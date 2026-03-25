using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services
{
    public interface IOrdenService
    {
        Task<IReadOnlyList<Orden>> GetAllOrdersAsync(CancellationToken ct = default);

        Task<Orden?> GetOrderByIdAsync(Guid id, CancellationToken ct = default);

        Task<Guid> CreateOrderAsync(string numeroMesa, CancellationToken ct = default);

        Task<bool> UpdateOrderTotalAsync(Guid orderId, decimal newTotal, CancellationToken ct = default);

        Task<bool> CancelOrderAsync(Guid orderId, CancellationToken ct = default);

        Task<bool> CompleteOrderAsync(Guid orderId, CancellationToken ct = default);

        Task<IReadOnlyList<Orden>> GetOrdersByStatusAsync(string estado, CancellationToken ct = default);

        Task<IReadOnlyList<Orden>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);

        Task<decimal> CalculateTotalByOrderAsync(Guid orderId, CancellationToken ct = default);
    }
}
