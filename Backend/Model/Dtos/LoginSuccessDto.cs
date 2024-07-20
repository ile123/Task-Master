namespace Model.Dtos;

public record LoginSuccessDto(Guid UserId, string Username, string Role, string JwtToken);