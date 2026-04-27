using Data.Context;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class SaleRepository : ISalesRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(List<Sale> sales)
    {
        await _context.Sales.AddRangeAsync(sales);
    }
    public async Task<Sale?> GetAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Drink)
            .ThenInclude(d => d.Ingredients)
            .FirstOrDefaultAsync(s => s.DrinkId == id);
    }
}