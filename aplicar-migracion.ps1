# Script PowerShell para ejecutar la migración de la base de datos
# Asegúrate de estar en la carpeta raíz del proyecto

Write-Host ""
Write-Host "=========================================="
Write-Host "Aplicando migraciones a la base de datos"
Write-Host "==========================================" -ForegroundColor Green
Write-Host ""

# Cambiar a la carpeta del proyecto
Set-Location "$PSScriptRoot"

# Ejecutar la migración
Write-Host "Ejecutando: dotnet ef database update" -ForegroundColor Cyan
dotnet ef database update

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host "✓ Migración completada exitosamente" -ForegroundColor Green
    Write-Host "==========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "La base de datos está lista para usar." -ForegroundColor Yellow
} else {
    Write-Host ""
    Write-Host "==========================================" -ForegroundColor Red
    Write-Host "✗ Error al aplicar la migración" -ForegroundColor Red
    Write-Host "==========================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Verifica que:" -ForegroundColor Yellow
    Write-Host "  - .NET SDK esté instalado (dotnet --version)" -ForegroundColor Yellow
    Write-Host "  - Entity Framework CLI esté instalado (dotnet tool install --global dotnet-ef)" -ForegroundColor Yellow
    Write-Host "  - La cadena de conexión sea correcta en Config/appsettings.json" -ForegroundColor Yellow
}

pause
