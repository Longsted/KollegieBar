namespace DataTransferObject.Model
{
    public class UserDataTransferObject
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRoleDataTransferObject RoleDataTransferObject { get; set; }

        public UserDataTransferObject(string username, string password, UserRoleDataTransferObject roleDataTransferObject)
        {
            UserName = username;
            Password = password;
            RoleDataTransferObject = roleDataTransferObject;
        }

        public UserDataTransferObject()
        {
        }
    }
}

