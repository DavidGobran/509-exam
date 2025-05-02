using System;
using atm.Interfaces;
using atm.Models;

namespace atm.Presentation
{
    public class LoginMenu
    {
        private readonly IUserService _userService;

        public LoginMenu(IUserService userService)
        {
            _userService = userService;
        }

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
