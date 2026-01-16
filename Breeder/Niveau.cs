using MySql.Data.MySqlClient;

namespace Breeder;

public class Niveau
{
    public int Id { get; set; }

    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    
    public override bool Equals(object obj) => Equals(obj as Niveau);

    public bool Equals(Niveau? statut)
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

    public static bool operator ==(Niveau? lhs, Niveau? rhs)
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

    public static bool operator !=(Niveau? lhs, Niveau? rhs) => !(lhs == rhs);
}

public interface INiveauDao
{
    List<Niveau> GetNiveaux();
    Niveau? GetNiveau(int id);
    void AjouterNiveau(string libelle);
    void SupprimerNiveau(int id);
}

public interface INiveauFacade
{
    List<Niveau> GetNiveaux();
    Niveau? GetNiveau(int id);
    void AjouterNiveau(string libelle);
    void SupprimerNiveau(int id);
}

public class NiveauDaoImpl : INiveauDao
{
    private readonly MySqlConnection _connection;

    public NiveauDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Niveau> GetNiveaux()
    {
        MySqlCommand command = new MySqlCommand("select id, libelle from niveau;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Niveau> niveaux = new List<Niveau>();
        while (reader.Read())
        {
            Niveau niveau = new Niveau();
            niveau.Id = reader.GetInt32("id");
            niveau.Libelle = reader.GetString("libelle");
            niveaux.Add(niveau);
        }

        reader.Close();
        return niveaux;
    }

    public Niveau? GetNiveau(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelle from niveau where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Niveau niveau = new Niveau();
        while (reader.Read())
        {
            niveau.Id = id;
            niveau.Libelle = reader.GetString("libelle");
        }

        reader.Close();
        return niveau;
    }

    public void AjouterNiveau(string libelle)
    {
        MySqlCommand command = new MySqlCommand("insert into niveau(libelle) values(@libelle);", _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.ExecuteNonQuery();
    }

    public void SupprimerNiveau(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from niveau where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }
}

public class NiveauFacadeImpl : INiveauFacade
{
    private readonly INiveauDao _niveauDao;

    public NiveauFacadeImpl(INiveauDao niveauDao)
    {
        _niveauDao = niveauDao;
    }
    
    public List<Niveau> GetNiveaux()
    {
        return _niveauDao.GetNiveaux();
    }

    public Niveau? GetNiveau(int id)
    {
        return _niveauDao.GetNiveau(id);
    }

    public void AjouterNiveau(string libelle)
    {
        _niveauDao.AjouterNiveau(libelle);
    }

    public void SupprimerNiveau(int id)
    {
        _niveauDao.SupprimerNiveau(id);
    }
}