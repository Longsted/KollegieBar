using Data.Model;
namespace Data.Interfaces;

public interface IUserRepository
{
    Task<User?>GetByIdAsync(int id);
    
    Task<List<User>> GetAllAsync();
    
    Task AddAsync(User user);
    
    Task DeleteAsync(User user);
    
    
    
}