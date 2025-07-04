using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Presentation
{
    /// <summary>
    /// Represents the administrator menu for managing customers.
    /// </summary>
    public class AdminMenu
    {
        /// <summary>
        /// The service for administrator-related operations.
        /// </summary>
        private readonly IAdministratorService _administratorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMenu"/> class.
        /// </summary>
        /// <param name="administratorService">The administrator service.</param>
        public AdminMenu(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        /// <summary>
        /// Displays the administrator menu.
        /// </summary>
        public void Display()
        {
            while (true)
            {
                Console.WriteLine("Administrator Menu:");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. View Customer");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        UpdateCustomer();
                        break;
                    case "3":
                        DeleteCustomer();
                        break;
                    case "4":
                        ViewCustomer();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        private void AddCustomer()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter pin code: ");
            string pinCode = Console.ReadLine();

            Console.Write("Enter account balance: ");
            int accountBalance = int.Parse(Console.ReadLine());

            Console.Write("Enter status: ");
            string status = Console.ReadLine();

            Console.Write("Enter account holder: ");
            string accountHolder = Console.ReadLine();

            var customer = new Customer(username, pinCode, accountBalance, 0, status, accountHolder);
            _administratorService.AddCustomer(customer);
            Console.WriteLine("Customer added successfully.");
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        private void UpdateCustomer()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            var customer = _administratorService.GetCustomer(accountNumber);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("Enter new username: ");
            customer.Username = Console.ReadLine();

            Console.Write("Enter new pin code: ");
            customer.PinCode = Console.ReadLine();

            Console.Write("Enter new status: ");
            customer.Status = Console.ReadLine();

            Console.Write("Enter new account holder: ");
            customer.AccountHolder = Console.ReadLine();

            _administratorService.UpdateCustomer(customer);
            Console.WriteLine("Customer updated successfully.");
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        private void DeleteCustomer()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            _administratorService.DeleteCustomer(accountNumber);
            Console.WriteLine("Customer deleted successfully.");
        }

        /// <summary>
        /// Views a customer's details.
        /// </summary>
        private void ViewCustomer()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            var customer = _administratorService.GetCustomer(accountNumber);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.WriteLine($"Username: {customer.Username}");
            Console.WriteLine($"Account Balance: {customer.AccountBalance}");
            Console.WriteLine($"Status: {customer.Status}");
            Console.WriteLine($"Account Holder: {customer.AccountHolder}");
        }
    }
}
