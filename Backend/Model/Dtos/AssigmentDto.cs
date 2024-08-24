using Model.Enums;

namespace Model.Dtos;

public record AssigmentDto(Guid Id, string Title, string Description, string Tags, Priority Priority, Status Status, DateTime DueDate, string Username);