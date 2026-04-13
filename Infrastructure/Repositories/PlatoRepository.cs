using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Restaurante.Application.Repositories;
using Sistema_Gestion_Restaurante.Domain.Entities;
using Sistema_Gestion_Restaurante.Infrastructure.Persistence;

namespace Sistema_Gestion_Restaurante.Infrastructure.Repositories;

public class PlatoRepository(RestauranteDbContext context) : IPlatoRepository
{
    private readonly RestauranteDbContext _context = context;

    // Obtiene todos los platos de la tabla
    public async Task<IReadOnlyList<Plato>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default)
    {
        return await _context.Platos.ToListAsync(ct);
    }

    // Obtiene por ID sin "rastreo" (más rápido, ideal para consultas)
    public async Task<Plato?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
    {
        return await _context.Platos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    // Obtiene por ID con "rastreo" (necesario para actualizar el precio en la HU3)
    public async Task<Plato?> GetByIdByUpdateAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default)
    {
        return await _context.Platos.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    // HU1: Lógica para buscar por nombre y validar que sea único
    public async Task<Plato?> GetByNameAsync(string nombre, CancellationToken ct = default)
    {
        return await _context.Platos
            .FirstOrDefaultAsync(p => p.Nombre.ToLower() == nombre.ToLower(), ct);
    }

    // Prepara un nuevo plato para ser insertado
    public async Task AddAsync(Plato plato, CancellationToken ct = default)
    {
        await _context.Platos.AddAsync(plato, ct);
    }

    // Guarda los cambios en la base de datos (Unit of Work)
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }

    // Simulación de borrado lógico
    public async Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default)
    {
        var plato = await GetByIdByUpdateAsync(id, false, ct);
        if (plato == null) return false;
        // Si tuvieras una propiedad IsDeleted, aquí la marcarías como true
        return true;
    }

    // Borrado definitivo de la base de datos
    public async Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default)
    {
        var plato = await _context.Platos.FindAsync(new object[] { id }, ct);
        if (plato == null) return false;
        _context.Platos.Remove(plato);
        return true;
    }
}

//AsNoTracking para consultas rápidas y métodos asíncronos para que la API no se bloquee."