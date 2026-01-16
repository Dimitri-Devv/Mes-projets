using MySql.Data.MySqlClient;

namespace Breeder;

public class Statut
{
    public int Id { get; set; }

    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Statut);

    public bool Equals(Statut? statut)
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

    public static bool operator ==(Statut? lhs, Statut? rhs)
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

    public static bool operator !=(Statut? lhs, Statut? rhs) => !(lhs == rhs);
}

public interface IStatutDao : InconnuRequired
{
    List<Statut> GetStatuts();
    List<Statut> GetStatutsSansInconnu();
    Statut? GetStatut(int id);
    void AjouterStatut(string libelle);
    void SupprimerStatut(int idStatut);
}

public interface IStatutFacade : InconnuRequired, InconnuProvider<Statut>
{
    List<Statut> GetStatuts();
    List<Statut> GetStatutsSansInconnu();
    Statut? GetStatut(int id);
    void AjouterStatut(string libelle);
    void SupprimerStatut(int id);
}


public class StatutDaoImpl : IStatutDao
{
    private readonly MySqlConnection _connection;

    public StatutDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public List<Statut> GetStatuts()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleStatut from statut;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Statut> statuts = new List<Statut>();
        while (reader.Read())
        {
            Statut statut = new Statut();
            statut.Id = reader.GetInt32("id");
            statut.Libelle = reader.GetString("libelleStatut");
            statuts.Add(statut);
        }

        reader.Close();
        return statuts;
    }

    public List<Statut> GetStatutsSansInconnu()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleStatut from statut where id != @id;", _connection);
        command.Parameters.AddWithValue("id", 1);
        MySqlDataReader reader = command.ExecuteReader();

        List<Statut> statuts = new List<Statut>();
        while (reader.Read())
        {
            Statut statut = new Statut();
            statut.Id = reader.GetInt32("id");
            statut.Libelle = reader.GetString("libelleStatut");
            statuts.Add(statut);
        }

        reader.Close();
        return statuts;
    }

    public Statut? GetStatut(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelleStatut from statut where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        
        Statut statut = new Statut();
        statut.Id = id;
        while (reader.Read())
        {
            statut.Libelle = reader.GetString("libelleStatut");
        }

        reader.Close();
        return statut;
    }

    public void AjouterStatut(string libelle)
    {
        MySqlCommand command = new MySqlCommand("insert into statut(libelleStatut) values(@libelle);", _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.ExecuteNonQuery();
    }

    public void SupprimerStatut(int idStatut)
    {
        MySqlCommand command = new MySqlCommand("delete from statut where id = @id;", _connection);
        command.Parameters.AddWithValue("id", idStatut);
        command.ExecuteNonQuery();
    }

    public void AjouterInconnu()
    {
        MySqlCommand command = new MySqlCommand("insert into statut(id, libelleStatut) values(@id, @libelle);", _connection);
        command.Parameters.AddWithValue("id", 1);
        command.Parameters.AddWithValue("libelle", "Inconnu");
        command.ExecuteNonQuery();
    }
}


public class StatutFacadeImpl : IStatutFacade
{
    private readonly IStatutDao _statutDao;

    public StatutFacadeImpl(IStatutDao statutDao)
    {
        _statutDao = statutDao;
    }
    
    public void AjouterInconnu()
    {
        _statutDao.AjouterInconnu();
    }

    public Statut GetInconnu()
    {
        Statut? statut = GetStatut(1);
        if (statut != null) 
            return statut;
        
        AjouterInconnu();
        statut = GetStatut(1) ?? throw new Exception(string.Format(MessageUtils.Erreur_Inconnu, "Statut"));
        return statut;
    }

    public List<Statut> GetStatuts()
    {
        return _statutDao.GetStatuts();
    }

    public List<Statut> GetStatutsSansInconnu()
    {
        return _statutDao.GetStatutsSansInconnu();
    }

    public Statut? GetStatut(int id)
    {
        return _statutDao.GetStatut(id);
    }

    public void AjouterStatut(string libelle)
    {
        _statutDao.AjouterStatut(libelle);
    }

    public void SupprimerStatut(int id)
    {
        _statutDao.SupprimerStatut(id);
    }
}