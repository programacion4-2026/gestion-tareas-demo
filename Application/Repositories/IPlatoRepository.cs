using Sistema_Gestion_Restaurante.Domain.Entities; // Importa las entidades del dominio (como Plato)
using System.Collections;
using System.Diagnostics; // Librería para diagnóstico (no se usa aquí directamente)
using System.Diagnostics.Contracts; // Librería para contratos (tampoco se usa aquí directamente)

namespace Sistema_Gestion_Restaurante.Application.Repositories
{
    // Este namespace indica que este archivo pertenece a la capa de Aplicación, específicamente a los repositorios.

    // Interface: Es un contrato que define qué métodos deben existir, pero NO contiene la lógica.
    public interface IPlatoRepository
    {
        // Obtener la lista de todos los platos (puede incluir eliminados si se indica)
        Task<IReadOnlyList<Plato>> GetAllAsync(bool includeDeleted = false, CancellationToken ct = default);

        // Obtener un plato por su ID (solo lectura, sin seguimiento de cambios en Entity Framework)
        Task<Plato?> GetByIdAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);

        // Obtener un plato por su ID para actualizar (con seguimiento de cambios en Entity Framework)
        Task<Plato?> GetByIdByUpdateAsync(Guid id, bool includeDeleted = false, CancellationToken ct = default);

        // Buscar un plato por su nombre (sirve para validar que no se repita)
        Task<Plato?> GetByNameAsync(string nombre, CancellationToken ct = default);

        // Agregar un nuevo plato al contexto (aún no guarda en la base de datos)
        Task AddAsync(Plato plato, CancellationToken ct = default);

        // Guardar los cambios realizados en la base de datos de forma asíncrona
        Task SaveChangesAsync(CancellationToken ct = default);

        // Borrado lógico: marca el plato como eliminado pero no lo borra físicamente
        Task<bool> SoftDeleteAsync(Guid id, CancellationToken ct = default);

        // Borrado físico: elimina completamente el plato de la base de datos
        Task<bool> HardDeleteAsync(Guid id, CancellationToken ct = default);
    }

}

//Task: Indica que el método es asíncrono. Esto permite que el servidor atienda a otros usuarios mientras la base de datos busca la información.

//IReadOnlyList: Devuelve una lista que solo se puede leer. Es más seguro porque evita que alguien modifique la lista accidentalmente en la capa de aplicación.

//Modo lectura (No Tracking): El GetByIdAsync se usa para mostrar datos. Como no tiene "seguimiento", consume mucha menos memoria del servidor.
