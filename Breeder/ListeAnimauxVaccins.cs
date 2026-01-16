using MySql.Data.MySqlClient;

namespace Breeder;

public class ListeAnimauxVaccins
{
    public int IdAnimal { get; set; }
    public int IdVaccin { get; set; }
    public DateTime Date { get; set; }

    public override bool Equals(object obj) => Equals(obj as ListeAnimauxVaccins);

    public bool Equals(ListeAnimauxVaccins? statut)
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
        return (IdVaccin == statut.IdVaccin && IdAnimal == statut.IdAnimal);
    }

    public override int GetHashCode() => (IdVaccin, IdAnimal).GetHashCode();

    public static bool operator ==(ListeAnimauxVaccins? lhs, ListeAnimauxVaccins? rhs)
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

    public static bool operator !=(ListeAnimauxVaccins? lhs, ListeAnimauxVaccins? rhs) => !(lhs == rhs);
}

public class Vaccination
{
    public Vaccin Vaccin { get; set; }
    public DateTime Date { get; set; }

    public Vaccination()
    {
    }
}

public interface IListeAnimauxVaccinsDao
{
    List<ListeAnimauxVaccins> GetListeAnimauxVaccins();
    List<int> GetIdVaccinsManquantsAnimal(int idAnimal);
    List<KeyValuePair<int, DateTime>> GetVaccinsByAnimal(int idAnimal);

    void AjouterAnimalVaccin(int idVaccin, int idAnimal);
    void RetirerAnimalVaccin(int idVaccin, int idAnimal);
}

public interface IListeAnimauxVaccinsFacade
{
    List<ListeAnimauxVaccins> GetListeAnimauxVaccins();
    List<Vaccin> GetVaccinsManquantsAnimal(int idAnimal);
    List<Vaccination> GetVaccinsByAnimal(int idAnimal);

    void AjouterAnimalVaccin(int idVaccin, int idAnimal);
    void RetirerAnimalVaccin(int idVaccin, int idAnimal);
}

public class ListeAnimauxVaccinsDaoImpl : IListeAnimauxVaccinsDao
{
    private readonly MySqlConnection _connection;

    public ListeAnimauxVaccinsDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }

    public List<ListeAnimauxVaccins> GetListeAnimauxVaccins()
    {
        MySqlCommand command = new MySqlCommand("select idAnimal, idVaccin, dateVaccination from listeanimauxvaccins;",
            _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<ListeAnimauxVaccins> listeAnimauxVaccinsList = new List<ListeAnimauxVaccins>();
        while (reader.Read())
        {
            ListeAnimauxVaccins listeAnimauxVaccins = new ListeAnimauxVaccins();
            listeAnimauxVaccins.IdVaccin = reader.GetInt32("idVaccin");
            listeAnimauxVaccins.IdAnimal = reader.GetInt32("idAnimal");
            listeAnimauxVaccins.Date = reader.GetDateTime("dateVaccination");
            listeAnimauxVaccinsList.Add(listeAnimauxVaccins);
        }

        reader.Close();
        return listeAnimauxVaccinsList;
    }

    public List<int> GetIdVaccinsManquantsAnimal(int idAnimal)
    {
        MySqlCommand command =
            new MySqlCommand(
                "select id from vaccin where id != 1 and not exists(select idVaccin from listeanimauxvaccins where id = idVaccin and idAnimal = @idAnimal);",
                _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idVaccins = new List<int>();
        while (reader.Read())
        {
            idVaccins.Add(reader.GetInt32("id"));
        }

        reader.Close();
        return idVaccins;
    }

    public List<KeyValuePair<int, DateTime>> GetVaccinsByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idVaccin, dateVaccination from listeanimauxvaccins where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<KeyValuePair<int, DateTime>> vaccinations = new List<KeyValuePair<int, DateTime>>();
        while (reader.Read())
        {
            vaccinations.Add(new KeyValuePair<int, DateTime>(reader.GetInt32("idVaccin"), reader.GetDateTime("dateVaccination")));
        }

        reader.Close();
        return vaccinations;
    }

    public void AjouterAnimalVaccin(int idVaccin, int idAnimal)
    {
        MySqlCommand command =
            new MySqlCommand(
                "insert into listeanimauxvaccins(idAnimal, idVaccin, dateVaccination) VALUES(@idAnimal, @idVaccin, @date);",
                _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("idVaccin", idVaccin);
        command.Parameters.AddWithValue("date", DateTime.Today);
        command.ExecuteNonQuery();
    }

    public void RetirerAnimalVaccin(int idVaccin, int idAnimal)
    {
        MySqlCommand command =
            new MySqlCommand("delete from listeanimauxvaccins where idVaccin = @idVaccin and idAnimal = @idAnimal;",
                _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("idVaccin", idVaccin);
        command.ExecuteNonQuery();
    }
}

public class ListeAnimauxVaccinsFacadeImpl : IListeAnimauxVaccinsFacade
{
    private readonly IListeAnimauxVaccinsDao _listeAnimauxVaccinsDao;
    private readonly IVaccinFacade _vaccinFacade;

    public ListeAnimauxVaccinsFacadeImpl(IListeAnimauxVaccinsDao listeAnimauxVaccinsDao, IVaccinFacade vaccinFacade)
    {
        _listeAnimauxVaccinsDao = listeAnimauxVaccinsDao;
        _vaccinFacade = vaccinFacade;
    }

    public List<ListeAnimauxVaccins> GetListeAnimauxVaccins()
    {
        return _listeAnimauxVaccinsDao.GetListeAnimauxVaccins();
    }

    public List<Vaccin> GetVaccinsManquantsAnimal(int idAnimal)
    {
        List<Vaccin> vaccins = new List<Vaccin>();
        foreach (int idVaccin in _listeAnimauxVaccinsDao.GetIdVaccinsManquantsAnimal(idAnimal))
        {
            Vaccin? vaccin = _vaccinFacade.GetVaccin(idVaccin);
            if (vaccin != null)
            {
                vaccins.Add(vaccin);
            }
        }

        return vaccins;
    }

    public List<Vaccination> GetVaccinsByAnimal(int idAnimal)
    {
        List<Vaccination> vaccins = new List<Vaccination>();
        foreach (var (idVaccin, date) in _listeAnimauxVaccinsDao.GetVaccinsByAnimal(idAnimal))
        {
            Vaccin? vaccin = _vaccinFacade.GetVaccin(idVaccin);
            if (vaccin != null)
            {
                vaccins.Add(new Vaccination { Vaccin = vaccin, Date = date});
            }
        }

        return vaccins;
    }


    public void AjouterAnimalVaccin(int idVaccin, int idAnimal)
    {
        _listeAnimauxVaccinsDao.AjouterAnimalVaccin(idVaccin, idAnimal);
    }

    public void RetirerAnimalVaccin(int idVaccin, int idAnimal)
    {
        _listeAnimauxVaccinsDao.RetirerAnimalVaccin(idVaccin, idAnimal);
    }
}