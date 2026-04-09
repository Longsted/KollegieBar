namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public UserRoles Role { get; set; }

    public User(string username, string password, UserRoles role)
    {
        UserName = username;
        Password = password;
        Role = role;
    }
    
}