using System.Collections.Generic;
using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{
    

    public class UserStory15
    {
        // Tests that a valid user can log in and receives the correct role.
        [Fact]
        public void ValidUser_ShouldLogin_AndReceiveCorrectRole()
        {
            var accounts = new Dictionary<string, UserRoles>
            {
                { "bartender1", UserRoles.Bartender },
                { "board1", UserRoles.BoardMember }
            };

            var service = new LoginService(accounts);

            var user = service.Login("board1");

            Assert.NotNull(user);
            Assert.Equal(UserRoles.BoardMember, user.Role);
        }

        // Tests that an invalid user cannot log in and receives no access.

        [Fact]
        public void InvalidUser_ShouldNotLogin()
        {
            var accounts = new Dictionary<string, UserRoles>
            {
                { "bartender1", UserRoles.Bartender }
            };

            var service = new LoginService(accounts);

            var user = service.Login("unknownUser");

            Assert.Null(user);
        }
    }
}
