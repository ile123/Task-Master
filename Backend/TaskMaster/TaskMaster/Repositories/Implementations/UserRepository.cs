using Microsoft.EntityFrameworkCore;
using TaskMaster.Data;
using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Models;
using TaskMaster.Repositories.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskMaster.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _dbContext
            .Users
            .FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == phoneNumber.ToLower());
    }

    public async Task<bool> DoesAdminExist()
    {
        return await _dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Role == Role.Admin) is not null;
    }

    public async Task AddUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid id)
    {
        var userToDelete = await _dbContext.Users.FindAsync(id);
        if (userToDelete == null) return;
        _dbContext.Users.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();
    }
}