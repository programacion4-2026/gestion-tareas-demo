namespace Sistema_Gestion_Restaurante.Application.Common.Models;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    // Método para cuando todo sale bien
    public static OperationResult<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    // Método para cuando hay un error (ej: Nombre repetido)
    public static OperationResult<T> Failure(string message) => new()
    {
        IsSuccess = false,
        ErrorMessage = message
    };
}