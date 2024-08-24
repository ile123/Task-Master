namespace Model.Dtos;

public record CreateAssigmentDto(string Title, string Description, string Tags, string Priority, string Status, string DueDate, string Username);