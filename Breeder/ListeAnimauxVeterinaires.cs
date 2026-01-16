using MySql.Data.MySqlClient;

namespace Breeder;

public class ListeAnimauxVeterinaires
{
    public int IdVeterinaire { get; set; }
    public int IdAnimal { get; set; }
    public string Observations { get; set; }
    
    public override bool Equals(object obj) => Equals(obj as ListeAnimauxVeterinaires);

    public bool Equals(ListeAnimauxVeterinaires? statut)
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
        return (IdVeterinaire == statut.IdVeterinaire && IdAnimal == statut.IdAnimal);
    }

    public override int GetHashCode() => (IdVeterinaire, IdAnimal).GetHashCode();

    public static bool operator ==(ListeAnimauxVeterinaires? lhs, ListeAnimauxVeterinaires? rhs)
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

    public static bool operator !=(ListeAnimauxVeterinaires? lhs, ListeAnimauxVeterinaires? rhs) => !(lhs == rhs);
}

public interface IListeAnimauxVeterinairesDao
{
    List<ListeAnimauxVeterinaires> GetListeAnimauxVeterinaires();
    List<int> GetAnimauxDeVeterinaire(int idVeterinaire);
    List<int> GetAnimauxPasCeVeterinaire(int idVeterinaire);
    List<int> GetVeterinairesByAnimal(int idAnimal);
    void AjouterAnimalVeterinaire(int idVeterinaire, int idAnimal, string observations);
    void SupprimerAnimalVeterinaire(int idVeterinaire, int idAnimal);
    void ModifierObservations(int idVeterinaire, int idAnimal, string observations);
    string GetObservations(int idVeterinaire, int idAnimal);
}

public interface IListeAnimauxVeterinairesFacade
{
    List<ListeAnimauxVeterinaires> GetListeAnimauxVeterinaires();
    List<Animal> GetAnimauxDeVeterinaire(int idVeterinaire);
    List<Animal> GetAnimauxPasCeVeterinaire(int idVeterinaire);
    List<Veterinaire?> GetVeterinairesByAnimal(int idAnimal);
    void AjouterAnimalVeterinaire(int idVeterinaire, int idAnimal, string observations);
    void SupprimerAnimalVeterinaire(int idVeterinaire, int idAnimal);
    void ModifierObservations(int idVeterinaire, int idAnimal, string observations);
    string GetObservations(int idVeterinaire, int idAnimal);
}

public class ListeAnimauxVeterinairesDaoImpl : IListeAnimauxVeterinairesDao
{
    private readonly MySqlConnection _connection;

    public ListeAnimauxVeterinairesDaoImpl(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public List<ListeAnimauxVeterinaires> GetListeAnimauxVeterinaires()
    {
        MySqlCommand command = new MySqlCommand("select idVeterinaire, idAnimal, observations from listeanimauxveterinaires;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<ListeAnimauxVeterinaires> listeAnimauxVeterinairesList = new List<ListeAnimauxVeterinaires>();
        while (reader.Read())
        {
            ListeAnimauxVeterinaires listeAnimauxVeterinaires = new ListeAnimauxVeterinaires();
            listeAnimauxVeterinaires.IdVeterinaire = reader.GetInt32("idVeterinaire");
            listeAnimauxVeterinaires.IdAnimal = reader.GetInt32("idAnimal");
            listeAnimauxVeterinaires.Observations = reader.GetString("obersvations");
            listeAnimauxVeterinairesList.Add(listeAnimauxVeterinaires);
        }

        reader.Close();
        return listeAnimauxVeterinairesList;
    }

    public List<int> GetAnimauxDeVeterinaire(int idVeterinaire)
    {
        MySqlCommand command = new MySqlCommand("select idAnimal from listeanimauxveterinaires where idVeterinaire = @idVeterinaire;", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idAnimaux = new List<int>();
        while (reader.Read())
        {
            idAnimaux.Add(reader.GetInt32("idAnimal"));
        }

        reader.Close();
        return idAnimaux;
    }

    public List<int> GetAnimauxPasCeVeterinaire(int idVeterinaire)
    {
        MySqlCommand command = new MySqlCommand("select id from animal where id != 1 and !EXISTS(select idAnimal from listeanimauxveterinaires where idVeterinaire = @idVeterinaire and idAnimal = animal.id);", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idAnimaux = new List<int>();
        while (reader.Read())
        {
            idAnimaux.Add(reader.GetInt32("id"));
        }

        reader.Close();
        return idAnimaux;
    }

    public List<int> GetVeterinairesByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idVeterinaire from listeanimauxveterinaires where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idVeterinaires = new List<int>();
        while (reader.Read())
        {
            idVeterinaires.Add(reader.GetInt32("idVeterinaire"));
        }

        reader.Close();
        return idVeterinaires;
    }

    public void AjouterAnimalVeterinaire(int idVeterinaire, int idAnimal, string observations)
    {
        MySqlCommand command = new MySqlCommand("insert into listeanimauxveterinaires(idVeterinaire, idAnimal, observations) values(@idVeterinaire, @idAnimal, @observations);", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("observations", observations);
        command.ExecuteNonQuery();
    }

    public void SupprimerAnimalVeterinaire(int idVeterinaire, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("delete from listeanimauxveterinaires where idVeterinaire = @idVeterinaire and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }

    public void ModifierObservations(int idVeterinaire, int idAnimal, string observations)
    {
        MySqlCommand command = new MySqlCommand("update listeanimauxveterinaires set observations = @observations where idVeterinaire = @idVeterinaire and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.Parameters.AddWithValue("observations", observations);
        command.ExecuteNonQuery();
    }

    public string GetObservations(int idVeterinaire, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select observations from listeanimauxveterinaires where idVeterinaire = @idVeterinaire and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idVeterinaire", idVeterinaire);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        string observations = (string)command.ExecuteScalar();
        return observations; 
    }
}

public class ListeAnimauxVeterinairesFacadeImpl : IListeAnimauxVeterinairesFacade
{
    private readonly IListeAnimauxVeterinairesDao _listeAnimauxVeterinairesDao;
    
    private readonly IAnimalFacade _animalFacade;
    private readonly IVeterinaireFacade _veterinaireFacade;

    public ListeAnimauxVeterinairesFacadeImpl(IListeAnimauxVeterinairesDao listeAnimauxVeterinairesDao, IAnimalFacade animalFacade, IVeterinaireFacade veterinaireFacade)
    {
        _listeAnimauxVeterinairesDao = listeAnimauxVeterinairesDao;
        _animalFacade = animalFacade;
        _veterinaireFacade = veterinaireFacade;
    }
    
    public List<ListeAnimauxVeterinaires> GetListeAnimauxVeterinaires()
    {
        return new List<ListeAnimauxVeterinaires>(_listeAnimauxVeterinairesDao.GetListeAnimauxVeterinaires());
    }

    public List<Animal> GetAnimauxDeVeterinaire(int idVeterinaire)
    {
        return _listeAnimauxVeterinairesDao.GetAnimauxDeVeterinaire(idVeterinaire)
            .Where(idAnimal => _animalFacade.GetAnimal(idAnimal) != null)
            .Select(idAnimal => _animalFacade.GetAnimal(idAnimal))
            .ToList()!;
    }

    public List<Animal> GetAnimauxPasCeVeterinaire(int idVeterinaire)
    {
        return _listeAnimauxVeterinairesDao.GetAnimauxPasCeVeterinaire(idVeterinaire)
            .Where(idAnimal => _animalFacade.GetAnimal(idAnimal) != null)
            .Select(idAnimal => _animalFacade.GetAnimal(idAnimal))
            .ToList()!;
    }

    public List<Veterinaire?> GetVeterinairesByAnimal(int idAnimal)
    {
        return _listeAnimauxVeterinairesDao.GetVeterinairesByAnimal(idAnimal)
            .Select(idVeterinaire => _veterinaireFacade.GetVeterinaire(idVeterinaire))
            .Where(veterinaire => veterinaire != null)
            .ToList();
    }

    public void AjouterAnimalVeterinaire(int idVeterinaire, int idAnimal, string observations)
    {
        _listeAnimauxVeterinairesDao.AjouterAnimalVeterinaire(idVeterinaire, idAnimal, observations);
    }

    public void SupprimerAnimalVeterinaire(int idVeterinaire, int idAnimal)
    {
        _listeAnimauxVeterinairesDao.SupprimerAnimalVeterinaire(idVeterinaire, idAnimal);
    }

    public void ModifierObservations(int idVeterinaire, int idAnimal, string observations)
    {
        _listeAnimauxVeterinairesDao.ModifierObservations(idVeterinaire, idAnimal, observations);
    }

    public string GetObservations(int idVeterinaire, int idAnimal)
    {
        return _listeAnimauxVeterinairesDao.GetObservations(idVeterinaire, idAnimal);
    }
}