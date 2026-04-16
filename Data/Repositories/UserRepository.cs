using Data.Context;
using Data.Mappers;

//tet
namespace Data.Repositories;

public class UserRepository
{

    private readonly AppDbContext _context = new AppDbContextFactory().CreateDbContext(Array.Empty<string>());

    public UserRepository()
    {

    }

    public virtual DataTransferObject.Model.User? GetUser(int id)
    {
        var user = _context.Users.Find(id);


        if (user == null)
        {
            return null;
        }

        return UserMapper.Map(user);
    }

    public virtual List<DataTransferObject.Model.User> GetUsers()
    {
        return _context.Users
            .Select(u => UserMapper.Map(u))
            .ToList();
    }

    public virtual void AddUser(DataTransferObject.Model.User user)
    {
        _context.Users.Add(UserMapper.Map(user));
        _context.SaveChanges();
    }

    public virtual void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public virtual DataTransferObject.Model.User CreateUser(DataTransferObject.Model.User user)
    {
        var entityUser = UserMapper.Map(user);

        _context.Users.Add(entityUser);
        _context.SaveChanges();

        return UserMapper.Map(entityUser);
    }

    public virtual void UpdateUser(DataTransferObject.Model.User user)
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