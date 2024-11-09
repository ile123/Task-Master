using Api.Repositories.Implementations;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.OpenApi.Extensions;
using Model.Dtos;
using Model.Entities;
using Model.Enums;
using Model.Generics;

namespace Api.Services.Implementations;

public class AssigmentService(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IMapper mapper) : IAssigmentService
{
    
    public async Task<ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>> GetAllAssignments(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize)
    {
        var assignments =
            await assignmentRepository.GetAllAssignments(keyword, sortBy, sortDirection, pageNumber, pageSize);
        return new ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>(true, "All assignments found!",
            new PaginationDto<IEnumerable<AssigmentDto>>(assignments.Select(mapper.Map<AssigmentDto>).ToList(),
                assignmentRepository.GetTotalAmountOfAssignments()));
    }

    public async Task<ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>> GetAllAssignmentsByUser(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize,
        string username)
    {
        var assignments =
            await assignmentRepository.GetAllAssignmentsByUser(keyword, sortBy, sortDirection, pageNumber, pageSize, username);
        return new ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>(true, "All user assignments found!",
            new PaginationDto<IEnumerable<AssigmentDto>>(assignments.Select(mapper.Map<AssigmentDto>).ToList(),
                assignmentRepository.GetTotalAmountOfAssignments()));
    }

    public async Task<ResultResponseDto<AssigmentDto>> GetAssignmentById(Guid id)
    {
        var assigment = await assignmentRepository.GetAssignmentById(id);
        return new ResultResponseDto<AssigmentDto>(true, "Assigment found successfully",
            mapper.Map<AssigmentDto>(assigment));
    }

    public async Task<ResultResponseDto<string>> AddAssignment(CreateAssigmentDto createAssigmentDto)
    {
        var user = await userRepository.GetUserByUsername(createAssigmentDto.Username);
        if (user is null)
            return new ResultResponseDto<string>(false, "ERROR: Could not save assignment.", "User not found.");

        var priority = createAssigmentDto.Priority switch
        {
            "Low" => Priority.Low,
            "Medium" => Priority.Medium,
            "High" => Priority.High,
            _ => Priority.Low
        };

        var assignment = new Assignment
        {
            Title = createAssigmentDto.Title,
            Description = createAssigmentDto.Description,
            Tags = createAssigmentDto.Tags,
            Priority = priority,
            Status = Status.Todo,
            DueDate = createAssigmentDto.DueDate,
        };

        assignment.User = user;
        
        user.Assignments.Add(assignment);
        
        await assignmentRepository.AddAssignment(assignment);

        return new ResultResponseDto<string>(true, "Assignment added.", "Assignment successfully added.");
    }


    public async Task<ResultResponseDto<string>> UpdateAssignment(Guid id, CreateAssigmentDto updateAssigmentDto)
    {
        var user = await userRepository.GetUserByUsername(updateAssigmentDto.Username);
        if (user is null)
            return new ResultResponseDto<string>(false, "ERROR: Could not save assignment.", "User not found.");
        
        var assignment = await assignmentRepository.GetAssignmentById(id);
        if (assignment is null)
            return new ResultResponseDto<string>(false, "ERROR: Could not save assignment.", "Assignment not found.");
        
        var priority = updateAssigmentDto.Priority switch
        {
            "Low" => Priority.Low,
            "Medium" => Priority.Medium,
            "High" => Priority.High,
            _ => Priority.Low
        };
        
        var status = updateAssigmentDto.Status switch
        {
            "Completed" => Status.Completed,
            "Ongoing" => Status.Ongoing,
            "Todo" => Status.Todo,
            _ => Status.Todo
        };
        
        if (assignment.User != user)
        {
            var previousUser = assignment.User;
            
            if (previousUser != null)
            {
                previousUser.Assignments.Remove(assignment);
                await userRepository.UpdateUser(previousUser);
            }
            
            assignment.User = user;
            user.Assignments.Add(assignment);
            
            //await userRepository.UpdateUser(user);
        }
        
        assignment.Title = updateAssigmentDto.Title;
        assignment.Description = updateAssigmentDto.Description;
        assignment.Tags = updateAssigmentDto.Tags;
        assignment.Priority = priority;
        assignment.Status = status;
        assignment.DueDate = updateAssigmentDto.DueDate;
        
        await assignmentRepository.UpdateAssignment(assignment);
        
        return new ResultResponseDto<string>(true, "Assignment updated", "Assignment successfully updated");
    }

    public async Task ChangeAssigmentStatus(Guid id)
    {
        var assigment = await assignmentRepository.GetAssignmentById(id);
        if(assigment is null) return;
        assigment.Status = assigment.Status switch
        {
            Status.Todo => Status.Ongoing,
            Status.Ongoing => Status.Completed,
            _ => Status.Completed
        };
        await assignmentRepository.UpdateAssignment(assigment);
    }


    public async Task<ResultResponseDto<string>> DeleteAssignment(Guid id)
    {
        await assignmentRepository.DeleteAssignment(id);
        return new ResultResponseDto<string>(true, "Assigment deleted.", "Assigment successfully deleted");
    }
}