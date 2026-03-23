using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Application.Services;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Infrastructure.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IDetalleOrdenRepository _detalleOrdenRepository;

        public OrdenService(
            IOrdenRepository ordenRepository,
            IDetalleOrdenRepository detalleOrdenRepository)
        {
            _ordenRepository = ordenRepository;
            _detalleOrdenRepository = detalleOrdenRepository;
        }

        public async Task<IReadOnlyList<Orden>> GetAllOrdersAsync(CancellationToken ct = default)
        {
            return await _ordenRepository.GetAllAsync(false, ct);
        }

        public async Task<Orden?> GetOrderByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _ordenRepository.GetByIdAsync(id, false, ct);
        }

        public async Task<Guid> CreateOrderAsync(string numeroMesa, CancellationToken ct = default)
        {
            // Validación de negocio: número de mesa no puede estar vacío
            if (string.IsNullOrWhiteSpace(numeroMesa))
            {
                throw new ArgumentException("El número de mesa es requerido.", nameof(numeroMesa));
            }

            var nuevaOrden = new Orden
            {
                NumeroMesa = numeroMesa.Trim(),
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                Total = 0
            };

            await _ordenRepository.AddAsync(nuevaOrden, ct);
            await _ordenRepository.SaveChangesAsync(ct);

            return nuevaOrden.Id;
        }

        public async Task<bool> UpdateOrderTotalAsync(Guid orderId, decimal newTotal, CancellationToken ct = default)
        {
            // Validación: el total no puede ser negativo
            if (newTotal < 0)
            {
                throw new ArgumentException("El total no puede ser negativo.", nameof(newTotal));
            }

            var orden = await _ordenRepository.GetByIdAsync(orderId, false, ct);
            if (orden == null)
            {
                return false;
            }

            orden.Total = newTotal;
            await _ordenRepository.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> CancelOrderAsync(Guid orderId, CancellationToken ct = default)
        {
            var orden = await _ordenRepository.GetByIdAsync(orderId, false, ct);
            if (orden == null)
            {
                return false;
            }

            // Solo se pueden cancelar órdenes pendientes
            if (orden.Estado != "Pendiente")
            {
                throw new InvalidOperationException($"No se puede cancelar una orden en estado '{orden.Estado}'.");
            }

            orden.Estado = "Cancelada";
            orden.Total = 0;
            await _ordenRepository.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> CompleteOrderAsync(Guid orderId, CancellationToken ct = default)
        {
            var orden = await _ordenRepository.GetByIdAsync(orderId, false, ct);
            if (orden == null)
            {
                return false;
            }

            // Validación: solo se pueden completar órdenes pendientes
            if (orden.Estado != "Pendiente")
            {
                throw new InvalidOperationException($"No se puede completar una orden en estado '{orden.Estado}'.");
            }

            orden.Estado = "Completada";
            await _ordenRepository.SaveChangesAsync(ct);
            return true;
        }

        public async Task<IReadOnlyList<Orden>> GetOrdersByStatusAsync(string estado, CancellationToken ct = default)
        {
            var todasLasOrdenes = await _ordenRepository.GetAllAsync(false, ct);
            return todasLasOrdenes.Where(o => o.Estado == estado).ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Orden>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha de fin.", nameof(startDate));
            }

            var todasLasOrdenes = await _ordenRepository.GetAllAsync(false, ct);
            return todasLasOrdenes
                .Where(o => o.Fecha.Date >= startDate.Date && o.Fecha.Date <= endDate.Date)
                .ToList()
                .AsReadOnly();
        }

        public async Task<decimal> CalculateTotalByOrderAsync(Guid orderId, CancellationToken ct = default)
        {
            var orden = await _ordenRepository.GetByIdAsync(orderId, false, ct);
            if (orden == null)
            {
                throw new KeyNotFoundException($"La orden con ID {orderId} no fue encontrada.");
            }

            return orden.Total;
        }
    }
}
