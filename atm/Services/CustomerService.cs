using System;
using System.Collections.Generic;
using atm.Interfaces;
using atm.Models;

namespace atm.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUserService _userService;

        public CustomerService(IUserService userService)
        {
            _userService = userService;
        }

        public decimal GetBalance(User user)
        {
            var customer = _userService.GetUser(user.Username) as Customer;
            return customer?.AccountBalance ?? 0;
        }

        public void Deposit(User user, decimal amount)
        {
            var customer = _userService.GetUser(user.Username) as Customer;
            if (customer != null)
            {
                customer.AccountBalance += amount;
                customer.AddTransaction($"Deposited: {amount}");
            }
        }

        public void Withdraw(User user, decimal amount)
        {
            var customer = _userService.GetUser(user.Username) as Customer;
            if (customer != null && customer.AccountBalance >= amount)
            {
                customer.AccountBalance -= amount;
                customer.AddTransaction($"Withdrew: {amount}");
            }
        }

        public List<string> GetTransactionHistory(User user)
        {
            var customer = _userService.GetUser(user.Username) as Customer;
            return customer?.TransactionHistory ?? new List<string>();
        }
    }
}
