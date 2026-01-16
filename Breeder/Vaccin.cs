using MySql.Data.MySqlClient;

namespace Breeder;

public class Vaccin
{
    public int Id { get; set; }
    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }

    public override bool Equals(object obj) => Equals(obj as Vaccin);

    public bool Equals(Vaccin? statut)
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

    public static bool operator ==(Vaccin? lhs, Vaccin? rhs)
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

    public static bool operator !=(Vaccin? lhs, Vaccin? rhs) => !(lhs == rhs);
}

public interface IVaccinDao
{
    List<Vaccin> GetVaccins();
    Vaccin? GetVaccin(int id);
    void AjouterVaccin(string libelle);
    void ModifierVaccin(Vaccin vaccin);
    void SupprimerVaccin(int id);
}

public interface IVaccinFacade
{
    List<Vaccin> GetVaccins();
    Vaccin? GetVaccin(int id);
    void AjouterVaccin(string libelle);
    void SupprimerVaccin(int id);
    void ModifierVaccin(Vaccin vaccin);
}


public class VaccinDaoImpl : IVaccinDao
{
    private readonly MySqlConnection _connection;

    public VaccinDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<Vaccin> GetVaccins()
    {
        MySqlCommand command = new MySqlCommand("select id, libelle from vaccin;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Vaccin> vaccins = new List<Vaccin>();
        while (reader.Read())
        {
            Vaccin vaccin = new Vaccin();
            vaccin.Id = reader.GetInt32("id");
            vaccin.Libelle = reader.GetString("libelle");
            vaccins.Add(vaccin);
        }

        reader.Close();
        return vaccins;
    }

    public Vaccin? GetVaccin(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelle from vaccin where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }
        
        Vaccin vaccin = new Vaccin();
        while (reader.Read())
        {
            vaccin.Id = id;
            vaccin.Libelle = reader.GetString("libelle");
        }

        reader.Close();
        return vaccin;
    }

    public void SupprimerVaccin(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from vaccin where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void AjouterVaccin(string libelle)
    {
        MySqlCommand command = new MySqlCommand("insert into vaccin(libelle) values(@libelle);", _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.ExecuteNonQuery();
    }

    public void ModifierVaccin(Vaccin vaccin)
    {
        MySqlCommand command = new MySqlCommand("update vaccin set libelle = @libelle where id = @id;", _connection);
        command.Parameters.AddWithValue("id", vaccin.Id);
        command.Parameters.AddWithValue("libelle", vaccin.Libelle);
        command.ExecuteNonQuery();
    }
}


public class VaccinFacadeImpl : IVaccinFacade
{
    private readonly IVaccinDao _vaccinDao;

    public VaccinFacadeImpl(IVaccinDao vaccinDao)
    {
        _vaccinDao = vaccinDao;
    }
    
    public List<Vaccin> GetVaccins()
    {
        return _vaccinDao.GetVaccins();
    }

    public Vaccin? GetVaccin(int id)
    {
        return _vaccinDao.GetVaccin(id);
    }

    public void AjouterVaccin(string libelle)
    {
        _vaccinDao.AjouterVaccin(libelle);
    }

    public void SupprimerVaccin(int id)
    {
        _vaccinDao.SupprimerVaccin(id);
    }

    public void ModifierVaccin(Vaccin vaccin)
    {
        _vaccinDao.ModifierVaccin(vaccin);
    }
}