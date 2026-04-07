# Historias de Usuario Implementadas

## Resumen
Con la estructura actual del proyecto, se han implementado las **Historias de Usuario 4-7**:

---

## HU 4: Como mesero quiero crear una orden
**Endpoint:** `POST /api/ordenes`

**Request Body:**
```json
{
  "numeroMesa": "5"
}
```

**Response (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000"
}
```

**Excepciones:**
- 400: Si el número de mesa está vacío o nulo

---

## HU 5: Como mesero quiero agregar platos a la orden
**Endpoint:** `POST /api/ordenes/{ordenId}/detalles`

**Request Body:**
```json
{
  "platoId": "550e8400-e29b-41d4-a716-446655440000",
  "cantidad": 2,
  "precioUnitario": 25.50
}
```

**Response (201 Created):**
```json
{
  "id": "660e8400-e29b-41d4-a716-446655440001"
}
```

**Excepciones:**
- 400: Si la cantidad o precio son menores o iguales a 0
- 404: Si la orden o el plato no existen

---

## HU 6: Como sistema quiero calcular el total automáticamente
**Endpoint Principal:** `GET /api/ordenes/{ordenId}/detalles/total`

Este endpoint suma todos los subtotales (cantidad × precio) de los detalles de una orden.

**Response:**
```json
{
  "total": 103.50
}
```

**Excepciones:**
- 404: Si la orden no existe

### Endpoints de Soporte:
- `GET /api/ordenes/{ordenId}/detalles` - Obtener todos los detalles de una orden
- `GET /api/ordenes/{ordenId}/detalles/{id}` - Obtener un detalle específico

---

## HU 7: Como usuario quiero cerrar la orden
**Endpoint:** `POST /api/ordenes/{ordenId}/completar`

**Response (200 OK):**
```json
{
  "message": "La orden fue completada exitosamente."
}
```

**Excepciones:**
- 404: Si la orden no existe
- 400: Si la orden ya está completada o cancelada

---

## Endpoints Adicionales Disponibles

### Órdenes
- `GET /api/ordenes` - Obtener todas las órdenes
- `GET /api/ordenes/{id}` - Obtener una orden por ID
- `POST /api/ordenes/{id}/cancelar` - Cancelar una orden
- `PUT /api/ordenes/{id}/total` - Actualizar el total manualmente
- `GET /api/ordenes/estado/{estado}` - Filtrar órdenes por estado

### Detalles de Órdenes
- `GET /api/ordenes/{ordenId}/detalles` - Listar todos los detalles
- `GET /api/ordenes/{ordenId}/detalles/{id}` - Obtener un detalle específico
- `PUT /api/ordenes/{ordenId}/detalles/{id}` - Actualizar cantidad/precio
- `DELETE /api/ordenes/{ordenId}/detalles/{id}` - Eliminar un detalle
- `GET /api/ordenes/{ordenId}/detalles/items-count` - Contar items en la orden

---

## Flujo Recomendado de Uso

1. **Crear una orden** (HU4)
   ```
   POST /api/ordenes
   ```

2. **Agregar platos a la orden** (HU5)
   ```
   POST /api/ordenes/{ordenId}/detalles
   POST /api/ordenes/{ordenId}/detalles (repetir para más platos)
   ```

3. **Verificar el total** (HU6)
   ```
   GET /api/ordenes/{ordenId}/detalles/total
   ```

4. **Cerrar/Completar la orden** (HU7)
   ```
   POST /api/ordenes/{ordenId}/completar
   ```

---

## Estructura de Carpetas Creada

```
API/
├── DTOs/
│   ├── CreateOrdenDto.cs
│   ├── OrdenDto.cs
│   ├── AddDetalleOrdenDto.cs
│   ├── DetalleOrdenDto.cs
│   └── UpdateOrdenTotalDto.cs
└── Controllers/
    ├── OrdeneController.cs
    └── DetallesController.cs
```

---

## Próximos Pasos (Historas de Usuario 1-3)

Para implementar las historias restantes, necesitarás:

1. **PlatoService** - Servicio para gestionar platos
   - HU1: Registrar un plato
   - HU2: Clasificar un plato por categoría
   - HU3: Actualizar precios

2. **PlatoController** - Controlador REST para platos

3. **CategoriaService** - Servicio para gestionar categorías (si es necesario)

---

## Notas de Implementación

- Los servicios utilizan **inyección de dependencias** registrados en `Program.cs`
- Todos los endpoints usan **async/await** y soportan `CancellationToken`
- La lógica de negocio está en los **servicios**, no en los controladores
- Los DTOs separan los datos de entrada/salida de las entidades del dominio
