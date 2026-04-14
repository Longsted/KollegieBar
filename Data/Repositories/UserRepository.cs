using Data.Context;
using Data.Mappers;

namespace Data.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public DataTransferObject.Model.User? GetUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            return null;
        }

        return UserMapper.Map(user);
    }

    public List<DataTransferObject.Model.User> GetUsers()
    {
        return _context.Users
            .Select(u => UserMapper.Map(u))
            .ToList();
    }

    public void AddUser(DataTransferObject.Model.User user)
    {
        _context.Users.Add(UserMapper.Map(user));
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public DataTransferObject.Model.User CreateUser(DataTransferObject.Model.User user)
    {
        var entityUser = UserMapper.Map(user);

        _context.Users.Add(entityUser);
        _context.SaveChanges();

        return UserMapper.Map(entityUser);
    }

    public void UpdateUser(DataTransferObject.Model.User user)
    {
        var existingUser = _context.Users.Find(user.Id);

        if (existingUser != null)
        {
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Role = (Data.Model.UserRole)user.Role;

            _context.SaveChanges();
        }
    }
}