using TaskMaster.Entities.Enums;

namespace TaskMaster.Entities.DTOs;

public record UserDto(Guid Id, string Email, string Username, string FullName, string PhoneNumber, string ProfileUrl, string Role);