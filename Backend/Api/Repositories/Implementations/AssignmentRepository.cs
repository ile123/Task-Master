using Api.Data;
using Api.Repositories.Interfaces;
using Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class AssignmentRepository(AppDbContext dbContext) : IAssignmentRepository
{
    
    public async Task<IEnumerable<Assignment>> GetAllAssignments(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize)
    {
        var query = dbContext.Assignments.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Title.Contains(keyword) ||
                                     x.Description.Contains(keyword) ||
                                     x.Tags.Contains(keyword) ||
                                     x.Priority.ToString().Contains(keyword) ||
                                     x.Status.ToString().Contains(keyword));
        }

        query = sortBy switch
        {
            "id" => sortDirection == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id),
            "title" => sortDirection == "asc" ? query.OrderBy(u => u.Title) : query.OrderByDescending(u => u.Title),
            "description" => sortDirection == "asc" ? query.OrderBy(u => u.Description) : query.OrderByDescending(u => u.Description),
            "tags" => sortDirection == "asc" ? query.OrderBy(u => u.Tags) : query.OrderByDescending(u => u.Tags),
            "priority" => sortDirection == "asc"
                ? query.OrderBy(u => u.Priority.ToString())
                : query.OrderByDescending(u => u.Priority.ToString()),
            "status" => sortDirection == "asc" ? query.OrderBy(u => u.Status.ToString()) : query.OrderByDescending(u => u.Status.ToString()),
            "duedate" => sortDirection == "asc" ? query.OrderBy(u => u.DueDate.ToString()) : query.OrderByDescending(u => u.DueDate.ToString()),
            _ => query
        };

        var tasks = await query
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return tasks;
    }

    public async Task<IEnumerable<Assignment>> GetAllAssignmentsByUser(string keyword, string sortBy, string sortDirection, int pageNumber, int pageSize,
        string username)
    {
        var user = dbContext
            .Users
            .FirstOrDefaultAsync(x => x.UserName == username)
            .Result ?? null;
        if (user is null) return new List<Assignment>();
        var query = user
            .Assignments
            .AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Title.Contains(keyword) ||
                                     x.Description.Contains(keyword) ||
                                     x.Tags.Contains(keyword) ||
                                     x.Priority.ToString().Contains(keyword) ||
                                     x.Status.ToString().Contains(keyword));
        }

        query = sortBy switch
        {
            "id" => sortDirection == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id),
            "title" => sortDirection == "asc" ? query.OrderBy(u => u.Title) : query.OrderByDescending(u => u.Title),
            "description" => sortDirection == "asc" ? query.OrderBy(u => u.Description) : query.OrderByDescending(u => u.Description),
            "tags" => sortDirection == "asc" ? query.OrderBy(u => u.Tags) : query.OrderByDescending(u => u.Tags),
            "priority" => sortDirection == "asc"
                ? query.OrderBy(u => u.Priority.ToString())
                : query.OrderByDescending(u => u.Priority.ToString()),
            "status" => sortDirection == "asc" ? query.OrderBy(u => u.Status.ToString()) : query.OrderByDescending(u => u.Status.ToString()),
            "duedate" => sortDirection == "asc" ? query.OrderBy(u => u.DueDate.ToString()) : query.OrderByDescending(u => u.DueDate.ToString()),
            _ => query
        };

        var tasks = await query
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return tasks;
    }

    public async Task<Assignment?> GetAssignmentById(Guid id)
    {
        return await dbContext
            .Assignments
            .FindAsync(id);
    }

    public async Task AddAssignment(Assignment assignment)
    {
        await dbContext.Assignments.AddAsync(assignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAssignment(Assignment assignment)
    {
        dbContext.Assignments.Update(assignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAssignment(Guid id)
    {
        var taskToDelete = await dbContext.Assignments.FindAsync(id);
        if (taskToDelete == null) return;
        dbContext.Assignments.Remove(taskToDelete);
        await dbContext.SaveChangesAsync();
    }

    public long GetTotalAmountOfAssignments()
    {
        return dbContext
            .Assignments
            .Count();
    }
}