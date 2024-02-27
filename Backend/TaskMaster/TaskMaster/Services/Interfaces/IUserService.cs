using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.Models;
using Task = TaskMaster.Entities.Models.Task;

namespace TaskMaster.Services.Interfaces;

public interface IUserService
{
    Task<ResultResponseDto<IEnumerable<UserDto>>> GetAllUsers();
    Task<ResultResponseDto<IEnumerable<TaskDto>>> GetAllUserTasks(Guid id);
    Task<ResultResponseDto<UserDto>> GetUser(Guid id);
    Task<ResultResponseDto<string>> UpdateUser(Guid id, UpdateUserDto userDto);
    Task<ResultResponseDto<string>> UpdatePassword(Guid id, ChangePasswordDto changePasswordDto);
    Task<ResultResponseDto<string>> DeleteUser(Guid id);
}