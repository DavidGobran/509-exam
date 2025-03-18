namespace atm.Models
{
    public class Administrator : User
    {
        public Administrator(string username, string password)
            : base(username, password, "Administrator")
        {
        }

    }
}
