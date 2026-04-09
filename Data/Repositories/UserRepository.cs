
using Data.Context;
using Data.Mappers;
using DataTransferObject.Model;

namespace Data.Repositories;

public class UserRepository
{
    public static DataTransferObject.Model.User GetUser(int id)
    {
        using (UserContext context = new UserContext())
        {
            return UserMapper.Map(context.Users.Find(id));
        }
    }

    public static List<User> GetUsers()
    {
        using (UserContext context = new UserContext())
        {
            return context.Users.Select(UserMapper.Map).ToList();
        }
    }

    public static void AddUser(DataTransferObject.Model.User user)
    {
        using (UserContext context = new UserContext())
        {
            context.Users.Add(UserMapper.Map(user));
            context.SaveChanges();
        }
    }
    
    public static void DeleteUser(int id)
    {
        using (UserContext context = new UserContext())
        {
            var user = context.Users.Find(id);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }

    public static DataTransferObject.Model.User CreateUser(DataTransferObject.Model.User user)
    {
        using (UserContext context = new UserContext())
        {
            var entityUser = UserMapper.Map(user);
            
            context.Users.Add(entityUser);
            context.SaveChanges();
            
            return UserMapper.Map(entityUser);
        }
    }

    public static void UpdateUser(DataTransferObject.Model.User user)
    {
        using (UserContext context = new UserContext())
        {
            var existingUser = context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                
                context.SaveChanges();
            }
        }
    }
    
}