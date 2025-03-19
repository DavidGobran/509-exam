using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IUserService _userService;

        public AdministratorService(IUserService userService)
        {
            _userService = userService;
        }

        public void AddCustomer(User user)
        {
            var newCustomer = new Customer(user.Username, user.Password);
            _userService.Register(newCustomer);
        }

        public void UpdateCustomer(User user)
        {
            var existingCustomer = _userService.GetUser(user.Username) as Customer;
            if (existingCustomer != null)
            {
                existingCustomer.Password = user.Password;
                _userService.UpdateUser(existingCustomer);
            }
        }

        public void DeleteCustomer(string username)
        {
            _userService.DeleteUser(username);
        }
    }
}