namespace DataTransferObject.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        
        
        public User()
        {
        }
        
        public User(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}

