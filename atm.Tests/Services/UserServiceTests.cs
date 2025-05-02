using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;
using atm.Services;

namespace atm.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var username = "testuser";
            var pinCode = "1234";
            var user = new User(username, "Customer", pinCode);
            _userRepositoryMock.Setup(repo => repo.GetUser(username, pinCode)).Returns(user);

            // Act
            var result = _userService.Login(username, pinCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(username, result.Username);
            Assert.AreEqual("Customer", result.UserType);
            _userRepositoryMock.Verify(repo => repo.GetUser(username, pinCode), Times.Once);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var username = "testuser";
            var pinCode = "wrongpin";
            _userRepositoryMock.Setup(repo => repo.GetUser(username, pinCode)).Returns((User)null);

            // Act
            var result = _userService.Login(username, pinCode);

            // Assert
            Assert.IsNull(result);
            _userRepositoryMock.Verify(repo => repo.GetUser(username, pinCode), Times.Once);
        }
    }
}