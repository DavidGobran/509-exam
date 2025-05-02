using MySql.Data.MySqlClient;

namespace atm.Interfaces
{
    public interface IMySqlConnectionWrapper
    {
        void Open();
        void Close();
        MySqlCommand CreateCommand();
    }
}