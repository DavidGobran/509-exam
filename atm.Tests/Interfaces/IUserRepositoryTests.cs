using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;

namespace atm.Tests.Interfaces
{
    [TestClass]
    public class IUserRepositoryTests
    {
        private Mock<IUserRepository> _userRepositoryMock;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var username = "testuser";
            var pinCode = "1234";
            var expectedUser = new User(username, "Customer", pinCode);
            _userRepositoryMock.Setup(repo => repo.GetUser(username, pinCode)).Returns(expectedUser);

            // Act
            var result = _userRepositoryMock.Object.GetUser(username, pinCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Username, result.Username);
            Assert.AreEqual(expectedUser.UserType, result.UserType);
            Assert.AreEqual(expectedUser.PinCode, result.PinCode);
        }

        [TestMethod]
        public void AddUser_ShouldAddUser()
        {
            // Arrange
            var user = new User("newuser", "Customer", "5678");

            // Act
            _userRepositoryMock.Object.AddUser(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.AddUser(user), Times.Once);
        }

        [TestMethod]
        public void UpdateUser_ShouldUpdateUser()
        {
            // Arrange
            var user = new User("existinguser", "Customer", "5678");

            // Act
            _userRepositoryMock.Object.UpdateUser(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateUser(user), Times.Once);
        }

        [TestMethod]
        public void DeleteUser_ShouldDeleteUser()
        {
            // Arrange
            var username = "deleteuser";

            // Act
            _userRepositoryMock.Object.DeleteUser(username);

            // Assert
            _userRepositoryMock.Verify(repo => repo.DeleteUser(username), Times.Once);
        }
    }
}
