using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;

namespace atm.Tests.Interfaces
{
    [TestClass]
    public class IAccountRepositoryTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
        }

        [TestMethod]
        public void AddAccount_ShouldCallAddAccountMethod()
        {
            // Arrange
            var customer = new Customer("username", "pinCode", 1000, 12345, "Active", "AccountHolder");

            // Act
            _mockAccountRepository.Object.AddAccount(customer);

            // Assert
            _mockAccountRepository.Verify(repo => repo.AddAccount(customer), Times.Once);
        }

        [TestMethod]
        public void UpdateAccount_ShouldCallUpdateAccountMethod()
        {
            // Arrange
            var customer = new Customer("username", "pinCode", 1000, 12345, "Active", "AccountHolder");

            // Act
            _mockAccountRepository.Object.UpdateAccount(customer);

            // Assert
            _mockAccountRepository.Verify(repo => repo.UpdateAccount(customer), Times.Once);
        }

        [TestMethod]
        public void DeleteAccount_ShouldCallDeleteAccountMethod()
        {
            // Arrange
            int accountNumber = 12345;

            // Act
            _mockAccountRepository.Object.DeleteAccount(accountNumber);

            // Assert
            _mockAccountRepository.Verify(repo => repo.DeleteAccount(accountNumber), Times.Once);
        }

        [TestMethod]
        public void GetAccount_ShouldCallGetAccountMethod()
        {
            // Arrange
            int accountNumber = 12345;

            // Act
            _mockAccountRepository.Object.GetAccount(accountNumber);

            // Assert
            _mockAccountRepository.Verify(repo => repo.GetAccount(accountNumber), Times.Once);
        }
    }
}
