using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Task = Model.Entities.Task;

namespace Api.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Task> Tasks => Set<Task>();
}