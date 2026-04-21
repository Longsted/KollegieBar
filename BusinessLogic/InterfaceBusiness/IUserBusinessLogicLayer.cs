using DataTransferObject.Model;

namespace BusinessLogic.InterfaceBusiness;

public interface IUserBusinessLogicLayer
{
    Task<UserDataTransferObject?> GetUserByIdAsync(int userId);
    
    Task<List<UserDataTransferObject>> GetUsersAsync();

    Task<int> CheckUserAsync(string password, string username);
}