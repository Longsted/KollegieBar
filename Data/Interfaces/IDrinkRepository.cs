using Data.Model;

namespace Data.Interfaces;

public interface IDrinkRepository
{
    Task<List<Drink>> GetAllAsync();
    
    Task<Drink> GetByIdAsync(int id);
    
    Task AddAsync(Drink drink);
    
    Task DeleteAsync(Drink drink);
    
    Task UpdateAsync(Drink drink);
}