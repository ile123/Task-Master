using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TaskMaster.Entities.DTOs;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Models;
using TaskMaster.Repositories.Interfaces;
using TaskMaster.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskMaster.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<ResultResponseDto<string>> Register(UserRegisterDto registerDto)
    {
        if (await _userRepository.GetUserByEmail(registerDto.Email) is not null)
        {
            return new ResultResponseDto<string>(false, "Email already in use!", "");
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
        await _userRepository.AddUser(user);
        return new ResultResponseDto<string>(true, "User added successfully", "");
    }

    public async Task<ResultResponseDto<string>> Login(UserAuthDto authDto)
    {
        var user = await _userRepository.GetUserByEmail(authDto.Email);
        if (user is null)
        {
            return new ResultResponseDto<string>(false, "ERROR: User with given email dose not exist!", "");
        }
        return !BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password) 
            ? new ResultResponseDto<string>(false, "ERROR: Given password is not the same !", "") 
            : new ResultResponseDto<string>(true, "User authenticated successfully!", CreateToken(_mapper.Map<UserDto>(user)));
    }

    public string CreateToken(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userDto.Username)
        };

        var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

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
        if(await _userRepository.DoesAdminExist()) return;
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
        await _userRepository.AddUser(admin);
    }
}