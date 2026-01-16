using MySql.Data.MySqlClient;

namespace Breeder;

public class Portee
{
    public int Id { get; set; }
    public int IdAnimal { get; set; }
    public DateTime Date { get; set; }
    public string Libelle { get; set; }
    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Portee);

    public bool Equals(Portee? statut)
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
        return (Id == statut.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Portee? lhs, Portee? rhs)
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

    public static bool operator !=(Portee? lhs, Portee? rhs) => !(lhs == rhs);
}

public interface IPorteeDao
{
    List<Portee> GetPortees();
    List<Portee> GetPorteesByAnimal(int idAnimal);
    Portee? GetPortee(int id);

    void AjouterPortee(int idMere, string libelle, DateTime date);
    void SupprimerPortee(int id, int idAnimal);
    void ModifierPortee(Portee portee);
}

public interface IPorteeFacade
{
    List<Portee> GetPortees();
    List<Portee> GetPorteesByAnimal(int idAnimal);
    Portee? GetPortee(int id);

    void AjouterPortee(int idMere, string libelle, DateTime date);
    void SupprimerPortee(int id, int idAnimal);
    void ModifierPortee(Portee portee);
}

public class PorteeDaoImpl : IPorteeDao
{
    private readonly MySqlConnection _connection;

    public PorteeDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Portee> GetPortees()
    {
        MySqlCommand command = new MySqlCommand("select id, idAnimal, libellePortee, datePortee from portee;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Portee> portees = new List<Portee>();
        while (reader.Read())
        {
            Portee portee = new Portee();
            portee.Id = reader.GetInt32("id");
            portee.IdAnimal = reader.GetInt32("idAnimal");
            portee.Libelle = reader.GetString("libellePortee");
            portee.Date = reader.GetDateTime("datePortee");
            portees.Add(portee);
        }

        reader.Close();
        return portees;
    }

    public List<Portee> GetPorteesByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select id, libellePortee, datePortee from portee where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<Portee> portees = new List<Portee>();
        while (reader.Read())
        {
            Portee portee = new Portee();
            portee.Id = reader.GetInt32("id");
            portee.IdAnimal = idAnimal;
            portee.Libelle = reader.GetString("libellePortee");
            portee.Date = reader.GetDateTime("datePortee");
            portees.Add(portee);
        }

        reader.Close();
        return portees;
    }

    public Portee? GetPortee(int id)
    {
        MySqlCommand command = new MySqlCommand("select idAnimal, libellePortee, datePortee from portee where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Portee portee = new Portee();
        while (reader.Read())
        {
            portee.Id = id;
            portee.IdAnimal = reader.GetInt32("idAnimal");
            portee.Libelle = reader.GetString("libellePortee");
            portee.Date = reader.GetDateTime("datePortee");
        }

        reader.Close();
        return portee;
    }

    public void AjouterPortee(int idMere, string libelle, DateTime date)
    {
        MySqlCommand command = new MySqlCommand("insert into portee(idAnimal, datePortee, libellePortee) values(@idAnimal, @datePortee, @libellePortee);", _connection);
        command.Parameters.AddWithValue("idAnimal", idMere);
        command.Parameters.AddWithValue("datePortee", date);
        command.Parameters.AddWithValue("libellePortee", libelle);
        command.ExecuteNonQuery();
    }

    public void SupprimerPortee(int id, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("delete from portee where id = @id and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }

    public void ModifierPortee(Portee portee)
    {
        MySqlCommand command = new MySqlCommand("update portee set libellePortee = @libelle where id = @id", _connection);
        command.Parameters.AddWithValue("id", portee.Id);
        command.Parameters.AddWithValue("libelle", portee.Libelle);
        command.ExecuteNonQuery();
    }
}

public class PorteeFacadeImpl : IPorteeFacade
{
    readonly IPorteeDao _porteeDao;

    public PorteeFacadeImpl(IPorteeDao porteeDao)
    {
        _porteeDao = porteeDao;
    }

    public List<Portee> GetPortees()
    {
        return _porteeDao.GetPortees();
    }

    public List<Portee> GetPorteesByAnimal(int idAnimal)
    {
        return _porteeDao.GetPorteesByAnimal(idAnimal);
    }

    public Portee? GetPortee(int id)
    {
        return _porteeDao.GetPortee(id);
    }

    public void AjouterPortee(int idMere, string libelle, DateTime date)
    {
        _porteeDao.AjouterPortee(idMere, libelle, date);
    }

    public void SupprimerPortee(int id, int idAnimal)
    {
        _porteeDao.SupprimerPortee(id, idAnimal);
    }

    public void ModifierPortee(Portee portee)
    {
        _porteeDao.ModifierPortee(portee);
    }
}
