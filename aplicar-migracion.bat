@REM Script para ejecutar la migración de la base de datos
@REM Asegúrate de estar en la carpeta raíz del proyecto

@echo off
echo.
echo ==========================================
echo Aplicando migraciones a la base de datos
echo ==========================================
echo.

@REM Cambiar a la carpeta del proyecto
cd /d "%~dp0"

@REM Ejecutar la migración
dotnet ef database update

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ==========================================
    echo ✓ Migración completada exitosamente
    echo ==========================================
    echo.
) else (
    echo.
    echo ==========================================
    echo ✗ Error al aplicar la migración
    echo ==========================================
    echo.
)

pause
