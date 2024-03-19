using TaskMaster.Entities.Enums;

namespace TaskMaster.Entities.DTOs;

public record LoginSuccessDto(Guid UserId, string Username, string Role, string JwtToken);