using Data.Model;
using DataTransferObject.Model;

namespace Data.Interfaces;

public interface IDrinkRepository
{
    Task<List<Drink>> GetAllAsync();
    
    Task<Drink> GetByIdAsync(int id);

    Task<List<Drink>> GetDrinksWithIngredientsAsync(List<int> drinkIds);

    Task AddAsync(Drink drink);
    
    Task DeleteAsync(Drink drink);
    
    Task UpdateAsync(Drink drink);
}