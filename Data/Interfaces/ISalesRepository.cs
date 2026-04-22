using Data.Model;

namespace Data.Interfaces;

public interface ISalesRepository
{
    Task AddRangeAsync(List<Sale> sales);
    
    Task<Sale?> GetAsync(int id);
}