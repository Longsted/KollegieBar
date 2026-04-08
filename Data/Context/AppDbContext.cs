using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { 
    }
}