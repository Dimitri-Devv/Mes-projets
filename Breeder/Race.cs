using MySql.Data.MySqlClient;

namespace Breeder;

public class Race
{
    public int Id { get; set; }
    public string Libelle { get; set; }

    public override string ToString()
    {
        return Libelle;
    }
    public override bool Equals(object obj) => Equals(obj as Race);

    public bool Equals(Race? statut)
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

    public static bool operator ==(Race? lhs, Race? rhs)
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

    public static bool operator !=(Race? lhs, Race? rhs) => !(lhs == rhs);
}

public interface IRaceDao : InconnuRequired
{
    List<Race> GetRaces();
    List<Race> GetRacesSansInconnu();
    Race? GetRace(int id);
    void AjouterRace(string libelle);
    void SupprimerRace(int id);
}

public interface IRaceFacade : InconnuRequired, InconnuProvider<Race>
{
    List<Race> GetRaces();
    List<Race> GetRacesSansInconnu();
    Race? GetRace(int id);
    void AjouterRace(string libelle);
    void SupprimerRace(int id);
}

public class RaceDaoImpl : IRaceDao
{
    private readonly MySqlConnection _connection;

    public RaceDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public void AjouterInconnu()
    {
        MySqlCommand command = new MySqlCommand("insert into race(id, libelleRace) values(@id, @libelle);", _connection);
        command.Parameters.AddWithValue("id", 1);
        command.Parameters.AddWithValue("libelle", "Inconnu");
        command.ExecuteNonQuery();
    }

    public List<Race> GetRaces()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleRace from race;", _connection);
        MySqlDataReader reader = command.ExecuteReader();
        
        List<Race> races = new List<Race>();
        while (reader.Read())
        {
            Race race = new Race();
            race.Id = reader.GetInt32("id");
            race.Libelle = reader.GetString("libelleRace");
            races.Add(race);
        }

        reader.Close();
        return races;
    }

    public List<Race> GetRacesSansInconnu()
    {
        MySqlCommand command = new MySqlCommand("select id, libelleRace from race where id != @id;", _connection);
        command.Parameters.AddWithValue("id", 1);
        MySqlDataReader reader = command.ExecuteReader();
        
        List<Race> races = new List<Race>();
        while (reader.Read())
        {
            Race race = new Race();
            race.Id = reader.GetInt32("id");
            race.Libelle = reader.GetString("libelleRace");
            races.Add(race);
        }

        reader.Close();
        return races;
    }

    public Race? GetRace(int id)
    {
        MySqlCommand command = new MySqlCommand("select libelleRace from race where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        Race race = new Race();
        race.Id = id;
        while (reader.Read())
        {
            race.Libelle = reader.GetString("libelleRace");
        }

        reader.Close();
        return race;
    }

    public void AjouterRace(string libelle)
    {
        MySqlCommand command = new MySqlCommand("insert into race(libelleRace) values(@libelle);", _connection);
        command.Parameters.AddWithValue("libelle", libelle);
        command.ExecuteNonQuery();
    }

    public void SupprimerRace(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from race where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }
}

public class RaceFacadeImpl : IRaceFacade
{
    private readonly IRaceDao _raceDao;

    public RaceFacadeImpl(IRaceDao raceDao)
    {
        _raceDao = raceDao;
    }

    public void AjouterInconnu()
    {
        _raceDao.AjouterInconnu();
    }

    public Race GetInconnu()
    {
        Race? race = GetRace(1);
        if (race != null)
            return race;

        AjouterInconnu();
        race = GetRace(1) ?? throw new Exception(string.Format(MessageUtils.Erreur_Inconnu, "Race"));
        return race;
    }

    public List<Race> GetRaces()
    {
        return _raceDao.GetRaces();
    }

    public List<Race> GetRacesSansInconnu()
    {
        return _raceDao.GetRacesSansInconnu();
    }

    public Race? GetRace(int id)
    {
        return _raceDao.GetRace(id);
    }

    public void AjouterRace(string libelle)
    {
        _raceDao.AjouterRace(libelle);
    }

    public void SupprimerRace(int id)
    {
        _raceDao.SupprimerRace(id);
    }
}