using BusinessLogic.InterfaceBusiness;
using BusinessLogic.Mappers;
using System.Linq;
using Data.UnitOfWork;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class UserBusinessLogicLayer :  IUserBusinessLogicLayer
{
    private readonly IUnitOfWork _unitOfWork;

    public UserBusinessLogicLayer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDataTransferObject?> GetUserByIdAsync(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user id");
        }

        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return UserMapper.Map(user);
    }

    public async Task<List<UserDataTransferObject>> GetUsersAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return users.Select(UserMapper.Map).ToList();
    }

    

    public async Task<int> CheckUserAsync(string password, string username)
    {
        var users = await GetUsersAsync();

        var foundUser = users.FirstOrDefault(u => u.Password == password && u.UserName == username);

        if (foundUser == null)
        {
            return 0;
        }

        return foundUser.RoleDataTransferObject switch
        {
            UserRoleDataTransferObject.BoardMember => 1,
            UserRoleDataTransferObject.Bartender => 2,
            _ => 0
        };
    }
}