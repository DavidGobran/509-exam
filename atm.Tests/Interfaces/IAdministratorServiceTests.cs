using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;
using atm.Services;
using MySql.Data.MySqlClient;
using System.Data;

namespace atm.Tests.Interfaces
{
    [TestClass]
    public class IAdministratorServiceTests
    {
        private Mock<IAdministratorService> _mockService;
        private Customer _customer;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IAdministratorService>();
            _customer = new Customer("testuser", "1234", 1000, 1, "Active", "Test User");
        }

        [TestMethod]
        public void AddCustomer_ShouldAddCustomer()
        {
            _mockService.Setup(s => s.AddCustomer(_customer));

            _mockService.Object.AddCustomer(_customer);

            _mockService.Verify(s => s.AddCustomer(_customer), Times.Once);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldUpdateCustomer()
        {
            _mockService.Setup(s => s.UpdateCustomer(_customer));

            _mockService.Object.UpdateCustomer(_customer);

            _mockService.Verify(s => s.UpdateCustomer(_customer), Times.Once);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldDeleteCustomer()
        {
            _mockService.Setup(s => s.DeleteCustomer(1));

            _mockService.Object.DeleteCustomer(1);

            _mockService.Verify(s => s.DeleteCustomer(1), Times.Once);
        }

        [TestMethod]
        public void GetCustomer_ShouldReturnCustomer()
        {
            _mockService.Setup(s => s.GetCustomer(1)).Returns(_customer);

            var customer = _mockService.Object.GetCustomer(1);

            Assert.IsNotNull(customer);
            Assert.AreEqual("testuser", customer.Username);
            Assert.AreEqual("1234", customer.PinCode);
            Assert.AreEqual(1000, customer.AccountBalance);
            Assert.AreEqual("Active", customer.Status);
            Assert.AreEqual("Test User", customer.AccountHolder);

            _mockService.Verify(s => s.GetCustomer(1), Times.Once);
        }
    }
}
