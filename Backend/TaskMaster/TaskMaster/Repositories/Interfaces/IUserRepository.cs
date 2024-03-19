using TaskMaster.Entities.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskMaster.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
    Task<bool> DoesAdminExist();
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(Guid id);
}