using Sistema_Gestion_Restaurante.Application.Common.Models;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services;

public interface IPlatoService
{
    // HU1 y HU2: Crear plato con validaciones de nombre, precio y categoría
    Task<OperationResult<Guid>> CreateAsync(string nombre, decimal precio, Guid categoriaId, CancellationToken ct = default);

    // HU3: Actualizar solo el precio validando que no sea negativo
    Task<OperationResult<bool>> UpdatePriceAsync(Guid platoId, decimal newPrice, CancellationToken ct = default);

    // Métodos para consultas
    Task<OperationResult<IReadOnlyList<Plato>>> GetAllAsync(CancellationToken ct = default);
    Task<OperationResult<Plato>> GetByIdAsync(Guid id, CancellationToken ct = default);
}