using Model.Dtos;
using Model.Entities;
using Model.Generics;

namespace Api.Services.Interfaces;

public interface IAssigmentService
{
    Task<ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>> GetAllAssignments(string keyword, string sortBy, string sortDirection, int pageNumber,
        int pageSize);

    Task<ResultResponseDto<PaginationDto<IEnumerable<AssigmentDto>>>> GetAllAssignmentsByUser(string keyword, string sortBy, string sortDirection,
        int pageNumber, int pageSize, string username);

    Task<ResultResponseDto<AssigmentDto>> GetAssignmentById(Guid id);
    Task<ResultResponseDto<string>> AddAssignment(CreateAssigmentDto createAssigmentDto);
    Task<ResultResponseDto<string>> UpdateAssignment(Guid id, CreateAssigmentDto updateAssigmentDto);
    Task ChangeAssigmentStatus(Guid id);
    Task<ResultResponseDto<string>> DeleteAssignment(Guid id);
}