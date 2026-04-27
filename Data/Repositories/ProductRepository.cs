using Data.Context;
using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }


    public Task DeleteAsync(Product product)
    {

        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }

 
    public async Task<List<Product>> GetWhereAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _context.Products.Where(predicate).ToListAsync();
    }
}