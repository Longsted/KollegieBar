using Data.Context;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly AppDbContext _context;

    public DrinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Drink>> GetAllAsync()
    {
        return await _context.Drinks
            .Include(d => d.Ingredients)
            .ToListAsync();
    }

    public async Task<Drink?> GetByIdAsync(int id)
    {
        
        return await _context.Drinks
            .Include(d => d.Ingredients)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Drink drink)
    {
        
        await _context.Drinks.AddAsync(drink);
    }

    public Task DeleteAsync(Drink drink)
    {
        _context.Drinks.Remove(drink);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Drink drink)
    {
        _context.Drinks.Update(drink);
        return Task.CompletedTask;
    }
}