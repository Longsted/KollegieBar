namespace BusinessLogic.Mappers
{
    internal class UserMapper
    {
        public static DataTransferObject.Model.UserDataTransferObject Map(Data.Model.User user)
        {
            return new DataTransferObject.Model.UserDataTransferObject
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                RoleDataTransferObject = (DataTransferObject.Model.UserRoleDataTransferObject)user.Role
            };
        }

        public static Data.Model.User Map(DataTransferObject.Model.UserDataTransferObject userDataTransferObject)
        {
            return new Data.Model.User
            {
                Id = userDataTransferObject.Id,
                UserName = userDataTransferObject.UserName,
                Password = userDataTransferObject.Password,
                Role = (Data.Model.UserRole)userDataTransferObject.RoleDataTransferObject
            };
        }
    }
}