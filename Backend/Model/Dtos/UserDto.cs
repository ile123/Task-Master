namespace Model.Dtos;

public record UserDto(Guid Id, string Email, string Username, string FullName, string PhoneNumber, string ProfileUrl, string Role);