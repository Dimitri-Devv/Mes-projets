using MySql.Data.MySqlClient;

namespace Breeder;

public class ClientAnimal
{
    public int Id { get; set; }
    public int IdClient { get; set; }
    public int IdAnimal { get; set; }
    
      
    public override bool Equals(object obj) => Equals(obj as ClientAnimal);

    public bool Equals(ClientAnimal? statut)
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

    public static bool operator ==(ClientAnimal? lhs, ClientAnimal? rhs)
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

    public static bool operator !=(ClientAnimal? lhs, ClientAnimal? rhs) => !(lhs == rhs);
}

public interface IClientAnimalDao
{
    List<ClientAnimal> GetClientAnimaux();
    ClientAnimal? GetClientAnimal(int id);
    
    List<int> GetIdAnimauxByClient(int idClient);
    int? GetIdClientAnimal(int idClient, int idAnimal);
    int? GetIdClientByAnimal(int idAnimal);
    
    void AjouterClientAnimal(int idClient, int idAnimal);
    void SupprimerClientAnimal(int id);
}

public interface IClientAnimalFacade
{
    List<ClientAnimal> GetClientAnimaux();
    
    List<Animal> GetAnimauxByClient(int idClient);
    ClientAnimal? GetClientAnimal(int idClient, int idAnimal);
    Client? GetClientByAnimal(int idAnimal);
    
    void AjouterClientAnimal(int idClient, int idAnimal);
    void SupprimerClientAnimal(int id);
}


public class ClientAnimalDaoImpl : IClientAnimalDao
{
    private readonly MySqlConnection _connection;

    public ClientAnimalDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<ClientAnimal> GetClientAnimaux()
    {
        MySqlCommand command = new MySqlCommand("select id, idClient, idAnimal from clientanimal;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<ClientAnimal> clients = new List<ClientAnimal>();
        while (reader.Read())
        {
            ClientAnimal clientAnimal = new ClientAnimal();
            clientAnimal.Id = reader.GetInt32("id");
            clientAnimal.IdClient = reader.GetInt32("idClient");
            clientAnimal.IdAnimal = reader.GetInt32("idAnimal");
            clients.Add(clientAnimal);
        }

        reader.Close();
        return clients;
    }

    public ClientAnimal? GetClientAnimal(int id)
    {
        MySqlCommand command = new MySqlCommand("select idClient, idAnimal from clientanimal where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        
        ClientAnimal clientAnimal = new ClientAnimal();
        while (reader.Read())
        {
            clientAnimal.Id = id;
            clientAnimal.IdClient = reader.GetInt32("idClient");
            clientAnimal.IdAnimal = reader.GetInt32("idAnimal");
        }

        reader.Close();
        return clientAnimal;
    }

    public List<int> GetIdAnimauxByClient(int idClient)
    {
        MySqlCommand command = new MySqlCommand("select idAnimal from clientanimal where idClient = @idClient;", _connection);
        command.Parameters.AddWithValue("idClient", idClient);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idAnimaux = new List<int>();
        while (reader.Read())
        {
            idAnimaux.Add(reader.GetInt32("idAnimal"));
        }

        reader.Close();
        return idAnimaux;
    }

    public int? GetIdClientAnimal(int idClient, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select id from clientanimal where idClient = @idClient and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idClient", idClient);
        command.Parameters.AddWithValue("idAnimal", idAnimal);

        object id = command.ExecuteScalar();
        if (id is not Int32 i)
        {
            return null;
        }

        return i;
    }

    public int? GetIdClientByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idClient from clientanimal where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        
        object id = command.ExecuteScalar();
        if (id is not Int32 i)
        {
            return null;
        }

        return i;
    }

    public void AjouterClientAnimal(int idClient, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("insert into clientanimal(idClient, idAnimal) values(@idClient, @idAnimal);", _connection);
        command.Parameters.AddWithValue("idClient", idClient);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }

    public void SupprimerClientAnimal(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from clientanimal where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }
}


public class ClientAnimalFacadeImpl : IClientAnimalFacade
{
    private readonly IClientAnimalDao _clientAnimalDao;
    private readonly IAnimalFacade _animalFacade;
    private readonly IClientFacade _clientFacade;

    public ClientAnimalFacadeImpl(IClientAnimalDao clientAnimalDao, IAnimalFacade animalFacade, IClientFacade clientFacade)
    {
        _clientAnimalDao = clientAnimalDao;
        _animalFacade = animalFacade;
        _clientFacade = clientFacade;
    }
    
    public List<ClientAnimal> GetClientAnimaux()
    {
        return _clientAnimalDao.GetClientAnimaux();
    }

    public List<Animal> GetAnimauxByClient(int idClient)
    {
        List<Animal> animals = new List<Animal>();
        foreach (int idAnimal in _clientAnimalDao.GetIdAnimauxByClient(idClient))
        {
            Animal? animal = _animalFacade.GetAnimal(idAnimal);
            if (animal != null)
            {
                animals.Add(animal);
            }
        }

        return animals;
    }

    public ClientAnimal? GetClientAnimal(int idClient, int idAnimal)
    {
        int? idClientAnimal = _clientAnimalDao.GetIdClientAnimal(idClient, idAnimal);
        return idClientAnimal == null ? null : _clientAnimalDao.GetClientAnimal((int)idClientAnimal);
    }

    public Client? GetClientByAnimal(int idAnimal)
    {
        int? idClient = _clientAnimalDao.GetIdClientByAnimal(idAnimal);
        return idClient == null ? null : _clientFacade.GetClient((int)idClient);
    }

    public void AjouterClientAnimal(int idClient, int idAnimal)
    {
        _clientAnimalDao.AjouterClientAnimal(idClient, idAnimal);
    }

    public void SupprimerClientAnimal(int id)
    {
        _clientAnimalDao.SupprimerClientAnimal(id);
    }
}