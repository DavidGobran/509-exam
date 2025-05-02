using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;
using atm.Services;

namespace atm.Tests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private CustomerService _customerService;
        private Customer _customer;

        [TestInitialize]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
            _customer = new Customer("testuser", "1234", 1000, 1, "Active", "Test User");
        }

        [TestMethod]
        public void GetBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            _customerRepositoryMock.Setup(repo => repo.GetBalance(_customer.Username)).Returns(1000);

            // Act
            int balance = _customerService.GetBalance(_customer);

            // Assert
            Assert.AreEqual(1000, balance);
            _customerRepositoryMock.Verify(repo => repo.GetBalance(_customer.Username), Times.Once);
        }

        [TestMethod]
        public void Deposit_ShouldIncreaseBalance()
        {
            // Arrange
            int depositAmount = 500;
            _customerRepositoryMock.Setup(repo => repo.Deposit(_customer.Username, depositAmount));

            // Act
            _customerService.Deposit(_customer, depositAmount);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Deposit(_customer.Username, depositAmount), Times.Once);
        }

        [TestMethod]
        public void Withdraw_ShouldDecreaseBalance()
        {
            // Arrange
            int withdrawAmount = 500;
            _customerRepositoryMock.Setup(repo => repo.Withdraw(_customer.Username, withdrawAmount));

            // Act
            _customerService.Withdraw(_customer, withdrawAmount);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Withdraw(_customer.Username, withdrawAmount), Times.Once);
        }
    }
}