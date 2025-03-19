namespace atm.Models
{
    public class User
    {
        public string Username { get; private set; }
        public string UserType { get; private set; }
        public string PinCode { get; set; }

        public User(string username, string userType, string pinCode)
        {
            Username = username;
            UserType = userType;
            PinCode = pinCode;
        }
    }
}