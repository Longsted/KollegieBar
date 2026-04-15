using Data.Repositories;
using DataTransferObject.Model;

namespace BusinessLogic.BusinessLogicLayer;

public class UserBusinessLogicLayer
{
    private readonly UserRepository _userRepository = new UserRepository();

    public UserBusinessLogicLayer()
    {

    }

    public User? GetUserById(int userId)
    {
        if (userId <= 0)
        {
            throw new IndexOutOfRangeException();
        }

        return _userRepository.GetUser(userId);
    }

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

    public void AddUser(User user)
    {
        if (user == null)
        {
            throw new NullReferenceException();
        }

        _userRepository.AddUser(user);
    }

    public void DeleteUser(int userId)
    {
        if (userId <= 0)
        {
            throw new IndexOutOfRangeException();
        }

        _userRepository.DeleteUser(userId);
    }

    public void UpdateUser(User user)
    {
        if (user == null)
        {
            throw new NullReferenceException();
        }

        if (user.Id <= 0)
        {
            throw new IndexOutOfRangeException();
        }

        _userRepository.UpdateUser(user);
    }

    public User CreateUser(User user)
    {
        if (user == null)
        {
            throw new NullReferenceException();
        }

        return _userRepository.CreateUser(user);
    }



}