namespace TaskMaster.Entities.DTOs;

public record UserRegisterDto(string Email, string Username, string FullName, string PhoneNumber, string ProfileUrl, string Password);