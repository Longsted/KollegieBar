using DataTransferObject.Model;
using Xunit;

namespace UnitTests
{

    public class UserStory12
    {
        // Tests that a board member can register pant and income increases.

        [Fact]
        public void BoardMember_ShouldBeAbleTo_RegisterPant()
        {
            var user = new User { Role = UserRoles.BoardMember };
            var service = new PantService();

            var result = service.RegisterPant(user, pantAmount: 5.0m);

            Assert.True(result);
            Assert.Equal(5.0m, service.TotalPantIncome);
        }

        // Tests that a board member can reset pant and income returns to zero.

        [Fact]
        public void BoardMember_ShouldBeAbleTo_ResetPant()
        {
            var user = new User { Role = UserRoles.BoardMember };
            var service = new PantService();

            service.RegisterPant(user, 10.0m);

            var result = service.ResetPant(user);

            Assert.True(result);
            Assert.Equal(0m, service.TotalPantIncome);
        }

        // Tests that a non-board member cannot register pant and income remains unchanged.

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_RegisterPant()
        {
            var user = new User { Role = UserRoles.Bartender }; 
            var service = new PantService();

            var result = service.RegisterPant(user, pantAmount: 5.0m);

            Assert.False(result);
            Assert.Equal(0m, service.TotalPantIncome);
        }

        // Tests that a non-board member cannot reset pant and income remains changed.

        [Fact]
        public void NonBoardMember_ShouldNotBeAbleTo_ResetPant()
        {
            var user = new User { Role = UserRoles.Bartender };
            var service = new PantService();

            service.RegisterPant(new User { Role = UserRoles.BoardMember }, 10.0m);

            var result = service.ResetPant(user);

            Assert.False(result);
            Assert.Equal(10.0m, service.TotalPantIncome);
        }
    }
}
