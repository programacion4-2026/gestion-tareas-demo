# 🚀 Guía para Empezar a Usar la API

## Pre-requisitos

- ✅ .NET SDK 6.0 o superior (`dotnet --version`)
- ✅ Entity Framework CLI (`dotnet tool install --global dotnet-ef`)
- ✅ SQL Server o SQLite (según tu configuración)

Si necesitas instalar EF CLI:
```bash
dotnet tool install --global dotnet-ef
```

---

## 1️⃣ Ejecutar la Migración

Esta es la tarea más importante. La migración crea la estructura de la base de datos.

### Opción A: Usar Script PowerShell (Recomendado para Windows)
```powershell
.\aplicar-migracion.ps1
```

### Opción B: Usar Script Batch
```cmd
.\aplicar-migracion.bat
```

### Opción C: Comando Manual
```bash
dotnet ef database update
```

**Resultado esperado:**
```
Build started...
Applying migration '20260310202051_TablasCompletas'.
Done.
```

---

## 2️⃣ Iniciar la Aplicación

```bash
dotnet run
```

Deberías ver algo como:
```
Now listening on: https://localhost:7101
Now listening on: http://localhost:5087
Application started. Press Ctrl+C to exit.
```

---

## 3️⃣ Probar la API

### Opción A: Usar Postman (Recomendado)

1. **Abre Postman**
2. **Importa la colección:**
   - Ve a `File` → `Import`
   - Selecciona: `Sistema_Gestion_Restaurante.postman_collection.json`
   - La colección se importará con todas las historias de usuario

3. **Ejecuta los requests en orden:**
   - Primero: "Crear una nueva orden" (HU4)
   - El ID se guardará automáticamente
   - Luego: "Agregar plato a la orden" (HU5)
   - Y así sucesivamente...

### Opción B: Usar REST Client en VS Code

1. **Instala la extensión:**
   - Open VS Code
   - Extensions → Busca "REST Client"
   - Instala por Huachao Mao

2. **Abre el archivo:**
   - `API/Requests/Sistema_Gestion_Restaurante.http`

3. **Ejecuta los requests:**
   - Haz clic en "Send Request" sobre cada endpoint
   - Los resultados aparecerán en una pestaña lateral

### Opción C: Swagger UI (Interactivo)

1. **Abre en tu navegador:**
   - https://localhost:7101/swagger/index.html

2. **Prueba los endpoints interactivamente:**
   - "Try it out"
   - Rellena los parámetros
   - "Execute"

---

## 📋 Flujo de Prueba Recomendado

### 1. Crear una Orden (HU4)
```bash
POST https://localhost:7101/api/ordenes
{
  "numeroMesa": "5"
}
```
**Resultado:** Recibirás un ID de orden. Guárdalo.

### 2. Agregar Platos (HU5)
```bash
POST https://localhost:7101/api/ordenes/{ordenId}/detalles
{
  "platoId": "550e8400-e29b-41d4-a716-446655440000",
  "cantidad": 2,
  "precioUnitario": 25.50
}
```

Repite para agregar más platos.

### 3. Ver Detalles
```bash
GET https://localhost:7101/api/ordenes/{ordenId}/detalles
```

### 4. Calcular Total (HU6)
```bash
GET https://localhost:7101/api/ordenes/{ordenId}/detalles/total
```

### 5. Completar Orden (HU7)
```bash
POST https://localhost:7101/api/ordenes/{ordenId}/completar
```

---

## 🐛 Solución de Problemas

### Error: "No .NET SDKs were found"
```bash
# Instala .NET SDK:
# Windows: https://dotnet.microsoft.com/download
winget install Microsoft.DotNet.SDK.8
```

### Error: "Could not find application dotnet-ef"
```bash
# Instala Entity Framework CLI:
dotnet tool install --global dotnet-ef
```

### Error: "The certificate provider was not found"
- Confía en el certificado HTTPS:
```bash
dotnet dev-certs https --trust
```

### Error: "Cannot open database file"
- Verifica la cadena de conexión en `Config/appsettings.json`
- Asegúrate de que la carpeta `Database/` tiene permisos de escritura

---

## 📁 Archivos Importantes

| Archivo | Propósito |
|---------|-----------|
| `aplicar-migracion.ps1` | Script PowerShell para migración |
| `aplicar-migracion.bat` | Script Batch para Windows |
| `Sistema_Gestion_Restaurante.postman_collection.json` | Colección Postman |
| `API/Requests/Sistema_Gestion_Restaurante.http` | REST Client para VS Code |
| `HISTORIAS_IMPLEMENTADAS.md` | Documentación de endpoints |

---

## ✨ Características Implementadas

✅ **HU4** - Crear orden  
✅ **HU5** - Agregar platos  
✅ **HU6** - Calcular total automáticamente  
✅ **HU7** - Cerrar orden  

---

## 📞 ¿Necesitas Ayuda?

- Verifica que la aplicación está corriendo: `https://localhost:7101/api/ordenes`
- Revisa `HISTORIAS_IMPLEMENTADAS.md` para ejemplos completos
- Consulta los logs en la terminal para más detalles

---

**¡Listo para empezar!** 🎉
