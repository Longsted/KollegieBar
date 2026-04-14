using DataTransferObject;
using DataTransferObject.Model;

namespace BusinessLogic;

public class UserBusinessLogicLayer
{

    public User GetUserById(int userId)
    {
        if (userId <= 0)
        {
            throw new IndexOutOfRangeException();
        }
        
        return Data.Repositories.UserRepository.GetUser(userId);
    }

    public List<User> GetUsers()
    {
        return Data.Repositories.UserRepository.GetUsers();
    }

    public void AddUser(User user)
    {
        if (user == null) throw new NullReferenceException();
        
        Data.Repositories.UserRepository.AddUser(user);
    }

    public void DeleteUser(int userId)
    {
        if (userId <= 0)
        {
            throw new IndexOutOfRangeException();
        }
        
        Data.Repositories.UserRepository.DeleteUser(userId);
    }
    
    public void UpdateUser(User user)
    {
        if (user == null) throw new NullReferenceException();
        if (user.Id <= 0) throw new IndexOutOfRangeException();
        
        Data.Repositories.UserRepository.UpdateUser(user);
    }

    public User CreateUser(User user)
    {
        Data.Repositories.UserRepository.CreateUser(user);
        return user;
    }
    
    
}