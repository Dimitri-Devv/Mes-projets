


namespace Breeder;

public class AnimalFacadeImpl2
{
    /*
    private readonly ICacheAnimalDAO _cacheAnimalDAO;
    private readonly IDatabaseAnimalDAO _databaseAnimalDAO;

    public AnimalFacadeImpl(ICacheAnimalDAO cacheAnimalDao, IDatabaseAnimalDAO databaseAnimalDao)
    {
        _cacheAnimalDAO = cacheAnimalDao;
        _databaseAnimalDAO = databaseAnimalDao;
    }

    public Dictionary<int, Animal> GetAnimaux()
    {
        return _cacheAnimalDAO.GetAll();
    }

    public List<Animal> GetAnimauxSansInconnu()
    {
        return GetAnimaux().Values.Where(animal => animal.Id != 1).ToList();
    }

    public Animal GetAnimal(int id)
    {
        return _cacheAnimalDAO.Get(id);
    }

    public void AjouterAnimal(string nom, string prenom, DateTime dateNaissance, Animal.SexeEnum sexe,
        decimal poidsActuel, int idStatut,
        int idPere, int idMere, int idRace, int idType)
    {
        Animal animal = new Animal(); // Création de l'objet animal
        animal.Nom = nom;
        animal.Prenom = prenom;
        animal.DateNaissance = dateNaissance;
        animal.Sexe = sexe;
        animal.Poids = poidsActuel;
        animal.IdStatut = idStatut;
        animal.IdPere = idPere;
        animal.IdMere = idMere;
        animal.IdRace = idRace;
        animal.IdType = idType;

        _databaseAnimalDAO.Add(animal); // On ajoute l'animal à la base de données
        _cacheAnimalDAO.Reload(_databaseAnimalDAO); // On récupère les animaux de la base de données en cache

        int lastAddedAnimal = _databaseAnimalDAO.GetLastAddedAnimal();
        FacadeProvider.GetInstance().FamilleFacade().AjouterFamilles(_cacheAnimalDAO.Get(lastAddedAnimal));
    }

    public void SupprimerAnimal(int id)
    {
        FacadeProvider.GetInstance().FamilleFacade().SupprimerFamillesByAnimal(id);
        _cacheAnimalDAO.Remove(id);
        _databaseAnimalDAO.Remove(id);
    }

    public void ModifierAnimal(Animal animal)
    {
        _databaseAnimalDAO.UpdateAnimal(animal);
    }

    public void ModifierStatut(int idAnimal, int idStatut)
    {
        _cacheAnimalDAO.UpdateStatus(idAnimal, idStatut);
        _databaseAnimalDAO.UpdateStatus(idAnimal, idStatut);
    }

    public void ModifierRace(Animal animal, Race race)
    {
        _cacheAnimalDAO.UpdateRace(animal, race);
        _databaseAnimalDAO.UpdateRace(animal, race);
    }

    public void ModifierType(Animal animal, TypeAnimal typeAnimal)
    {
        _cacheAnimalDAO.UpdateType(animal, typeAnimal);
        _databaseAnimalDAO.UpdateType(animal, typeAnimal);
    }

    public int GetLastAddedAnimal()
    {
        return _databaseAnimalDAO.GetLastAddedAnimal();
    }

    public List<Animal> GetEnfantsPortees(int idPortee)
    {
        Portee portee = FacadeProvider.GetInstance().PorteeFacade().GetPortee(idPortee);
        return FacadeProvider.GetInstance().FamilleFacade().GetEnfantsByMere(portee.IdAnimal)
            .Where(animal => animal.DateNaissance == portee.Date).ToList();
    }

    public List<Animal> GetMeresPortees()
    {
        return _cacheAnimalDAO.GetAll().Values
            .Where(animal => animal.Id != 1 && animal.Sexe == Animal.SexeEnum.FEMELLE &&
                             FacadeProvider.GetInstance().PorteeFacade().GetPorteesByAnimal(animal.Id).Count > 0)
            .ToList();
    }

    public List<Animal> GetMales()
    {
        return _cacheAnimalDAO.GetAll().Values.Where(animal => animal.Sexe == Animal.SexeEnum.MALE).ToList();
    }

    public List<Animal> GetFemelles()
    {
        return _cacheAnimalDAO.GetAll().Values.Where(animal => animal.Sexe == Animal.SexeEnum.FEMELLE).ToList();
    }

    public List<Animal> GetAnimauxRecents()
    {
        return GetAnimaux().Values.Where(animal => animal.Id != 1).OrderBy(animal => animal.Id).TakeLast(5).Reverse().ToList();
        IEnumerable<Animal> animals = GetAnimaux().Values.Where(animal => animal.Id != 1);
        return animals.Skip(Math.Max(0, animals.Count() - 7)).ToList();
    }

    public List<Animal> GetAnimauxPoidsPasAJour()
    {
        return GetAnimaux().Values.Where(animal =>
        {
            if (animal.Id == 1)
            {
                return false;
            }
            
            List<CourbePoids> courbesPoids = FacadeProvider.GetInstance().PoidsFacade().GetCourbesPoids(animal.Id);
            if (courbesPoids.Count <= 0 || courbesPoids.Count > FacadeProvider.GetInstance().PoidsFacade().GetTempsPeriodeObservation() ||
                courbesPoids.Last().DateSaisie >= DateTime.Today)
            {
                return false;
            }

            return true;
        }).ToList();
    }

    public void AjouterInconnu()
    {
        throw new NotImplementedException();
    }

    public Animal GetInconnu()
    {
        throw new NotImplementedException();
    }
    */
}