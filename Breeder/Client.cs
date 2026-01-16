using System.Collections;
using MySql.Data.MySqlClient;

namespace Breeder;

public class Client
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Adresse { get; set; }

    public string Mail { get; set; }

    public string Telephone { get; set; }

    public int IdNiveau { get; set; }

    public override string ToString()
    {
        return Nom + " " + Prenom;
    }
    
    public override bool Equals(object obj) => Equals(obj as Client);

    public bool Equals(Client? statut)
    {
        if (statut is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (ReferenceEquals(this, statut))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (GetType() != statut.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return Id == statut.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Client? lhs, Client? rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Client? lhs, Client? rhs) => !(lhs == rhs);
}

public interface IClientDao
{
    List<Client> GetClients();
    List<Client> GetClientsRecents(int nombre);
    Client? GetClient(int id);
    void AjouterClient(string nom, string prenom, string adresse, string mail, string telephone, int idNiveau);
    void SupprimerClient(int id);
    void ModifierClient(Client client);
}

public interface IClientFacade
{
    List<Client> GetClients();
    List<Client> GetClientsRecents();
    Client? GetClient(int id);
    void AjouterClient(string nom, string prenom, string adresse, string mail, string telephone, int idNiveau);
    void SupprimerClient(int id);
    void ModifierClient(Client client);
}

public class ClientDaoImpl : IClientDao
{
    private readonly MySqlConnection _connection;

    public ClientDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Client> GetClients()
    {
        MySqlCommand command =
            new MySqlCommand("select id, nom, prenom, adresse, mail, telephone, idNiveau from client;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Client> clients = new List<Client>();
        while (reader.Read())
        {
            Client client = new Client();
            client.Id = reader.GetInt32("id");
            client.Nom = reader.GetString("nom");
            client.Prenom = reader.GetString("prenom");
            client.Adresse = reader.GetString("adresse");
            client.Mail = reader.GetString("mail");
            client.Telephone = reader.GetString("telephone");
            client.IdNiveau = reader.GetInt32("idNiveau");
            clients.Add(client);
        }

        reader.Close();
        return clients;
    }

    public List<Client> GetClientsRecents(int nombre)
    {
        MySqlCommand command = new MySqlCommand("select id, nom, prenom, adresse, mail, telephone, idNiveau from client order by id desc limit @nombre;", _connection);
        command.Parameters.AddWithValue("nombre", nombre);
        MySqlDataReader reader = command.ExecuteReader();

        List<Client> clients = new List<Client>();
        while (reader.Read())
        {
            Client client = new Client();
            client.Id = reader.GetInt32("id");
            client.Nom = reader.GetString("nom");
            client.Prenom = reader.GetString("prenom");
            client.Adresse = reader.GetString("adresse");
            client.Mail = reader.GetString("mail");
            client.Telephone = reader.GetString("telephone");
            client.IdNiveau = reader.GetInt32("idNiveau");
            clients.Add(client);
        }

        reader.Close();
        return clients;
    }

    public Client? GetClient(int id)
    {
        MySqlCommand command =
            new MySqlCommand("select nom, prenom, adresse, mail, telephone, idNiveau from client where id = @id;",
                _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Client client = new Client();
        while (reader.Read())
        {
            client.Id = id;
            client.Nom = reader.GetString("nom");
            client.Prenom = reader.GetString("prenom");
            client.Adresse = reader.GetString("adresse");
            client.Mail = reader.GetString("mail");
            client.Telephone = reader.GetString("telephone");
            client.IdNiveau = reader.GetInt32("idNiveau");
        }

        reader.Close();
        return client;
    }

    public void AjouterClient(string nom, string prenom, string adresse, string mail, string telephone, int idNiveau)
    {
        MySqlCommand command = new MySqlCommand();
        command.Connection = _connection;
        command.CommandText =
            "insert into client(nom, prenom, adresse, mail, telephone, idNiveau) values(@nom, @prenom, @adresse, @mail, @telephone, @idNiveau);";
        command.Parameters.AddWithValue("nom", nom);
        command.Parameters.AddWithValue("prenom", prenom);
        command.Parameters.AddWithValue("adresse", adresse);
        command.Parameters.AddWithValue("mail", mail);
        command.Parameters.AddWithValue("telephone", telephone);
        command.Parameters.AddWithValue("idNiveau", idNiveau);
        command.ExecuteNonQuery();
    }

    public void SupprimerClient(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from client where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void ModifierClient(Client client)
    {
        MySqlCommand command =
            new MySqlCommand(
                "update client set nom = @nom, prenom = @prenom, adresse = @adresse, mail = @mail, telephone = @telephone, idNiveau = @idNiveau where id = @id;",
                _connection);
        command.Parameters.AddWithValue("id", client.Id);
        command.Parameters.AddWithValue("nom", client.Nom);
        command.Parameters.AddWithValue("prenom", client.Prenom);
        command.Parameters.AddWithValue("adresse", client.Adresse);
        command.Parameters.AddWithValue("mail", client.Mail);
        command.Parameters.AddWithValue("telephone", client.Telephone);
        command.Parameters.AddWithValue("idNiveau", client.IdNiveau);
        command.ExecuteNonQuery();
    }
}

public class ClientFacadeImpl : IClientFacade
{
    private readonly IClientDao _clientDao;

    public ClientFacadeImpl(IClientDao clientDao)
    {
        _clientDao = clientDao;
    }

    public List<Client> GetClients()
    {
        return _clientDao.GetClients();
    }

    public List<Client> GetClientsRecents()
    {
        return _clientDao.GetClientsRecents(5);
    }

    public Client? GetClient(int id)
    {
        return _clientDao.GetClient(id);
    }

    public void AjouterClient(string nom, string prenom, string adresse, string mail, string telephone, int idNiveau)
    {
        _clientDao.AjouterClient(nom, prenom, adresse, mail, telephone, idNiveau);
    }

    public void SupprimerClient(int id)
    {
        _clientDao.SupprimerClient(id);
    }

    public void ModifierClient(Client client)
    {
        _clientDao.ModifierClient(client);
    }
}