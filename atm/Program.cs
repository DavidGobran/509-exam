using System;
using System.Data;
using MySql.Data.MySqlClient;
using Ninject;
using atm.DependencyInjection;
using atm.Interfaces;
using atm.Services;
using atm.Models;

class Program
{
    static void Main(string[] args)
    {
        // Initialize dependency injection
        IKernel kernel = new StandardKernel(new DependencyInjectionConfig());

        // Connect to MySQL database
        string connectionString = "server=127.0.0.1;user=atm_user;database=midterm;password=password";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Successfully connected to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
                return;
            }
        }

        // Get user service from DI container
        var userService = kernel.Get<IUserService>();

        // User login
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter 5-digit pin code:");
        string pinCode = Console.ReadLine();

        var user = userService.Login(username, pinCode);
        if (user == null)
        {
            Console.WriteLine("Invalid username or pin code.");
            return;
        }

        // Display menu based on user type
        if (user.UserType == "Customer")
        {
            var customer = new Customer(username, pinCode, 0, 0, "", "");
            DisplayCustomerMenu(kernel.Get<ICustomerService>(), customer);
        }
        else if (user.UserType == "Administrator")
        {
            DisplayAdministratorMenu(kernel.Get<IAdministratorService>());
        }
    }

    static void DisplayCustomerMenu(ICustomerService customerService, Customer customer)
    {
        while (true)
        {
            Console.WriteLine("Customer Menu:");
            Console.WriteLine("1. Withdraw Cash");
            Console.WriteLine("2. Deposit Cash");
            Console.WriteLine("3. Display Balance");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter amount to withdraw:");
                    int withdrawAmount = int.Parse(Console.ReadLine());
                    customerService.Withdraw(customer, withdrawAmount);
                    int balanceAfterWithdrawal = customerService.GetBalance(customer);
                    Console.WriteLine($"Cash Successfully Withdrawn\nAccount #{customer.AccountNumber}\nDate: {DateTime.Now:MM/dd/yyyy}\nWithdrawn: {withdrawAmount}\nBalance: {balanceAfterWithdrawal}");
                    break;
                case "2":
                    Console.WriteLine("Enter amount to deposit:");
                    int depositAmount = int.Parse(Console.ReadLine());
                    customerService.Deposit(customer, depositAmount);
                    int balanceAfterDeposit = customerService.GetBalance(customer);
                    Console.WriteLine($"Cash Deposited Successfully.\nAccount #{customer.AccountNumber}\nDate: {DateTime.Now:MM/dd/yyyy}\nDeposited: {depositAmount}\nBalance: {balanceAfterDeposit}");
                    break;
                case "3":
                    decimal currentBalance = customerService.GetBalance(customer);
                    Console.WriteLine($"Account #{customer.AccountNumber}\nDate: {DateTime.Now:MM/dd/yyyy}\nBalance: {currentBalance}");
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAdministratorMenu(IAdministratorService administratorService)
    {
        while (true)
        {
            Console.WriteLine("Administrator Menu:");
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Delete Existing Account");
            Console.WriteLine("3. Update Account Information");
            Console.WriteLine("4. Search for Account");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter new customer username:");
                    string newUsername = Console.ReadLine();
                    Console.WriteLine("Enter new customer 5-digit pin code:");
                    string newPinCode = Console.ReadLine();
                    if (newPinCode.Length != 5)
                    {
                        Console.WriteLine("Pin code must be 5 digits.");
                        break;
                    }
                    Console.WriteLine("Enter new customer status:");
                    string newStatus = Console.ReadLine();
                    Console.WriteLine("Enter new customer account holder:");
                    string newAccountHolder = Console.ReadLine();
                    Console.WriteLine("Enter initial deposit amount:");
                    int initialDeposit = int.Parse(Console.ReadLine());
                    var newUser = new Customer(newUsername, newPinCode, initialDeposit, 0, newStatus, newAccountHolder);
                    administratorService.AddCustomer(newUser);
                    Console.WriteLine($"Account for {newUsername} was successfully created.");
                    break;
                case "2":
                    Console.WriteLine("Enter customer account number to delete:");
                    int deleteAccountNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Re-enter customer account number to confirm deletion:");
                    int confirmDeleteAccountNumber = int.Parse(Console.ReadLine());
                    if (deleteAccountNumber == confirmDeleteAccountNumber)
                    {
                        administratorService.DeleteCustomer(deleteAccountNumber);
                        Console.WriteLine($"Account number {deleteAccountNumber} was successfully deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Account numbers do not match. Deletion cancelled.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Enter customer account number to update:");
                    int updateAccountNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new username:");
                    string updateUsername = Console.ReadLine();
                    Console.WriteLine("Enter new 5-digit pin code:");
                    string updatePinCode = Console.ReadLine();
                    if (updatePinCode.Length != 5)
                    {
                        Console.WriteLine("Pin code must be 5 digits.");
                        break;
                    }
                    Console.WriteLine("Enter new status:");
                    string updateStatus = Console.ReadLine();
                    Console.WriteLine("Enter new account holder:");
                    string updateAccountHolder = Console.ReadLine();
                    var updatedCustomer = new Customer(updateUsername, updatePinCode, 0, updateAccountNumber, updateStatus, updateAccountHolder);
                    administratorService.UpdateCustomer(updatedCustomer);
                    break;
                case "4":
                    Console.WriteLine("Enter customer account number to search:");
                    int searchAccountNumber = int.Parse(Console.ReadLine());
                    var customer = administratorService.GetCustomer(searchAccountNumber);
                    if (customer != null)
                    {
                        Console.WriteLine($"Username: {customer.Username}, Balance: {customer.AccountBalance}, Status: {customer.Status}, Account Holder: {customer.AccountHolder}");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}