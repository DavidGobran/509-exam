using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;

namespace atm.Tests.Interfaces
{
    [TestClass]
    public class IUserServiceTests
    {
        private Mock<IUserService> _userServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var username = "testuser";
            var pinCode = "1234";
            var user = new User(username, "Customer", pinCode);
            _userServiceMock.Setup(service => service.Login(username, pinCode)).Returns(user);

            // Act
            var result = _userServiceMock.Object.Login(username, pinCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(username, result.Username);
            Assert.AreEqual("Customer", result.UserType);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var username = "testuser";
            var pinCode = "wrongpin";
            _userServiceMock.Setup(service => service.Login(username, pinCode)).Returns((User)null);

            // Act
            var result = _userServiceMock.Object.Login(username, pinCode);

            // Assert
            Assert.IsNull(result);
        }
    }
}
