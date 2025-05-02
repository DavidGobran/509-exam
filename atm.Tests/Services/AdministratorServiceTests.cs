using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using atm.Interfaces;
using atm.Models;
using atm.Services;

namespace atm.Tests.Services
{
    [TestClass]
    public class AdministratorServiceTests
    {
        private Mock<IAdministratorRepository> _mockRepository;
        private AdministratorService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IAdministratorRepository>();
            _service = new AdministratorService(_mockRepository.Object);
        }

        [TestMethod]
        public void AddCustomer_ShouldCallRepositoryAddCustomer()
        {
            var customer = new Customer("testuser", "1234", 1000, 1, "Active", "Test User");

            _service.AddCustomer(customer);

            _mockRepository.Verify(r => r.AddCustomer(customer), Times.Once);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldCallRepositoryUpdateCustomer()
        {
            var customer = new Customer("testuser", "1234", 1000, 1, "Active", "Test User");

            _service.UpdateCustomer(customer);

            _mockRepository.Verify(r => r.UpdateCustomer(customer), Times.Once);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldCallRepositoryDeleteCustomer()
        {
            int accountNumber = 1;

            _service.DeleteCustomer(accountNumber);

            _mockRepository.Verify(r => r.DeleteCustomer(accountNumber), Times.Once);
        }

        [TestMethod]
        public void GetCustomer_ShouldCallRepositoryGetCustomerAndReturnCustomer()
        {
            int accountNumber = 1;
            var expectedCustomer = new Customer("testuser", "1234", 1000, accountNumber, "Active", "Test User");

            _mockRepository.Setup(r => r.GetCustomer(accountNumber)).Returns(expectedCustomer);

            var customer = _service.GetCustomer(accountNumber);

            Assert.IsNotNull(customer);
            Assert.AreEqual(expectedCustomer.Username, customer.Username);
            Assert.AreEqual(expectedCustomer.PinCode, customer.PinCode);
            Assert.AreEqual(expectedCustomer.AccountBalance, customer.AccountBalance);
            Assert.AreEqual(expectedCustomer.Status, customer.Status);
            Assert.AreEqual(expectedCustomer.AccountHolder, customer.AccountHolder);

            _mockRepository.Verify(r => r.GetCustomer(accountNumber), Times.Once);
        }
    }
}