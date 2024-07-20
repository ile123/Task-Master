using Model.Entities;
using Task = System.Threading.Tasks.Task;

namespace Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers(string keyword, string sortBy, int pageNumber, int pageSize);
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByPhoneNumber(string phoneNumber);
    Task<bool> DoesAdminExist();
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(Guid id);
}