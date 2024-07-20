using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using Model.Dtos;
using Model.Enums;
using Model.Generics;

namespace Api.Services.Implementations;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<ResultResponseDto<IEnumerable<UserDto>>> GetAllUsers(string keyword, string sortBy, int pageNumber, int pageSize)
    {
        var users = await userRepository.GetAllUsers(keyword, sortBy, pageNumber, pageSize);
        return new ResultResponseDto<IEnumerable<UserDto>>(true, "All users found!", users.Select(x => mapper.Map<UserDto>(x)).ToList());
    }

    public async Task<ResultResponseDto<IEnumerable<TaskDto>>> GetAllUserTasks(Guid id)
    {
        try
        {
            var user = await userRepository.GetUserById(id);
            if (user is null) throw new Exception("ERROR: User with given id not found -> " + id);
            return new ResultResponseDto<IEnumerable<TaskDto>>(true, "User tasks retrieved!",
                user.Tasks.Select(x => mapper.Map<TaskDto>(x)).ToList());
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new ResultResponseDto<IEnumerable<TaskDto>>(true, exception.Message, new List<TaskDto>());
        }
    }

    public async Task<ResultResponseDto<UserDto>> GetUser(Guid id)
    {
        try
        {
            var user = await userRepository.GetUserById(id);
            if (user is null) throw new Exception("ERROR: User with given id not found -> " + id);
            var userDto = mapper.Map<UserDto>(user);
            return new ResultResponseDto<UserDto>(true, "User found successfully!", userDto);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new ResultResponseDto<UserDto>(false, exception.Message,
                new UserDto(Guid.Empty, "", "", "", "", "", Role.Member.ToString()));
        }
    }

    public async Task<ResultResponseDto<string>> UpdateUser(Guid id, UpdateUserDto userDto)
    {
        try
        {
            var user = await userRepository.GetUserById(id);
            if (user is null) throw new Exception("ERROR: User with given id not found -> " + id);
            user.UserName = userDto.Username;
            user.FullName = userDto.FullName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.ProfileUrl = userDto.ProfileUrl;
            await userRepository.UpdateUser(user);
            return new ResultResponseDto<string>(true, "User updated successfully!", "");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new ResultResponseDto<string>(false, exception.Message, "");
        }
    }

    public async Task<ResultResponseDto<string>> UpdatePassword(Guid id, ChangePasswordDto changePasswordDto)
    {
        try
        {
            var user = await userRepository.GetUserById(id);
            if (user is null)
                throw new Exception("ERROR: User with given id dose not exist -> " + id);
            var newPassword = BCrypt
                .Net
                .BCrypt
                .HashPassword(changePasswordDto.NewPassword);
            user.Password = newPassword;
            await userRepository.UpdateUser(user);
            return new ResultResponseDto<string>(true, "User password changed successfully!", "");
        }
        catch (Exception exception)
        {
            return new ResultResponseDto<string>(false, exception.Message, "");
        }
    }

    public async Task<ResultResponseDto<string>> DeleteUser(Guid id)
    {
        try
        {
            await userRepository.DeleteUser(id);
            return new ResultResponseDto<string>(true, "User deleted successfully!", "");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new ResultResponseDto<string>(false, "ERROR: Server error!", "");
        }
    }
}