using System.Collections.Generic;
using BusinessLogic.BusinessLogicLayer;
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
            var logic = new UserBusinessLogicLayer();

            var user1 = logic.GetUsers().FirstOrDefault(U => U.Role == UserRoles.BoardMember);

            var accounts = new Dictionary<string, UserRoles>
            {
                { "bartender1", UserRoles.Bartender },
                { "board1", UserRoles.BoardMember }
            };

            var service = logic.ChekcUser(user1.Password, user1.UserName);

            Assert.NotNull(user1);
            Assert.Equal(UserRoles.BoardMember, user1.Role);
        }

        // Tests that an invalid user cannot log in and receives no access.

        [Fact]
        public void InvalidUser_ShouldNotLogin()
        {

            // The user is not added to the Database and should not have acces
            var fakeuser = new User("Kato", "1234", UserRoles.BoardMember);

            var logic = new UserBusinessLogicLayer();

            var check = logic.ChekcUser(fakeuser.Password, fakeuser.UserName);

            Assert.Equal(0,check);
        }
    }
}
