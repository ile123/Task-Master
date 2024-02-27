using Microsoft.AspNetCore.Mvc;
using TaskMaster.Entities.DTOs;
using TaskMaster.Services.Interfaces;

namespace TaskMaster.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResultResponseDto<string>>> Register(UserRegisterDto request)
    {
        try
        {
            var result = await _authService.Register(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, "ERROR: Registration attempt failed!", ""));
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ResultResponseDto<string>>> Login(UserAuthDto request)
    {
        try
        {
            var response = await _authService.Login(request);
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
        //In Angular, after the app starts, send a request that will hit this endpoint
        await _authService.InitializeAdmin();
    }
}