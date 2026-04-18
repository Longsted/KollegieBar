using Data.Model;

namespace Data.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    
    Task<List<Product>> GetAllAsync();
    
    Task AddAsync(Product product);
    
    Task DeleteAsync(Product product);
    
    Task SaveChangesAsync();
}