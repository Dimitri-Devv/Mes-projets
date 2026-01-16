using MySql.Data.MySqlClient;

namespace Breeder;

public interface IDatabaseFactory
{
    MySqlConnection NewConnection();
}

public class DatabaseFactoryImpl : IDatabaseFactory
{
    private const string ChaineConnexion = "server=localhost;uid=root;pwd=;database=breeder";

    public MySqlConnection NewConnection()
    {
        MySqlConnection connection = new MySqlConnection(ChaineConnexion);
        connection.Open();
        return connection;
    }
}