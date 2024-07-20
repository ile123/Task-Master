using Api.Data;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Enums;
using Task = System.Threading.Tasks.Task;

namespace Api.Repositories.Implementations;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllUsers(string keyword, string sortBy, int pageNumber, int pageSize)
    {
        var query = dbContext.Users.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.UserName.Contains(keyword) ||
                                     x.FullName.Contains(keyword) ||
                                     x.Email.Contains(keyword) ||
                                     x.PhoneNumber.Contains(keyword));
        }
        query = sortBy switch
        {
            "id" => sortBy == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id),
            "email" => sortBy == "asc" ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
            "username" => sortBy == "asc" ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName),
            "fullName" => sortBy == "asc" ? query.OrderBy(u => u.FullName) : query.OrderByDescending(u => u.FullName),
            "phoneNumber" => sortBy == "asc"
                ? query.OrderBy(u => u.PhoneNumber)
                : query.OrderByDescending(u => u.PhoneNumber),
            _ => query
        };
        var users = await query
            .Skip(pageNumber)
            .Take(pageSize)
            .ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == phoneNumber.ToLower());
    }

    public async Task<bool> DoesAdminExist()
    {
        return await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Role == Role.Admin) is not null;
    }

    public async Task AddUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid id)
    {
        var userToDelete = await dbContext.Users.FindAsync(id);
        if (userToDelete == null) return;
        dbContext.Users.Remove(userToDelete);
        await dbContext.SaveChangesAsync();
    }
}