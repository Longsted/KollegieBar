namespace DataTransferObject.Model
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public UserDto(string username, string password, UserRole role)
        {
            UserName = username;
            Password = password;
            Role = role;
        }

        public UserDto()
        {
        }
    }
}

