using DataTransferObject.Model;

namespace Data.Mappers
{
    
    internal class UserMapper
    {
        public static DataTransferObject.Model.User Map(Data.Model.User user)
        {
            return new DataTransferObject.Model.User
            {
                UserName = user.UserName,
                Password = user.Password,
                Role = (DataTransferObject.Model.UserRoles)user.Role
            };
        }

        public static Data.Model.User Map(DataTransferObject.Model.User user)
        {
            return new Data.Model.User
            {
                UserName = user.UserName,
                Password = user.Password,
                Role = (Data.Model.UserRole)user.Role
            };
        }
    }
}
