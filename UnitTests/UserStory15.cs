using BusinessLogic.BusinessLogicLayer;
using Data.Interfaces;
using Data.Model;
using Data.UnitOfWork;
using Moq;
using Xunit;
using EntityUserRole = Data.Model.UserRole;

namespace UnitTests
{
    public class UserStory15
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserBusinessLogicLayer _service;

        public UserStory15()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUnitOfWork.Setup(unitOfWork => unitOfWork.Users).Returns(_mockUserRepository.Object);
            _service = new UserBusinessLogicLayer(_mockUnitOfWork.Object);
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

            _mockUserRepository.Setup(repository => repository.GetAllAsync()).ReturnsAsync(users);

            var result = await _service.CheckUserAsync("1234", "board1");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task InvalidUser_ShouldNotLogin()
        {
            _mockUserRepository.Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<User>());

            var result = await _service.CheckUserAsync("wrong", "user");

            Assert.Equal(0, result);
        }
    }
}