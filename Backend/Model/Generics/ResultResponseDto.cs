namespace Model.Generics;

public record ResultResponseDto<T>(bool Success, string Message, T Result);