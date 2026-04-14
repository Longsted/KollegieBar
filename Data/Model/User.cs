namespace Data.Model
{
   public class User
   {
       public int Id { get; set; }
       public string UserName { get; set; } = "";
       public string Password { get; set; } = "";
       public UserRole Role { get; set; }

       public User()
       {
           
       }

       public User(int id, string userName, string password, UserRole role)
       {
           Id = id;
           UserName = userName;
           Password = password;
           Role = role;
       }
       
   } 
}

