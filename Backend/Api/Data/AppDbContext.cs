using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Api.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Assignments)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}