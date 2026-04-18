using DataTransferObject.Model;

namespace BusinessLogic.InterfaceBusiness;

public interface IUserBusinessLogicLayer
{
    Task<UserDto?> GetUserByIdAsync(int userId);
    
    Task<List<UserDto>> GetUsersAsync();

    Task<int> CheckUserAsync(string password, string username);
}