namespace atm.Models
{
    public class Administrator : User
    {
        public Administrator(string username, string pinCode)
            : base(username, "Administrator", pinCode)
        {
        }
    }
}
