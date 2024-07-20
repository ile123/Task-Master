using Model.Dtos;
using Model.Generics;

namespace Api.Services.Interfaces;

public interface IUserService
{
    Task<ResultResponseDto<IEnumerable<UserDto>>> GetAllUsers(string keyword, string sortBy, int pageNumber, int pageSize);
    Task<ResultResponseDto<IEnumerable<TaskDto>>> GetAllUserTasks(Guid id);
    Task<ResultResponseDto<UserDto>> GetUser(Guid id);
    Task<ResultResponseDto<string>> UpdateUser(Guid id, UpdateUserDto userDto);
    Task<ResultResponseDto<string>> UpdatePassword(Guid id, ChangePasswordDto changePasswordDto);
    Task<ResultResponseDto<string>> DeleteUser(Guid id);
}