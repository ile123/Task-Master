using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Generics;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpGet("get-by-token")]
    public async Task<ActionResult<ResultResponseDto<UserDto?>>> GetUserByToken()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString()
            .Replace("Bearer ", "");
        if (token == "")
        {
            return BadRequest(new ResultResponseDto<UserDto?>(false, "User with given JWT not found.",
                new UserDto(Guid.Empty, "", "", "", "", "", "")));
        }
        var response = await authService.GetUserByToken(token);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResultResponseDto<string>>> Register(UserRegisterDto request)
    {
        try
        {
            var result = await authService.Register(request);
            if (result.Message is "Email already in use" or "Phone number already in use")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, "ERROR: Registration attempt failed!", exception.Message));
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ResultResponseDto<string>>> Login(UserAuthDto request)
    {
        try
        {
            var response = await authService.Login(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new ResultResponseDto<string>(false, "ERROR: Login attempt failed!", ""));
        }
    }

    [HttpPost("initialize-admin")]
    public async Task InitializeAdmin()
    {
        await authService.InitializeAdmin();
    }
}