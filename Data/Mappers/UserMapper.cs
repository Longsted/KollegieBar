using DataTransferObject.Model;

namespace Data.Mappers
{
    
    internal class UserMapper
    {
        public static DataTransferObject.Model.User Map(User user)
        {
            return new DataTransferObject.Model.User(user.Id, user.UserName);
        }


        public static User MapBack(DataTransferObject.Model.User user)
        {
            return  new User(user.Id, user.UserName);
        }
    }
}
