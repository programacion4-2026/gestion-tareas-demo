using Sistema_Gestion_Restaurante.Application.Common.Models;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;

namespace Sistema_Gestion_Restaurante.Application.Services; // Corregido a Application

public class Plato_Service(IPlatoRepository repository) : IPlatoService // Asegúrate que el nombre sea IPlatoService
{
    private readonly IPlatoRepository _repository = repository;

    public async Task<OperationResult<IReadOnlyList<Plato>>> GetAllAsync(CancellationToken ct = default)
    {
        var platos = await _repository.GetAllAsync(false, ct);
        return OperationResult<IReadOnlyList<Plato>>.Success(platos);
    }

    public async Task<OperationResult<Plato>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var plato = await _repository.GetByIdAsync(id, false, ct);
        if (plato == null)
            return OperationResult<Plato>.Failure("El plato no existe.");

        return OperationResult<Plato>.Success(plato);
    }

    public async Task<OperationResult<Guid>> CreateAsync(string nombre, decimal precio, Guid categoriaId, CancellationToken ct = default)
    {
        if (precio <= 0)
            return OperationResult<Guid>.Failure("El precio debe ser mayor a cero.");

        var platoExistente = await _repository.GetByNameAsync(nombre, ct);
        if (platoExistente != null)
            return OperationResult<Guid>.Failure("Ya existe un plato con ese nombre.");

        if (categoriaId == Guid.Empty)
            return OperationResult<Guid>.Failure("La categoría es obligatoria.");

        var nuevoPlato = new Plato
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            Precio = precio,
            CategoriaId = categoriaId
        };

        await _repository.AddAsync(nuevoPlato, ct);
        await _repository.SaveChangesAsync(ct);

        return OperationResult<Guid>.Success(nuevoPlato.Id);
    }

    public async Task<OperationResult<bool>> UpdatePriceAsync(Guid platoId, decimal newPrice, CancellationToken ct = default)
    {
        if (newPrice < 0)
            return OperationResult<bool>.Failure("El precio no puede ser un valor negativo.");

        var plato = await _repository.GetByIdByUpdateAsync(platoId, false, ct);

        if (plato == null)
            return OperationResult<bool>.Failure("Plato no encontrado.");

        plato.Precio = newPrice;
        await _repository.SaveChangesAsync(ct);
        return OperationResult<bool>.Success(true);
    }

    public async Task<OperationResult<bool>> DeleteAsync(Guid platoId, CancellationToken ct = default)
    {
        var result = await _repository.HardDeleteAsync(platoId, ct);
        if (!result) return OperationResult<bool>.Failure("No se pudo eliminar el plato.");

        await _repository.SaveChangesAsync(ct);
        return OperationResult<bool>.Success(true);
    }
}
