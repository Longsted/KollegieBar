using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    

    public class UserStory15
    {
        // Tests that a valid user can log in and receives the correct role.
        [Fact]
        public void ValidUser_ShouldLogin_AndReceiveCorrectRole()
        {
            var accounts = new Dictionary<string, UserRole>
            {
                { "bartender1", UserRole.Bartender },
                { "board1", UserRole.BoardMember }
            };

            var service = new LoginService(accounts);

            var user = service.Login("board1");

            Assert.NotNull(user);
            Assert.Equal(UserRole.BoardMember, user.Role);
        }

        // Tests that an invalid user cannot log in and receives no access.

        [Fact]
        public void InvalidUser_ShouldNotLogin()
        {
            var accounts = new Dictionary<string, UserRole>
            {
                { "bartender1", UserRole.Bartender }
            };

            var service = new LoginService(accounts);

            var user = service.Login("unknownUser");

            Assert.Null(user);
        }
    }
}
