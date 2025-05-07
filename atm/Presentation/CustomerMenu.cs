using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Presentation
{
    /// <summary>
    /// Represents the customer menu for account operations.
    /// </summary>
    public class CustomerMenu
    {
        /// <summary>
        /// The service for customer-related operations.
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// The customer associated with the menu.
        /// </summary>
        private readonly Customer _customer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMenu"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        /// <param name="customer">The customer.</param>
        public CustomerMenu(ICustomerService customerService, Customer customer)
        {
            _customerService = customerService;
            _customer = customer;
        }

        /// <summary>
        /// Displays the customer menu.
        /// </summary>
        public void Display()
        {
            while (true)
            {
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. View Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewBalance();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        Withdraw();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Views the balance of the customer.
        /// </summary>
        private void ViewBalance()
        {
            int balance = _customerService.GetBalance(_customer);
            Console.WriteLine($"Your balance is: {balance}");
        }

        /// <summary>
        /// Deposits an amount into the customer's account.
        /// </summary>
        private void Deposit()
        {
            Console.Write("Enter amount to deposit: ");
            int amount = int.Parse(Console.ReadLine());
            _customerService.Deposit(_customer, amount);
            Console.WriteLine("Deposit successful.");
        }

        /// <summary>
        /// Withdraws an amount from the customer's account.
        /// </summary>
        private void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            int amount = int.Parse(Console.ReadLine());
            _customerService.Withdraw(_customer, amount);
            Console.WriteLine("Withdrawal successful.");
        }
    }
}
