using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Presentation
{
    /// <summary>
    /// Represents the login menu for user authentication.
    /// </summary>
    public class LoginMenu
    {
        /// <summary>
        /// The service for user-related operations.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginMenu"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public LoginMenu(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Prompts the user to log in.
        /// </summary>
        /// <returns>The authenticated user, or null if authentication fails.</returns>
        public User Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter pin code: ");
            string pinCode = Console.ReadLine();

            return _userService.Login(username, pinCode);
        }
    }
}
