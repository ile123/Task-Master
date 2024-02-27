namespace TaskMaster.Entities.DTOs;

public record ResultResponseDto<T>(bool Success, string Message, T Result);