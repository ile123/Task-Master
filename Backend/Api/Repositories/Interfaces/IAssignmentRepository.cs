using Model.Entities;

namespace Api.Repositories.Interfaces;

public interface IAssignmentRepository 
{
    Task<IEnumerable<Assignment>> GetAllAssignments(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize);
    Task<IEnumerable<Assignment>> GetAllAssignmentsByUser(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize, string username);
    Task<Assignment?> GetAssignmentById(Guid id);
    Task AddAssignment(Assignment assignment);
    Task UpdateAssignment(Assignment assignment);
    Task DeleteAssignment(Guid id);
    long GetTotalAmountOfAssignments();
}