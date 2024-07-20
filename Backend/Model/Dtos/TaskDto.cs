using Model.Enums;

namespace Model.Dtos;

public record TaskDto(Guid Id, string Title, string Description, string Tags, Priority Priority, Status Status, DateTime DueDate);