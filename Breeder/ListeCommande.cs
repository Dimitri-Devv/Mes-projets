using Breeder.Filter;
using MySql.Data.MySqlClient;

namespace Breeder;

public class ListeCommande
{
    public int IdCommande { get; set; }
    public int IdAnimal { get; set; }
    
    public override bool Equals(object obj) => Equals(obj as ListeCommande);

    public bool Equals(ListeCommande? statut)
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
        return (IdCommande == statut.IdCommande && IdAnimal == statut.IdAnimal);
    }

    public override int GetHashCode() => (IdCommande, IdAnimal).GetHashCode();

    public static bool operator ==(ListeCommande? lhs, ListeCommande? rhs)
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

    public static bool operator !=(ListeCommande? lhs, ListeCommande? rhs) => !(lhs == rhs);
}

public class DepenseAnimal
{
    public Commande Commande { get; set; }
    public decimal Cout { get; set; }
    
    public DepenseAnimal() {}
}

public enum ListeCommandeFilter
{
    Id_Commande,
    Id_Animal
}

public class ListeCommandeFilterFieldAdapter : IFilterFieldAdapter<ListeCommandeFilter>
{
    public string? GetField(ListeCommandeFilter filter)
    {
        return filter switch
        {
            ListeCommandeFilter.Id_Commande => "idCommande",
            ListeCommandeFilter.Id_Animal => "idAnimal",
            _ => null
        };
    }
}

public class ListeCommandeFilterReadStrategy : IFilterReadStrategy<ListeCommande>
{
    public ListeCommande Read(MySqlDataReader reader)
    {
        ListeCommande listeCommande = new ListeCommande();
        listeCommande.IdCommande = reader.GetInt32("idCommande");
        listeCommande.IdAnimal = reader.GetInt32("idAnimal");
        return listeCommande;
    }
}

public interface IListeCommandeDao
{
    List<ListeCommande> GetListesCommandes();

    List<ListeCommande> GetListesCommandesWithFilters(IFilterFieldValue<ListeCommandeFilter>[] filterFieldValues, IFilterBehaviourValue[] filterBehaviourValues);

    List<int> GetIdCommandesByAnimal(int idAnimal);
    List<int> GetIdAnimauxByCommande(int idCommande);

    bool CommandeExists(int idCommande, int idAnimal);

    void Commander(int idCommande, List<int> idAnimaux);

    void ModifierListeCommandes(int idCommande, List<int> animaux);
    void SupprimerListeCommande(int idCommande, int idAnimal);
}

public interface IListeCommandeFacade
{
    List<ListeCommande> GetListesCommandes();
    List<ListeCommande> GetListesCommandesByCommande(int idCommande);
    List<Commande> GetCommandesByAnimal(int idAnimal);
    List<Animal> GetAnimauxByCommande(int idCommande);

    List<DepenseAnimal> GetDepensesAnimal(int idAnimal);

    bool CommandeExists(int idCommande, int idAnimal);
    void Commander(int idCommande, List<int> idAnimaux);

    void SupprimerListeCommande(int idCommande, int idAnimal);
    void ModifierListeCommandes(int idCommande, List<int> animaux);
}

public class ListeCommandeDaoImpl : IListeCommandeDao
{
    private readonly MySqlConnection _connection;
    private readonly IFilterProvider<ListeCommande, ListeCommandeFilter> _filterProvider;

    public ListeCommandeDaoImpl(MySqlConnection connection, IFilterProvider<ListeCommande, ListeCommandeFilter> filterProvider)
    {
        _connection = connection;
        _filterProvider = filterProvider;
    }

    public List<ListeCommande> GetListesCommandes()
    {
        MySqlCommand command = new MySqlCommand("select idCommande, idAnimal from listecommande;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<ListeCommande> listeCommandes = new List<ListeCommande>();
        while (reader.Read())
        {
            ListeCommande listeCommande = new ListeCommande();
            listeCommande.IdCommande = reader.GetInt32("idCommande");
            listeCommande.IdAnimal = reader.GetInt32("idAnimal");
            listeCommandes.Add(listeCommande);
        }

        reader.Close();
        return listeCommandes;
    }

    public List<ListeCommande> GetListesCommandesWithFilters(
        IFilterFieldValue<ListeCommandeFilter>[] filterFieldValues,
        IFilterBehaviourValue[] filterBehaviourValues)
    {
        return _filterProvider.GetWithFilters("select idCommande, idAnimal from listecommande where ",
            filterFieldValues, filterBehaviourValues);
    }

    public List<int> GetIdCommandesByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idCommande from listecommande where idAnimal = @idAnimal;",
            _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idCommandes = new List<int>();
        while (reader.Read())
        {
            idCommandes.Add(reader.GetInt32("idCommande"));
        }

        reader.Close();
        return idCommandes;
    }

    public List<int> GetIdAnimauxByCommande(int idCommande)
    {
        MySqlCommand command = new MySqlCommand("select idAnimal from listecommande where idCommande = @idCommande;",
            _connection);
        command.Parameters.AddWithValue("idCommande", idCommande);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idAnimaux = new List<int>();
        while (reader.Read())
        {
            idAnimaux.Add(reader.GetInt32("idAnimal"));
        }

        reader.Close();
        return idAnimaux;
    }

    public bool CommandeExists(int idCommande, int idAnimal)
    {
        MySqlCommand command =
            new MySqlCommand(
                "select exists(select 1 from listecommande where idCommande = @idCommande and idAnimal = @idAnimal);",
                _connection);
        command.Parameters.AddWithValue("idCommande", idCommande);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        return (int)command.ExecuteScalar() == 1;
    }

    public void Commander(int idCommande, List<int> idAnimaux)
    {
        MySqlCommand command =
            new MySqlCommand("insert into listecommande(idCommande, idAnimal) values (@idCommande, @idAnimal);",
                _connection);
        command.Parameters.AddWithValue("idCommande", idCommande);
        foreach (int idAnimal in idAnimaux)
        {
            if (command.Parameters.Contains("idAnimal"))
            {
                command.Parameters.RemoveAt("idAnimal");
            }

            command.Parameters.AddWithValue("idAnimal", idAnimal);
            command.ExecuteNonQuery();
        }
    }

    public void ModifierListeCommandes(int idCommande, List<int> animaux)
    {
        
    }

    public void SupprimerListeCommande(int idCommande, int idAnimal)
    {
        MySqlCommand command =
            new MySqlCommand("delete from listecommande where idCommande = @idCommande and idAnimal = @idAnimal;",
                _connection);
        command.Parameters.AddWithValue("idCommande", idCommande);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();
    }
}

public class ListeCommandeFacadeImpl : IListeCommandeFacade
{
    private readonly IListeCommandeDao _listeCommandeDao;
    private readonly ICommandeFacade _commandeFacade;
    private readonly IAnimalFacade _animalFacade;

    public ListeCommandeFacadeImpl(IListeCommandeDao listeCommandeDao, ICommandeFacade commandeFacade, IAnimalFacade animalFacade)
    {
        _listeCommandeDao = listeCommandeDao;
        _commandeFacade = commandeFacade;
        _animalFacade = animalFacade;
    }

    public List<ListeCommande> GetListesCommandes()
    {
        return _listeCommandeDao.GetListesCommandes();
    }

    public List<ListeCommande> GetListesCommandesByCommande(int idCommande)
    {
        return _listeCommandeDao.GetListesCommandesWithFilters(
            new IFilterFieldValue<ListeCommandeFilter>[]
            {
                AbstractFilterFieldValue<ListeCommandeFilter>.Of(ListeCommandeFilter.Id_Commande, FilterOperation.EQUAL,
                    idCommande.ToString())
            },
            Array.Empty<IFilterBehaviourValue>()
        );
    }

    public List<Commande> GetCommandesByAnimal(int idAnimal)
    {
        List<Commande> commandes = new List<Commande>();
        foreach (int idCommande in _listeCommandeDao.GetIdCommandesByAnimal(idAnimal))
        {
            Commande? commande = _commandeFacade.GetCommande(idCommande);
            if (commande != null)
            {
                commandes.Add(commande);
            }
        }

        return commandes;
    }

    public List<Animal> GetAnimauxByCommande(int idCommande)
    {
        List<Animal> animaux = new List<Animal>();
        foreach (int idAnimal in _listeCommandeDao.GetIdAnimauxByCommande(idCommande))
        {
            Animal? animal = _animalFacade.GetAnimal(idAnimal);
            if (animal != null)
            {
                animaux.Add(animal);
            }
        }

        return animaux;
    }

    public List<DepenseAnimal> GetDepensesAnimal(int idAnimal)
    {
        List<DepenseAnimal> depensesAnimals = new List<DepenseAnimal>();
        
        foreach (Commande commande in GetCommandesByAnimal(idAnimal))
        {
            decimal cout = commande.Total / GetListesCommandesByCommande(commande.Id).Count(listeCommande => listeCommande.IdAnimal != idAnimal);
            depensesAnimals.Add(new DepenseAnimal { Commande = commande, Cout = cout});
        }

        return depensesAnimals;
    }

    public bool CommandeExists(int idCommande, int idAnimal)
    {
        return _listeCommandeDao.CommandeExists(idCommande, idAnimal);
    }

    public void Commander(int idCommande, List<int> idAnimaux)
    {
        _listeCommandeDao.Commander(idCommande, idAnimaux);
    }

    public void SupprimerListeCommande(int idCommande, int idAnimal)
    {
        _listeCommandeDao.SupprimerListeCommande(idCommande, idAnimal);
    }

    public void ModifierListeCommandes(int idCommande, List<int> animaux)
    {
        _listeCommandeDao.ModifierListeCommandes(idCommande, animaux);
    }
}