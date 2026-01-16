using Breeder.Filter;
using MySql.Data.MySqlClient;

namespace Breeder;

public class Famille
{
    public int Id { get; set; }
    public int IdAnimal { get; set; }
    public int IdRole { get; set; }

    public override bool Equals(object obj) => Equals(obj as Famille);

    public bool Equals(Famille? statut)
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
        return (Id == statut.Id && IdAnimal == statut.IdAnimal && IdRole == statut.IdRole);
    }

    public override int GetHashCode() => (Id, IdAnimal, IdRole).GetHashCode();

    public static bool operator ==(Famille? lhs, Famille? rhs)
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

    public static bool operator !=(Famille? lhs, Famille? rhs) => !(lhs == rhs);
}

public enum FamilleFilter
{
    Id,
    Id_Animal,
    Id_Role
}

public class FamilleFilterFieldAdapter : IFilterFieldAdapter<FamilleFilter>
{
    public string? GetField(FamilleFilter filter)
    {
        return filter switch
        {
            FamilleFilter.Id => "id",
            FamilleFilter.Id_Animal => "idAnimal",
            FamilleFilter.Id_Role => "idRole",
            _ => null
        };
    }
}

public class FamilleFilterReadStrategy : IFilterReadStrategy<Famille>
{
    public Famille Read(MySqlDataReader reader)
    {
        Famille famille = new Famille();
        famille.Id = reader.GetInt32("id");
        famille.IdAnimal = reader.GetInt32("idAnimal");
        famille.IdRole = reader.GetInt32("idRole");
        return famille;
    }
}

public interface IFamilleDao
{
    /*
    List<Famille> GetFamilles();
    List<Famille> GetFamillesWithFilters(IFilterFieldValue<FamilleFilter>[] familleFilters, IFilterBehaviourValue[] behavioursFilters);
    */
    
    List<int> GetIdEnfants(int idPere, int idMere);
    /*
    List<Famille> GetFamilleByIdAndAnimal(int id, int idAnimal);
    List<Famille> GetFamilleByAnimalAndRole(int idAnimal, int idRole);
    List<Famille> GetFamilleById(int id);
    List<Famille> GetFamilleByAnimal(int idAnimal);
    */
    
    /*
    int? GetIdRole(int idFamille, int idAnimal);

    Famille? GetFamille(int id, int idAnimal, int idRole);
    
    void AjouterFamilles(Animal animal);
    void SupprimerFamillesByAnimal(int idAnimal);

 */
}

public interface IFamilleFacade
{
    //List<Famille> GetFamilles();
    
    //List<Famille> GetFamillesById(int id);
    //List<Famille> GetFamillesByAnimal(int idAnimal);
    //List<Famille> GetFamillesByIdAndAnimal(int id, int idAnimal);
    //List<Famille> GetFamillesByAnimalAndRole(int idAnimal, int idRole);

    List<Animal> GetEnfants(int idPere, int idMere);
    //List<Animal> GetEnfantsByMere(int idMere);

    //Role? GetRole(int idFamille, int idAnimal);

    //Famille? GetFamille(int id, int idAnimal, int idRole);
    //Famille? GetFamilleFromParents(int idMere, int idPere);

    //void SupprimerFamillesByAnimal(int idAnimal);
    //void AjouterFamilles(Animal animal);
}


public class FamilleDaoImpl : IFamilleDao
{
    private readonly MySqlConnection _connection;
    private readonly IFilterProvider<Famille, FamilleFilter> _filterProvider;

    public FamilleDaoImpl(MySqlConnection connection, IFilterProvider<Famille, FamilleFilter> filterProvider)
    {
        _connection = connection;
        _filterProvider = filterProvider;
    }

    public List<Famille> GetFamilles()
    {
        MySqlCommand command = new MySqlCommand("select id, idAnimal, idRole from famille;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        List<Famille> familles = new List<Famille>();
        while (reader.Read())
        {
            Famille famille = new Famille();
            famille.Id = reader.GetInt32("id");
            famille.IdAnimal = reader.GetInt32("idAnimal");
            famille.IdRole = reader.GetInt32("idRole");
            familles.Add(famille);
        }

        reader.Close();
        return familles;
    }

    public List<Famille> GetFamillesWithFilters(IFilterFieldValue<FamilleFilter>[] familleFilters, IFilterBehaviourValue[] behavioursFilters)
    {
        return _filterProvider.GetWithFilters("select id, idAnimal, idRole from famille where ", familleFilters, behavioursFilters);
    }

    public List<int> GetIdEnfants(int idPere, int idMere)
    {
        MySqlCommand command = new MySqlCommand();
        command.Connection = _connection;
        command.CommandText =
            "select idAnimal from famille where id in (select id from famille where (idRole = 2 and idAnimal = @idPere) or (idRole = 3 and idAnimal = @idMere)) and idRole = 4;";
        command.Parameters.AddWithValue("idPere", idPere);
        command.Parameters.AddWithValue("idMere", idMere);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idEnfants = new List<int>();
        while (reader.Read())
        {
            idEnfants.Add(reader.GetInt32("idAnimal"));
        }

        reader.Close();
        return idEnfants;
    }

    public int? GetIdRole(int idFamille, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idRole from famille where id = @id and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("id", idFamille);
        command.Parameters.AddWithValue("idAnimal", idAnimal);

        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        int id = reader.GetInt32("idRole");
        reader.Close();
        
        return id;
    }

    public Famille GetFamille(int id, int idAnimal, int idRole)
    {
        MySqlCommand command = new MySqlCommand("select id, idAnimal, idRole from famille;", _connection);
        MySqlDataReader reader = command.ExecuteReader();

        Famille famille = new Famille();
        while (reader.Read())
        {
            famille.Id = reader.GetInt32("id");
            famille.IdAnimal = reader.GetInt32("idAnimal");
            famille.IdRole = reader.GetInt32("idRole");
        }

        reader.Close();
        return famille;
    }

    public void AjouterFamilles(Animal animal)
    {
        
    }

    public List<Famille> GetFamilleByIdAndAnimal(int id, int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select idRole from famille where id = @id and idAnimal = @idAnimal;",
            _connection);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<Famille> familles = new List<Famille>();

        while (reader.Read())
        {
            Famille famille = new Famille();
            famille.Id = id;
            famille.IdAnimal = idAnimal;
            famille.IdRole = reader.GetInt32("idRole");
            familles.Add(famille);
        }

        reader.Close();
        return familles;
    }

    public List<Famille> GetFamilleByAnimalAndRole(int idAnimal, int idRole)
    {
        MySqlCommand command =
            new MySqlCommand("select id from famille where idRole = @idRole and idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idRole", idRole);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<Famille> familles = new List<Famille>();
        while (reader.Read())
        {
            Famille famille = new Famille();
            famille.Id = reader.GetInt32("id");
            famille.IdAnimal = idAnimal;
            famille.IdRole = idRole;
            familles.Add(famille);
        }

        reader.Close();
        return familles;
    }

    public List<Famille> GetFamilleById(int id)
    {
        MySqlCommand command = new MySqlCommand("select idAnimal, idRole from famille where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        List<Famille> familles = new List<Famille>();

        while (reader.Read())
        {
            Famille famille = new Famille();
            famille.Id = id;
            famille.IdAnimal = reader.GetInt32("idAnimal");
            famille.IdRole = reader.GetInt32("idRole");
            familles.Add(famille);
        }

        reader.Close();
        return familles;
    }

    public List<Famille> GetFamilleByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("select id, idRole from famille where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        MySqlDataReader reader = command.ExecuteReader();

        List<Famille> familles = new List<Famille>();
        while (reader.Read())
        {
            Famille famille = new Famille();
            famille.Id = reader.GetInt32("id");
            famille.IdAnimal = idAnimal;
            famille.IdRole = reader.GetInt32("idRole");
            familles.Add(famille);
        }

        reader.Close();
        return familles;
    }

    public void SupprimerFamillesByAnimal(int idAnimal)
    {
        MySqlCommand command = new MySqlCommand("delete from famille where idAnimal = @idAnimal;", _connection);
        command.Parameters.AddWithValue("idAnimal", idAnimal);
        command.ExecuteNonQuery();

        //command.CommandText = "delete from famille where (select count(idRole) from famille where idRole != 4) = 0;";
        //command.ExecuteNonQuery();
    }

    public int GetLastAddedFamille()
    {
        MySqlCommand command = new MySqlCommand("select max(id) from famille;", _connection);
        return (Int32)command.ExecuteScalar();
    }

    public void AddFamille(Famille famille)
    {
        MySqlCommand command = new MySqlCommand("insert into famille(id, idAnimal, idRole) values(@id, @idAnimal, @idRole);", _connection);
        command.Parameters.AddWithValue("id", famille.Id);
        command.Parameters.AddWithValue("idAnimal", famille.IdAnimal);
        command.Parameters.AddWithValue("idRole", famille.IdRole);
        command.ExecuteNonQuery();
    }
}

public class FamilleFacadeImpl : IFamilleFacade
{
    private readonly IFamilleDao _familleDao;
    private readonly IAnimalFacade _animalFacade;
    private readonly IRoleFacade _roleFacade;

    public FamilleFacadeImpl(IFamilleDao familleDao, IAnimalFacade animalFacade, IRoleFacade roleFacade)
    {
        _familleDao = familleDao;
        _animalFacade = animalFacade;
        _roleFacade = roleFacade;
    }
    
    /*

    public List<Famille> GetFamilles()
    {
        return _familleDao.GetFamilles();
    }

    public List<Famille> GetFamillesById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Famille> GetFamillesByAnimal(int idAnimal)
    {
        throw new NotImplementedException();
    }

    public List<Famille> GetFamillesByIdAndAnimal(int id, int idAnimal)
    {
        throw new NotImplementedException();
    }

    public List<Famille> GetFamillesByAnimalAndRole(int idAnimal, int idRole)
    {
        throw new NotImplementedException();
    }

    /*
    public List<Famille> GetFamilleByIdAndAnimal(int id, int idAnimal)
    {
        return _familleDao.GetFamillesWithFilters(
            new IFilterFieldValue<FamilleFilter>[] {
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id, FilterOperation.EQUAL, id.ToString()),
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id_Animal, FilterOperation.EQUAL, idAnimal.ToString())},
            Array.Empty<IFilterBehaviourValue>());
    }

    public List<Famille> GetFamilleByAnimalAndRole(int idAnimal, int idRole)
    {
        return _familleDao.GetFamillesWithFilters(
            new IFilterFieldValue<FamilleFilter>[] {
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id_Role, FilterOperation.EQUAL, idRole.ToString()),
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id_Animal, FilterOperation.EQUAL, idAnimal.ToString())},
            Array.Empty<IFilterBehaviourValue>());
    }

    public List<Famille> GetFamilleById(int id)
    {
        return _familleDao.GetFamillesWithFilters(
            new IFilterFieldValue<FamilleFilter>[] {
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id, FilterOperation.EQUAL, id.ToString())},
            Array.Empty<IFilterBehaviourValue>());
    }

    public List<Famille> GetFamilleByAnimal(int idAnimal)
    {
        return _familleDao.GetFamillesWithFilters(
            new IFilterFieldValue<FamilleFilter>[] {
                AbstractFilterFieldValue<FamilleFilter>.Of(FamilleFilter.Id_Animal, FilterOperation.EQUAL, idAnimal.ToString())},
            Array.Empty<IFilterBehaviourValue>());
    }
    */

    public List<Animal> GetEnfants(int idPere, int idMere)
    {
        List<Animal> enfants = new List<Animal>(); 
        foreach (int idEnfant in _familleDao.GetIdEnfants(idPere, idMere))
        {
            Animal? enfant = _animalFacade.GetAnimal(idEnfant);
            if (enfant == null)
            {
                continue;
            }
            enfants.Add(enfant);
        }

        return enfants;
    }

    /*
    public List<Animal> GetEnfantsByMere(int idMere)
    {
        List<Famille> familles = GetFamilleByAnimal(idMere).Where(famille => famille.IdRole == 3).ToList();
        List<Animal> animals = new List<Animal>();
        
        foreach (Famille famille in familles)
        {
            foreach (int idAnimal in GetFamilleById(famille.Id)
                         .Where(famille => famille.IdRole == 4)
                         .Select(famille => famille.IdAnimal))
            {
                animals.Add(FacadeProvider.GetInstance().AnimalFacade().GetAnimal(idAnimal));
            }
        }

        return animals;
    }

    public Role? GetRole(int idFamille, int idAnimal)
    {
        int? idRole = _familleDao.GetIdRole(idFamille, idAnimal);
        if (idRole == null)
        {
            return null;
        }

        return _roleFacade.GetRole((int)idRole);
    }

    public Famille? GetFamille(int id, int idAnimal, int idRole)
    {
        return _familleDao.GetFamille(id, idAnimal, idRole);
    }

    public Famille? GetFamilleFromParents(int idMere, int idPere)
    {
        
    }

    public void SupprimerFamillesByAnimal(int idAnimal)
    {
        _familleDao.SupprimerFamillesByAnimal(idAnimal);
    }

    public void AjouterFamilles(Animal animal)
    {
        int idFamille = 0;

        List<Famille> famillesMere = GetFamilleByAnimalAndRole(animal.IdMere, 3);
        List<Famille> famillesPere = GetFamilleByAnimalAndRole(animal.IdPere, 2);

        foreach (Famille familleMere in famillesMere)
        {
            foreach (Famille famillePere in famillesPere)
            {
                if (familleMere.Id == famillePere.Id)
                {
                    idFamille = familleMere.Id;
                }
            }
        }
        
        if (idFamille > 0)
        {
            Famille famille = new Famille();
            famille.Id = idFamille;
            famille.IdAnimal = animal.Id;
            famille.IdRole = 4;
            
            Console.WriteLine("Famille déjà existante " + JsonSerializer.Serialize(famille));
            _familleDao.AddFamille(famille);
        }
        else
        {
            int id = _familleDao.GetLastAddedFamille() + 1;
            Famille famille = new Famille();
            famille.Id = id;
            famille.IdAnimal = animal.Id;
            famille.IdRole = 4;

            _familleDao.AddFamille(famille);

            famille.IdAnimal = animal.IdMere;
            famille.IdRole = 3;
            _familleDao.AddFamille(famille);

            famille.IdAnimal = animal.IdPere;
            famille.IdRole = 2;
            _familleDao.AddFamille(famille);
        }
        
        _cacheFamilleDao.Reload(_familleDao.GetFamilles());
    }
    */
}