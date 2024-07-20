using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Model.Dtos;
using Model.Entities;
using Model.Enums;
using Model.Generics;
using Task = System.Threading.Tasks.Task;

namespace Api.Services.Implementations;

public class AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    : IAuthService
{
    public async Task<ResultResponseDto<string>> Register(UserRegisterDto registerDto)
    {
        if (await userRepository.GetUserByEmail(registerDto.Email) is not null)
        {
            return new ResultResponseDto<string>(false, "ERROR: Email already in use!", "Email already in use");
        } 
        if (await userRepository.GetUserByPhoneNumber(registerDto.PhoneNumber) is not null)
        {
            return new ResultResponseDto<string>(false, "ERROR: Phone number already in use!", "Phone number already in use");
        }
        var user = new User
        {
            Email = registerDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            UserName = registerDto.Username,
            FullName = registerDto.FullName,
            PhoneNumber = registerDto.PhoneNumber,
            ProfileUrl = registerDto.ProfileUrl,
            Role = Role.Member
        };
        await userRepository.AddUser(user);
        return new ResultResponseDto<string>(true, "User added successfully", "");
    }

    public async Task<ResultResponseDto<LoginSuccessDto>> Login(UserAuthDto authDto)
    {
        var user = await userRepository.GetUserByEmail(authDto.Email);
        if (user is null)
        {
            return new ResultResponseDto<LoginSuccessDto>(false, "ERROR: User with given email dose not exist!", new LoginSuccessDto(Guid.Empty, "", Role.Member.ToString(), ""));
        }
        return !BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password) 
            ? new ResultResponseDto<LoginSuccessDto>(false, "ERROR: Given password is not the same !", new LoginSuccessDto(Guid.Empty, "", Role.Member.ToString(), ""))
            : new ResultResponseDto<LoginSuccessDto>(true, "User authenticated successfully!", new LoginSuccessDto(user.Id, user.UserName, user.Role.ToString(), CreateToken(mapper.Map<UserDto>(user))));
    }

    public async Task<ResultResponseDto<UserDto?>> GetUserByToken(string jwtToken)
    {
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        var nameId = decodedToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
        if (nameId is null)
            return new ResultResponseDto<UserDto?>(false, "NameId in JWT Token was not found.",
                new UserDto(Guid.Empty, "", "", "", "", "", ""));
        var user = await userRepository.GetUserById(Guid.Parse(nameId));
        return new ResultResponseDto<UserDto?>(true, "User retried successfully", mapper.Map<UserDto>(user));

    }

    public string CreateToken(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.Role, userDto.Role)
        };

        var appSettingsToken = configuration.GetSection("AppSettings:Token").Value;

        if (appSettingsToken is null) throw new Exception("ERROR: Token is null!");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task InitializeAdmin()
    {
        if(await userRepository.DoesAdminExist()) return;
        var admin = new User
        {
            Email = "admin@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
            UserName = "Admin123",
            FullName = "Admin Admin",
            PhoneNumber = "091-111-1111",
            ProfileUrl = "https://static.vecteezy.com/system/resources/previews/000/290/610/original/administration-vector-icon.jpg",
            Role = Role.Admin
        };
        await userRepository.AddUser(admin);
    }
}