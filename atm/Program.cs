using System;
using System.Data;
using MySql.Data.MySqlClient;
using Ninject;
using atm.DependencyInjection;
using atm.Interfaces;
using atm.Services;

class Program
{
    static void Main(string[] args)
    {
        // Initialize dependency injection
        IKernel kernel = new StandardKernel(new DependencyInjectionConfig());

        // Connect to MySQL database
        string connectionString = "server=127.0.0.1;user=root;database=midterm;password=password";
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
        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();

        var user = userService.Login(username, password);
        if (user == null)
        {
            Console.WriteLine("Invalid username or password.");
            return;
        }

        // Display menu based on user type
        if (user.UserType == "Customer")
        {
            DisplayCustomerMenu(kernel.Get<ICustomerService>(), user);
        }
        else if (user.UserType == "Administrator")
        {
            DisplayAdministratorMenu(kernel.Get<IAdministratorService>(), user);
        }
    }

    static void DisplayCustomerMenu(ICustomerService customerService, User user)
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
                    decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                    customerService.Withdraw(user, withdrawAmount);
                    break;
                case "2":
                    Console.WriteLine("Enter amount to deposit:");
                    decimal depositAmount = decimal.Parse(Console.ReadLine());
                    customerService.Deposit(user, depositAmount);
                    break;
                case "3":
                    Console.WriteLine($"Balance: {customerService.GetBalance(user)}");
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAdministratorMenu(IAdministratorService administratorService, User user)
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
                    Console.WriteLine("Enter new customer password:");
                    string newPassword = Console.ReadLine();
                    administratorService.AddCustomer(new User(newUsername, newPassword, "Customer"));
                    break;
                case "2":
                    Console.WriteLine("Enter customer username to delete:");
                    string deleteUsername = Console.ReadLine();
                    administratorService.DeleteCustomer(deleteUsername);
                    break;
                case "3":
                    Console.WriteLine("Enter customer username to update:");
                    string updateUsername = Console.ReadLine();
                    Console.WriteLine("Enter new password:");
                    string updatePassword = Console.ReadLine();
                    administratorService.UpdateCustomer(new User(updateUsername, updatePassword, "Customer"));
                    break;
                case "4":
                    Console.WriteLine("Enter customer username to search:");
                    string searchUsername = Console.ReadLine();
                    var customer = administratorService.GetCustomer(searchUsername);
                    if (customer != null)
                    {
                        Console.WriteLine($"Username: {customer.Username}, Balance: {customer.AccountBalance}");
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
