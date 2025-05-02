using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Presentation
{
    public class CustomerMenu
    {
        private readonly ICustomerService _customerService;
        private readonly Customer _customer;

        public CustomerMenu(ICustomerService customerService, Customer customer)
        {
            _customerService = customerService;
            _customer = customer;
        }

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

        private void ViewBalance()
        {
            int balance = _customerService.GetBalance(_customer);
            Console.WriteLine($"Your balance is: {balance}");
        }

        private void Deposit()
        {
            Console.Write("Enter amount to deposit: ");
            int amount = int.Parse(Console.ReadLine());
            _customerService.Deposit(_customer, amount);
            Console.WriteLine("Deposit successful.");
        }

        private void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            int amount = int.Parse(Console.ReadLine());
            _customerService.Withdraw(_customer, amount);
            Console.WriteLine("Withdrawal successful.");
        }
    }
}
