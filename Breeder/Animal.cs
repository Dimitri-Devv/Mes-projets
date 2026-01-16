using Breeder.Filter;

using MySql.Data.MySqlClient;

namespace Breeder;

public class Animal
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public DateTime DateNaissance { get; set; }

    public decimal Poids { get; set; }

    public SexeEnum Sexe { get; set; }

    public int IdRace { get; set; }

    public int IdType { get; set; }

    public int IdStatut { get; set; }

    public int IdPere { get; set; }

    public int IdMere { get; set; }

    public override string ToString()
    {
        return Nom + " " + Prenom;
    }

    public enum SexeEnum
    {
        INCONNU,
        MALE,
        FEMELLE
    }
    
    public override bool Equals(object obj) => Equals(obj as Animal);

    public bool Equals(Animal? statut)
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

    public static bool operator ==(Animal? lhs, Animal? rhs)
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

    public static bool operator !=(Animal? lhs, Animal? rhs) => !(lhs == rhs);
}

public enum AnimalFilter
{
    Id,
    Sexe
}

public class AnimalFilterFieldAdapter : IFilterFieldAdapter<AnimalFilter>
{
    public string? GetField(AnimalFilter filter)
    {
        return filter switch
        {
            AnimalFilter.Id => "id",
            AnimalFilter.Sexe => "sexe",
            _ => null
        };
    }
}

public class AnimalFilterReadStrategy : IFilterReadStrategy<Animal>
{
    public Animal Read(MySqlDataReader reader)
    {
        Animal animal = new Animal();
        animal.Id = reader.GetInt32("id");
        animal.Nom = reader.GetString("nom");
        animal.Prenom = reader.GetString("prenom");
        animal.Sexe = sexeToEnum(reader.GetString("sexe"));
        animal.DateNaissance = reader.GetDateTime("dateNaissance");
        animal.Poids = reader.GetDecimal("poidsActuel");
        animal.IdStatut = reader.GetInt32("idStatut");
        animal.IdType = reader.GetInt32("idType");
        animal.IdRace = reader.GetInt32("idRace");
        animal.IdMere = reader.GetInt32("idMere");
        animal.IdPere = reader.GetInt32("idPere");
        return animal;
    }

    private Animal.SexeEnum sexeToEnum(string sexe)
    {
        return sexe == "Inconnu" ? Animal.SexeEnum.INCONNU :
            sexe == "Mâle" ? Animal.SexeEnum.MALE : Animal.SexeEnum.FEMELLE;
    }
}

public interface IAnimalDao : InconnuRequired
{
    List<Animal> GetAnimaux();

    List<Animal> GetAnimauxWithFilters(IFilterFieldValue<AnimalFilter>[] filterFieldValue,
        IFilterBehaviourValue[] filterBehaviourValues);

    Animal? GetAnimal(int id);
    
    List<int> GetIdEnfantsPortees(int idPortee);

    void AjouterAnimal(string nom, string prenom, DateTime dateNaissance, Animal.SexeEnum sexe, decimal poidsActuel,
        int idStatut, int idPere, int idMere, int idRace, int idType);

    void SupprimerAnimal(int id);
    void ModifierAnimal(Animal animal);

    void ModifierStatut(int idAnimal, int idStatut);
    void ModifierRace(int idAnimal, int idRace);
    void ModifierType(int idAnimal, int idTypeAnimal);
}

public interface IAnimalFacade : InconnuRequired, InconnuProvider<Animal>
{
    List<Animal> GetAnimaux();
    List<Animal> GetAnimauxSansInconnu();
    List<Animal> GetAnimauxBySexe(Animal.SexeEnum sexe);
    List<Animal> GetMeresPortees();
    List<Animal> GetAnimauxRecents();
    List<Animal> GetAnimauxPoidsPasAJour();
    List<Animal> GetEnfantsPortees(int idPortee);

    Animal? GetAnimal(int id);


    void AjouterAnimal(string nom, string prenom, DateTime dateNaissance, Animal.SexeEnum sexe, decimal poidsActuel,
        int idStatut, int idPere, int idMere, int idRace, int idType);

    void SupprimerAnimal(int id);
    void ModifierAnimal(Animal animal);

    void ModifierStatut(int idAnimal, int idStatut);
    void ModifierRace(int idAnimal, int idRace);
    void ModifierType(int idAnimal, int idTypeAnimal);
}

public class AnimalDaoImpl : IAnimalDao
{
    private readonly MySqlConnection _connection;
    private readonly IFilterProvider<Animal, AnimalFilter> _filterProvider;

    public AnimalDaoImpl(MySqlConnection connection, IFilterProvider<Animal, AnimalFilter> filterProvider)
    {
        _connection = connection;
        _filterProvider = filterProvider;
    }

    public void AjouterInconnu()
    {
        MySqlCommand command = new MySqlCommand(
            "insert into animal(id, nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace, idType) " +
            "values(@id, @nom, @prenom, @dateNaissance, @sexe, @poids, @idStatut, @idPere, @idMere, @idRace, @idType);",
            _connection);
        command.Parameters.AddWithValue("id", 1);
        command.Parameters.AddWithValue("nom", "Inconnu");
        command.Parameters.AddWithValue("prenom", "Inconnu");
        command.Parameters.AddWithValue("dateNaissance", DateTime.Today);
        command.Parameters.AddWithValue("sexe", "Inconnu");
        command.Parameters.AddWithValue("poids", 0);
        command.Parameters.AddWithValue("idStatut", 1);
        command.Parameters.AddWithValue("idPere", 1);
        command.Parameters.AddWithValue("idMere", 1);
        command.Parameters.AddWithValue("idRace", 1);
        command.Parameters.AddWithValue("idType", 1);
        command.ExecuteNonQuery();
    }

    public List<Animal> GetAnimaux()
    {
        MySqlCommand command = new();
        command.Connection = _connection;
        command.CommandText =
            "select id, nom, prenom, datenaissance, sexe, poidsactuel, idstatut, idpere, idmere, idrace, idtype from animal;";

        MySqlDataReader reader = command.ExecuteReader();

        List<Animal> animals = new List<Animal>();
        while (reader.Read())
        {
            Animal animal = new Animal();
            animal.Id = reader.GetInt32("id");
            animal.Nom = reader.GetString("nom");
            animal.Prenom = reader.GetString("prenom");
            animal.Sexe = sexeToEnum(reader.GetString("sexe"));
            animal.DateNaissance = reader.GetDateTime("dateNaissance");
            animal.Poids = reader.GetDecimal("poidsActuel");
            animal.IdStatut = reader.GetInt32("idStatut");
            animal.IdType = reader.GetInt32("idType");
            animal.IdRace = reader.GetInt32("idRace");
            animal.IdMere = reader.GetInt32("idMere");
            animal.IdPere = reader.GetInt32("idPere");

            animals.Add(animal);
        }

        reader.Close();
        return animals;
    }

    public List<Animal> GetAnimauxWithFilters(IFilterFieldValue<AnimalFilter>[] filterFieldValue, IFilterBehaviourValue[] filterBehaviourValues)
    {
        return _filterProvider.GetWithFilters(
            "select id, nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace, idType from animal where ",
            filterFieldValue, filterBehaviourValues);
    }

    public Animal? GetAnimal(int id)
    {
        MySqlCommand command =
            new MySqlCommand(
                "select nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace, idType from animal where id = @id;",
                _connection);
        command.Parameters.AddWithValue("id", id);
        MySqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            reader.Close();
            return null;
        }

        Animal animal = new Animal();
        animal.Id = id;
        while (reader.Read())
        {
            animal.Nom = reader.GetString("nom");
            animal.Prenom = reader.GetString("prenom");
            string sexe = reader.GetString("sexe");
            animal.Sexe = sexe == "Inconnu" ? Animal.SexeEnum.INCONNU :
                sexe == "Mâle" ? Animal.SexeEnum.MALE : Animal.SexeEnum.FEMELLE;
            animal.DateNaissance = reader.GetDateTime("dateNaissance");
            animal.Poids = reader.GetDecimal("poidsActuel");
            animal.IdStatut = reader.GetInt32("idStatut");
            animal.IdType = reader.GetInt32("idType");
            animal.IdRace = reader.GetInt32("idRace");
            animal.IdMere = reader.GetInt32("idMere");
            animal.IdPere = reader.GetInt32("idPere");
        }

        reader.Close();
        return animal;
    }

    public List<int> GetIdEnfantsPortees(int idPortee)
    {
        MySqlCommand command = new MySqlCommand();
        command.Connection = _connection;
        command.CommandText = "select id from animal where idMere = (select idAnimal from portee where id = @idPortee and datePortee = dateNaissance);";
        command.Parameters.AddWithValue("idPortee", idPortee);
        MySqlDataReader reader = command.ExecuteReader();

        List<int> idAnimaux = new List<int>();
        while (reader.Read())
        {
            idAnimaux.Add(reader.GetInt32("id"));
        }
        
        reader.Close();
        return idAnimaux;
    }

    public void AjouterAnimal(string nom, string prenom, DateTime dateNaissance, Animal.SexeEnum sexe,
        decimal poidsActuel, int idStatut,
        int idPere, int idMere, int idRace, int idType)
    {
        MySqlCommand command = new MySqlCommand(
            "insert into animal(nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace, idType) " +
            "values(@nom, @prenom, @dateNaissance, @sexe, @poids, @idStatut, @idPere, @idMere, @idRace, @idType);",
            _connection);
        command.Parameters.AddWithValue("nom", nom);
        command.Parameters.AddWithValue("prenom", prenom);
        command.Parameters.AddWithValue("dateNaissance", dateNaissance);
        command.Parameters.AddWithValue("sexe", sexe.ToString());
        command.Parameters.AddWithValue("poids", poidsActuel);
        command.Parameters.AddWithValue("idStatut", idStatut);
        command.Parameters.AddWithValue("idPere", idPere);
        command.Parameters.AddWithValue("idMere", idMere);
        command.Parameters.AddWithValue("idRace", idRace);
        command.Parameters.AddWithValue("idType", idType);
        command.ExecuteNonQuery();
    }

    public void SupprimerAnimal(int id)
    {
        MySqlCommand command = new MySqlCommand("delete from animal where id = @id;", _connection);
        command.Parameters.AddWithValue("id", id);
        command.ExecuteNonQuery();
    }

    public void ModifierAnimal(Animal animal)
    {
        MySqlCommand command = new MySqlCommand();
        command.Connection = _connection;
        command.CommandText =
            "update animal set nom = @nom, prenom = @prenom, dateNaissance = @dateNaissance, sexe = @sexe, poidsActuel = @poidsActuel, " +
            "idStatut = @idStatut, idPere = @idPere, idMere = @idMere, idRace = @idRace, idType = @idType where id = @id;";
        
        command.Parameters.AddWithValue("nom", animal.Nom);
        command.Parameters.AddWithValue("prenom", animal.Prenom);
        command.Parameters.AddWithValue("dateNaissance", animal.DateNaissance);
        command.Parameters.AddWithValue("sexe", animal.Sexe);
        command.Parameters.AddWithValue("poidsActuel", animal.Poids);
        command.Parameters.AddWithValue("idStatut", animal.IdStatut);
        command.Parameters.AddWithValue("idPere", animal.IdPere);
        command.Parameters.AddWithValue("idMere", animal.IdMere);
        command.Parameters.AddWithValue("idRace", animal.IdRace);
        command.Parameters.AddWithValue("idType", animal.IdType);
        command.Parameters.AddWithValue("id", animal.Id);
        command.ExecuteNonQuery();
    }

    public void ModifierStatut(int idAnimal, int idStatut)
    {
        MySqlCommand command = new MySqlCommand("update animal set idStatut = @idStatut where id = @id;", _connection);
        command.Parameters.AddWithValue("id", idAnimal);
        command.Parameters.AddWithValue("idStatut", idStatut);
        command.ExecuteNonQuery();
    }

    public void ModifierRace(int idAnimal, int idRace)
    {
        MySqlCommand command = new MySqlCommand("update animal set idRace = @idRace where id = @id;", _connection);
        command.Parameters.AddWithValue("id", idAnimal);
        command.Parameters.AddWithValue("idRace", idRace);
        command.ExecuteNonQuery();
    }

    public void ModifierType(int idAnimal, int idTypeAnimal)
    {
        MySqlCommand command = new MySqlCommand("update animal set idType = @idType where id = @id;", _connection);
        command.Parameters.AddWithValue("id", idAnimal);
        command.Parameters.AddWithValue("idType", idTypeAnimal);
        command.ExecuteNonQuery();
    }

    private Animal.SexeEnum sexeToEnum(string sexe)
    {
        return sexe == "Inconnu" ? Animal.SexeEnum.INCONNU :
            sexe == "Mâle" ? Animal.SexeEnum.MALE : Animal.SexeEnum.FEMELLE;
    }
}

public class AnimalFacadeImpl : IAnimalFacade
{
    private readonly IAnimalDao _animalDao;
    private readonly IPorteeFacade _porteeFacade;
    private readonly IPoidsFacade _poidsFacade;

    public AnimalFacadeImpl(IAnimalDao animalDao, IPorteeFacade porteeFacade, IPoidsFacade poidsFacade)
    {
        _animalDao = animalDao;
        _porteeFacade = porteeFacade;
        _poidsFacade = poidsFacade;
    }

    public void AjouterInconnu()
    {
        _animalDao.AjouterInconnu();
    }

    public Animal GetInconnu()
    {
        Animal? animal = GetAnimal(1);
        if (animal != null)
        {
            return animal;
        }

        AjouterInconnu();
        animal = GetAnimal(1) ?? throw new Exception(string.Format(MessageUtils.Erreur_Inconnu, "Animal"));
        return animal;
    }

    public List<Animal> GetAnimaux()
    {
        return _animalDao.GetAnimaux();
    }

    public List<Animal> GetAnimauxBySexe(Animal.SexeEnum sexe)
    {
        return _animalDao.GetAnimauxWithFilters(
            new IFilterFieldValue<AnimalFilter>[]
            {
                AbstractFilterFieldValue<AnimalFilter>.Of(AnimalFilter.Sexe, FilterOperation.EQUAL, sexe.ToString())
            },
            Array.Empty<IFilterBehaviourValue>()
        );
    }

    public List<Animal> GetMeresPortees()
    {
        return _animalDao.GetAnimauxWithFilters(
            new IFilterFieldValue<AnimalFilter>[]
                { AbstractFilterFieldValue<AnimalFilter>.Of(AnimalFilter.Id, FilterOperation.DIFFERENT, "1") },
            Array.Empty<IFilterBehaviourValue>()
        ).Where(animal => _porteeFacade.GetPorteesByAnimal(animal.Id).Any()).ToList();
    }

    public List<Animal> GetAnimauxRecents()
    {
        return _animalDao.GetAnimauxWithFilters(
            new IFilterFieldValue<AnimalFilter>[]
                { AbstractFilterFieldValue<AnimalFilter>.Of(AnimalFilter.Id, FilterOperation.DIFFERENT, "1") },
            new[]
            {
                FilterBehaviourValue.Of(FilterBehaviour.Order_By_Desc, "id"),
                FilterBehaviourValue.Of(FilterBehaviour.Limit, "5")
            });
    }

    public List<Animal> GetAnimauxPoidsPasAJour()
    {
        return _animalDao.GetAnimauxWithFilters(
            new IFilterFieldValue<AnimalFilter>[]
                { AbstractFilterFieldValue<AnimalFilter>.Of(AnimalFilter.Id, FilterOperation.DIFFERENT, "1") },
            Array.Empty<IFilterBehaviourValue>()
        ).Where(animal =>
        {
            List<CourbePoids> courbesPoids =
                FacadeProvider.GetInstance().PoidsFacade().GetCourbesPoids(animal.Id).ToList();
            return courbesPoids.Count > 0 && courbesPoids.Count <= _poidsFacade.GetTempsPeriodeObservation() &&
                   courbesPoids.Last().DateSaisie < DateTime.Today;
        }).ToList();
    }

    public List<Animal> GetEnfantsPortees(int idPortee)
    {
        List<Animal> enfants = new List<Animal>();
        foreach (int idEnfant in _animalDao.GetIdEnfantsPortees(idPortee))
        {
            Animal? enfant = GetAnimal(idEnfant);
            if (enfant == null)
            {
                continue;
            }
            enfants.Add(enfant);
        }

        return enfants;
    }

    public List<Animal> GetAnimauxSansInconnu()
    {
        return _animalDao.GetAnimauxWithFilters(
            new IFilterFieldValue<AnimalFilter>[]
                { AbstractFilterFieldValue<AnimalFilter>.Of(AnimalFilter.Id, FilterOperation.DIFFERENT, "1") },
            Array.Empty<IFilterBehaviourValue>());
    }

    public Animal? GetAnimal(int id)
    {
        return _animalDao.GetAnimal(id);
    }

    public void AjouterAnimal(string nom, string prenom, DateTime dateNaissance, Animal.SexeEnum sexe,
        decimal poidsActuel, int idStatut,
        int idPere, int idMere, int idRace, int idType)
    {
        _animalDao.AjouterAnimal(nom, prenom, dateNaissance, sexe, poidsActuel, idStatut, idPere, idMere, idRace,
            idType);
    }

    public void SupprimerAnimal(int id)
    {
        _animalDao.SupprimerAnimal(id);
    }

    public void ModifierAnimal(Animal animal)
    {
        _animalDao.ModifierAnimal(animal);
    }

    public void ModifierStatut(int idAnimal, int idStatut)
    {
        _animalDao.ModifierStatut(idAnimal, idStatut);
    }

    public void ModifierRace(int idAnimal, int idRace)
    {
        _animalDao.ModifierRace(idAnimal, idRace);
    }

    public void ModifierType(int idAnimal, int idTypeAnimal)
    {
        _animalDao.ModifierType(idAnimal, idTypeAnimal);
    }
}