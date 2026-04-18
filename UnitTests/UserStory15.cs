using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;
using EntityUserRole = Data.Model.UserRole;
using DtoUserRole = DataTransferObject.Model.UserRole;

namespace UnitTests
{
    public class UserStory15
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UserBusinessLogicLayer _service;

        public UserStory15()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockUserRepo = new Mock<IUserRepository>();

            _mockUow.Setup(u => u.Users).Returns(_mockUserRepo.Object);

            _service = new UserBusinessLogicLayer(_mockUow.Object);
        }

        [Fact]
        public async Task ValidUser_ShouldLogin_AndReceiveCorrectRole()
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "board1",
                    Password = "1234",
                    Role = EntityUserRole.BoardMember
                }
            };

            _mockUserRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var result = await _service.CheckUserAsync("1234", "board1");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task InvalidUser_ShouldNotLogin()
        {
            _mockUserRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var result = await _service.CheckUserAsync("wrong", "user");

            Assert.Equal(0, result);
        }
    }
}