using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Application.Services;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Infrastructure.Services
{
    public class DetalleOrdenService : IDetalleOrdenService
    {
        private readonly IDetalleOrdenRepository _detalleOrdenRepository;
        private readonly IOrdenRepository _ordenRepository;
        private readonly IPlatoRepository _platoRepository;

        public DetalleOrdenService(
            IDetalleOrdenRepository detalleOrdenRepository,
            IOrdenRepository ordenRepository,
            IPlatoRepository platoRepository)
        {
            _detalleOrdenRepository = detalleOrdenRepository;
            _ordenRepository = ordenRepository;
            _platoRepository = platoRepository;
        }

        public async Task<IReadOnlyList<Detalle_orden>> GetAllDetailsAsync(CancellationToken ct = default)
        {
            return await _detalleOrdenRepository.GetAllAsync(false, ct);
        }

        public async Task<Detalle_orden?> GetDetailByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _detalleOrdenRepository.GetByIdAsync(id, false, ct);
        }

        public async Task<IReadOnlyList<Detalle_orden>> GetDetailsByOrderIdAsync(Guid ordenId, CancellationToken ct = default)
        {
            // Validar que la orden existe
            var orden = await _ordenRepository.GetByIdAsync(ordenId, false, ct);
            if (orden == null)
            {
                throw new KeyNotFoundException($"La orden con ID {ordenId} no fue encontrada.");
            }

            var todosLosDetalles = await _detalleOrdenRepository.GetAllAsync(false, ct);
            return todosLosDetalles.Where(d => d.OrdenId == ordenId).ToList().AsReadOnly();
        }

        public async Task<Guid> AddDetailsToOrderAsync(Guid ordenId, Guid platoId, int cantidad, decimal precioUnitario, CancellationToken ct = default)
        {
            // Validaciones de negocio
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0.", nameof(cantidad));
            }

            if (precioUnitario <= 0)
            {
                throw new ArgumentException("El precio unitario debe ser mayor a 0.", nameof(precioUnitario));
            }

            // Validar que la orden existe
            var orden = await _ordenRepository.GetByIdAsync(ordenId, false, ct);
            if (orden == null)
            {
                throw new KeyNotFoundException($"La orden con ID {ordenId} no fue encontrada.");
            }

            // Validar que el plato existe
            var plato = await _platoRepository.GetByIdAsync(platoId, false, ct);
            if (plato == null)
            {
                throw new KeyNotFoundException($"El plato con ID {platoId} no fue encontrado.");
            }

            var nuevoDetalle = new Detalle_orden
            {
                OrdenId = ordenId,
                PlatoId = platoId,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario
            };

            await _detalleOrdenRepository.AddAsync(nuevoDetalle, ct);
            await _detalleOrdenRepository.SaveChangesAsync(ct);

            return nuevoDetalle.Id;
        }

        public async Task<bool> UpdateDetailAsync(Guid detailId, int newCantidad, decimal newPrecioUnitario, CancellationToken ct = default)
        {
            // Validaciones
            if (newCantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0.", nameof(newCantidad));
            }

            if (newPrecioUnitario <= 0)
            {
                throw new ArgumentException("El precio unitario debe ser mayor a 0.", nameof(newPrecioUnitario));
            }

            var detalle = await _detalleOrdenRepository.GetByIdAsync(detailId, false, ct);
            if (detalle == null)
            {
                return false;
            }

            detalle.Cantidad = newCantidad;
            detalle.PrecioUnitario = newPrecioUnitario;
            await _detalleOrdenRepository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> RemoveDetailAsync(Guid detailId, CancellationToken ct = default)
        {
            var detalle = await _detalleOrdenRepository.GetByIdAsync(detailId, false, ct);
            if (detalle == null)
            {
                return false;
            }

            return await _detalleOrdenRepository.HardDeleteAsync(detailId, ct);
        }

        public async Task<decimal> CalculateSubtotalAsync(Guid detailId, CancellationToken ct = default)
        {
            var detalle = await _detalleOrdenRepository.GetByIdAsync(detailId, false, ct);
            if (detalle == null)
            {
                throw new KeyNotFoundException($"El detalle con ID {detailId} no fue encontrado.");
            }

            return detalle.Cantidad * detalle.PrecioUnitario;
        }

        public async Task<decimal> CalculateTotalByOrderAsync(Guid ordenId, CancellationToken ct = default)
        {
            var detalles = await GetDetailsByOrderIdAsync(ordenId, ct);

            decimal total = 0;
            foreach (var detalle in detalles)
            {
                total += detalle.Cantidad * detalle.PrecioUnitario;
            }

            return total;
        }

        public async Task<int> GetTotalItemsInOrderAsync(Guid ordenId, CancellationToken ct = default)
        {
            var detalles = await GetDetailsByOrderIdAsync(ordenId, ct);
            return detalles.Sum(d => d.Cantidad);
        }
    }
}
