using Microsoft.EntityFrameworkCore;
using TaskMaster.Entities.Models;
using Task = TaskMaster.Entities.Models.Task;

namespace TaskMaster.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Task> Notes => Set<Task>();
}