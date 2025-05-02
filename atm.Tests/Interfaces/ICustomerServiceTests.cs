using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;

namespace atm.Tests.Interfaces
{
    [TestClass]
    public class ICustomerServiceTests
    {
        private Mock<ICustomerService> _mockCustomerService;
        private Customer _customer;

        [TestInitialize]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customer = new Customer("testuser", "1234", 1000, 1, "Active", "Test User");
        }

        [TestMethod]
        public void GetBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            _mockCustomerService.Setup(service => service.GetBalance(_customer)).Returns(1000);

            // Act
            int balance = _mockCustomerService.Object.GetBalance(_customer);

            // Assert
            Assert.AreEqual(1000, balance);
        }

        [TestMethod]
        public void Deposit_ShouldIncreaseBalance()
        {
            // Arrange
            _mockCustomerService.Setup(service => service.Deposit(_customer, 500)).Callback(() => _customer.AccountBalance += 500);

            // Act
            _mockCustomerService.Object.Deposit(_customer, 500);

            // Assert
            Assert.AreEqual(1500, _customer.AccountBalance);
        }

        [TestMethod]
        public void Withdraw_ShouldDecreaseBalance()
        {
            // Arrange
            _mockCustomerService.Setup(service => service.Withdraw(_customer, 500)).Callback(() => _customer.AccountBalance -= 500);

            // Act
            _mockCustomerService.Object.Withdraw(_customer, 500);

            // Assert
            Assert.AreEqual(500, _customer.AccountBalance);
        }
    }
}
