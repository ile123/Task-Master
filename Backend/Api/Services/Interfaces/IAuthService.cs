using Model.Dtos;
using Model.Generics;
using Task = System.Threading.Tasks.Task;

namespace Api.Services.Interfaces;

public interface IAuthService
{
    Task<ResultResponseDto<string>> Register(UserRegisterDto authDto);
    Task<ResultResponseDto<LoginSuccessDto>> Login(UserAuthDto authDto);
    Task InitializeAdmin();
    Task<ResultResponseDto<UserDto?>> GetUserByToken(string jwtToken);
    string CreateToken(UserDto userDto);
}