using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Generics;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssigmentController(IAssigmentService assigmentService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminRequired")]
    public async Task<ActionResult<ResultResponseDto<string>>> GetAllAssigment(
        [FromQuery] string keyword = "",
        [FromQuery] string sortBy = "id",
        [FromQuery] string sortDirection = "asc",
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await assigmentService.GetAllAssignments(keyword, sortBy, sortDirection, pageNumber, pageSize);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }
    
    [HttpGet("assignments-by-user/{username}")]
    [Authorize(Policy = "AnyRoleRequired")]
    public async Task<ActionResult<ResultResponseDto<string>>> GetAllAssigmentByUser(
        string username,
        [FromQuery] string keyword = "",
        [FromQuery] string sortBy = "id",
        [FromQuery] string sortDirection = "asc",
        [FromQuery] int pageNumber = 0,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await assigmentService.GetAllAssignmentsByUser(keyword, sortBy, sortDirection, pageNumber, pageSize, username);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "AnyRoleRequired")]
    public async Task<ActionResult<ResultResponseDto<AssigmentDto>>> GetAssigmentById(Guid id)
    {
        try
        {
            var result = await assigmentService.GetAssignmentById(id);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminRequired")]
    public async Task<ActionResult<ResultResponseDto<string>>> AddAssigment(CreateAssigmentDto createAssigmentDto)
    {
        try
        {
            var result = await assigmentService.AddAssignment(createAssigmentDto);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }

    [HttpPost("change-assigment-status/{id:guid}")]
    [Authorize(Policy = "AnyRoleRequired")]
    public async Task ChangeAssigmentStatus(Guid id)
    {
        await assigmentService.ChangeAssigmentStatus(id);
    } 
    
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "AdminRequired")]
    public async Task<ActionResult<ResultResponseDto<string>>> UpdateAssigment(Guid id, CreateAssigmentDto createAssigmentDto)
    {
        try
        {
            var result = await assigmentService.UpdateAssignment(id, createAssigmentDto);
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
    public async Task<ActionResult<ResultResponseDto<string>>> DeleteAssigment(Guid id)
    {
        try
        {
            var result = await assigmentService.DeleteAssignment(id);
            return Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return StatusCode(500, new ResultResponseDto<string>(false, exception.Message, ""));
        }
    }
}
