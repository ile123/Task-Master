using Microsoft.AspNetCore.Mvc;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskMaster.Services.Interfaces;

public interface IAuthService
{
    Task<ResultResponseDto<string>> Register(UserRegisterDto authDto);
    Task<ResultResponseDto<LoginSuccessDto>> Login(UserAuthDto authDto);
    Task InitializeAdmin();
    Task<ResultResponseDto<UserDto?>> GetUserByToken(string jwtToken);
    string CreateToken(UserDto userDto);
}