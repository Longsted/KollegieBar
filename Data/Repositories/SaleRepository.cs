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
            .FirstOrDefaultAsync(s => s.SaleId == id);
    }
    
    public  async Task<List<Sale>> GetAllWithRelationsAsync()
    {
        return await _context.Sales.Include(p => p.Product)
            .Include(s => s.Drink).ToListAsync();
    }

    public async Task<List<Sale>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
    {
        return await  _context.Sales.Where(p => p.SaleDate >= startDate && p.SaleDate < endDate).Include(p => p.Product)
            .Include(s => s.Drink).ToListAsync();
    }
}