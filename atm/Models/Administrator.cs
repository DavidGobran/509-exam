namespace atm.Models
{
    /// <summary>
    /// Represents an administrator in the system.
    /// </summary>
    public class Administrator : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Administrator"/> class.
        /// </summary>
        /// <param name="username">The username of the administrator.</param>
        /// <param name="pinCode">The pin code of the administrator.</param>
        public Administrator(string username, string pinCode)
            : base(username, "Administrator", pinCode)
        {
        }
    }
}
