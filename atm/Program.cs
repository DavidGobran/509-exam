using System;
using Ninject;
using atm.DependencyInjection;
using atm.Interfaces;
using atm.Models;
using atm.Presentation;

class Program
{
    static void Main(string[] args)
    {
        // Initialize dependency injection
        IKernel kernel = new StandardKernel(new DependencyInjectionConfig());

        // Get user service from DI container
        var userService = kernel.Get<IUserService>();

        // User login
        var loginMenu = new LoginMenu(userService);
        var user = loginMenu.Login();

        if (user == null)
        {
            Console.WriteLine("Invalid username or pin code.");
            return;
        }

        // Display menu based on user type
        if (user.UserType == "Customer")
        {
            var customer = new Customer(user.Username, user.PinCode, 0, 0, "", "");
            var customerMenu = new CustomerMenu(kernel.Get<ICustomerService>(), customer);
            customerMenu.Display();
        }
        else if (user.UserType == "Administrator")
        {
            var adminMenu = new AdminMenu(kernel.Get<IAdministratorService>());
            adminMenu.Display();
        }
    }
}
