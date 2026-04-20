namespace BusinessLogic.Mappers
{
    internal class UserMapper
    {
        public static DataTransferObject.Model.UserDto Map(Data.Model.User user)
        {
            return new DataTransferObject.Model.UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Role = (DataTransferObject.Model.UserRole)user.Role
            };
        }

        public static Data.Model.User Map(DataTransferObject.Model.UserDto userDto)
        {
            return new Data.Model.User
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Role = (Data.Model.UserRole)userDto.Role
            };
        }
    }
}