using Microsoft.AspNetCore.Mvc;
using TaskMaster.Entities.DTOs;

namespace TaskMaster.Services.Interfaces;

public interface IAuthService
{
    Task<ResultResponseDto<string>> Register(UserRegisterDto authDto);
    Task<ResultResponseDto<string>> Login(UserAuthDto authDto);
    Task InitializeAdmin();
    string CreateToken(UserDto userDto);
}