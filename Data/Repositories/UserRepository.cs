using Data.Context;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = _context.Users.Find(id);

        if (id <= 0)
        {
            throw new InvalidOperationException("Id must be greater than zero.");
        }

        return await _context.Users.FindAsync(user);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task DeleteAsync(User user)
    {
        if (user == null)
        {
            throw new InvalidOperationException("User cannot be null.");
        }

        _context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}