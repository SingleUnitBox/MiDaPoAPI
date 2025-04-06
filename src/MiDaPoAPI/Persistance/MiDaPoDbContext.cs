using Microsoft.EntityFrameworkCore;
using MiDaPoAPI.Models;

namespace MiDaPoAPI.Persistance;

public class MiDaPoDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Follow> Follows { get; set; }

    public MiDaPoDbContext(DbContextOptions<MiDaPoDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        modelBuilder.HasDefaultSchema("socialmedia");
    }
}