namespace TaskMaster.Entities.DTOs;

public record UserRegisterDto(string Email, string Password, string Username, string FullName, string PhoneNumber, string ProfileUrl);