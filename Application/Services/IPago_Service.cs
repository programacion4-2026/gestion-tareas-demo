using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services
{
    public interface IPagoService
    {
        Task<IReadOnlyList<Pago>> GetAllAsync(CancellationToken ct = default);

        Task<Pago?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<IReadOnlyList<Pago>> GetByOrderIdAsync(Guid ordenId, CancellationToken ct = default);

        Task<Guid> RegisterPaymentAsync(Guid ordenId, decimal monto, string metodoPago, CancellationToken ct = default);

        Task<bool> ConfirmPaymentAsync(Guid pagoId, CancellationToken ct = default);

        Task<bool> DeleteAsync(Guid pagoId, CancellationToken ct = default);
    }
}
