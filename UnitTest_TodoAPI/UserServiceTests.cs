using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoAPI.Model;
using TodoAPI.Services;
using TodoAPI.ViewModel;
namespace UnitTest_TodoAPI
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<TodoDbContext> _mockDbContext;
        private UserService _userService;
        public UserServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockDbContext = new Mock<TodoDbContext>();
            _userService = new UserService(_mockDbContext.Object, _mockMapper.Object);
        }
        private Mock<DbSet<User>> SetupMockDbSet(IEnumerable<User> users)
        {
            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            return mockDbSet;
        }
        private void SetupDbContextWithUsers(IEnumerable<User> users)
        {
            _mockDbContext.Setup(db => db.Users).Returns(SetupMockDbSet(users).Object);
        }

        [TestMethod]
        public void AddUser_WhenUserDoesNotExist_Success()
        {
            //Arrange
            UserData user = new();

            SetupDbContextWithUsers(new List<User>());

            //Act
            var result = _userService.AddUser(user);
            
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddUser_WhenUserDoesNotExist_Failure()
        {
            //Arrange
            var existingUser = new User { Username = "MuhilTheHacker" };
            var user = new UserData { Username = "MuhilTheHacker" };

            SetupDbContextWithUsers(new List<User> { existingUser });

            //Act
            var result = _userService.AddUser(user);

            //Assert
            Assert.IsFalse(result);
            _mockDbContext.Verify(db => db.Add(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        public void GetUser_Success_UserExists()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            var existingUser = new User { Username = username, Password = password };

            SetupDbContextWithUsers(new List<User> { existingUser });

            // Act
            var result = _userService.GetUser(username, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUser_Failure_UserDoesNotExists()
        {
            // Arrange
            var username = "nonExistingUser";
            var password = "nonExistingPassword";

            SetupDbContextWithUsers(new List<User>());

            // Act
            var result = _userService.GetUser(username, password);

            // Assert
            Assert.IsFalse(result);
        }
    }
}