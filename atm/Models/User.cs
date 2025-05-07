namespace atm.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets the type of the user.
        /// </summary>
        public string UserType { get; private set; }

        /// <summary>
        /// Gets or sets the pin code of the user.
        /// </summary>
        public string PinCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="userType">The type of the user.</param>
        /// <param name="pinCode">The pin code of the user.</param>
        public User(string username, string userType, string pinCode)
        {
            Username = username;
            UserType = userType;
            PinCode = pinCode;
        }
    }
}