using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Generics;

namespace Api.Controllers;

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
    [Authorize(Policy = "AdminRequired")]
    public async Task<ActionResult<ResultResponseDto<string>>> GetAllUsers(
        [FromQuery] string keyword = "",
        [FromQuery] string sortBy = "",
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _userService.GetAllUsers(keyword, sortBy, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpGet("info/{id:guid}")]
    [Authorize(Policy = "AnyRoleRequired")]
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
    [Authorize(Policy = "AnyRoleRequired")]
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
    [Authorize(Policy = "AnyRoleRequired")]
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
    [Authorize(Policy = "AnyRoleRequired")]
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
    [Authorize(Policy = "AdminRequired")]
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