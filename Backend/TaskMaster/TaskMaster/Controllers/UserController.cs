using Microsoft.AspNetCore.Mvc;
using TaskMaster.Entities.DTOs;
using TaskMaster.Services.Interfaces;

namespace TaskMaster.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ResultResponseDto<string>>> GetAllUsers()
    {
        try
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpGet("info/{id:guid}")]
    public async Task<ActionResult<ResultResponseDto<UserDto>>> GetUserInfo(Guid id)
    {
        try
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpGet("tasks/{id:guid}")]
    public async Task<ActionResult<ResultResponseDto<IEnumerable<TaskDto>>>> GetUserTasks(Guid id)
    {
        try
        {
            var result = await _userService.GetAllUserTasks(id);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpPut("update-user/{id:guid}")]
    public async Task<ActionResult<ResultResponseDto<string>>> UpdateUser(Guid id, UpdateUserDto userDto)
    {
        try
        {
            var result = await _userService.UpdateUser(id, userDto);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpPatch("change-password/{id:guid}")]
    public async Task<ActionResult<ResultResponseDto<string>>> ChangePassword(Guid id, ChangePasswordDto passwordDto)
    {
        try
        {
            var result = await _userService.UpdatePassword(id, passwordDto);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ResultResponseDto<string>>> DeleteUser(Guid id)
    {
        try
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }
}