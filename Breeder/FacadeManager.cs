namespace Breeder;

public interface IFacades
{
    IStatutFacade StatutFacade();
    ITypeFacade TypeFacade();
    IRaceFacade RaceFacade();
    IFamilleFacade FamilleFacade();
    IPorteeFacade PorteeFacade();
    INiveauFacade NiveauFacade();
    IAnimalFacade AnimalFacade();
    IClientFacade ClientFacade();
    IClientAnimalFacade ClientAnimalFacade();
    IPoidsFacade PoidsFacade();
    IFournisseurFacade FournisseurFacade();
    ICommandeFacade CommandeFacade();
    IListeCommandeFacade ListeCommandeFacade();
    IVaccinFacade VaccinFacade();
    IVeterinaireFacade VeterinaireFacade();
    IListeAnimauxVaccinsFacade ListeAnimauxVaccinsFacade();
    IListeAnimauxVeterinairesFacade ListeAnimauxVeterinairesFacade();
    IUserFacade UserFacade();
}


public class FacadeProvider : IFacades
{
    private static IFacades _instance;
    
    private readonly IStatutFacade _statutFacade;
    private readonly ITypeFacade _typeFacade;
    private readonly IRaceFacade _raceFacade;
    private readonly IFamilleFacade _familleFacade;
    private readonly IPorteeFacade _porteeFacade;
    private readonly INiveauFacade _niveauFacade;
    private readonly IAnimalFacade _animalFacade;
    private readonly IClientFacade _clientFacade;
    private readonly IClientAnimalFacade _clientAnimalFacade;
    private readonly IPoidsFacade _poidsFacade;
    private readonly IFournisseurFacade _fournisseurFacade;
    private readonly ICommandeFacade _commandeFacade;
    private readonly IListeCommandeFacade _listeCommandeFacade;
    private readonly IVaccinFacade _vaccinFacade;
    private readonly IVeterinaireFacade _veterinaireFacade;
    private readonly IListeAnimauxVaccinsFacade _listeAnimauxVaccinsFacade;
    private readonly IListeAnimauxVeterinairesFacade _listeAnimauxVeterinairesFacade;
    private readonly IUserFacade _userFacade;

    private FacadeProvider(IStatutFacade statutFacade, ITypeFacade typeFacade, IRaceFacade raceFacade, IFamilleFacade familleFacade, 
        IPorteeFacade porteeFacade, INiveauFacade niveauFacade, IAnimalFacade animalFacade, IClientFacade clientFacade, IClientAnimalFacade clientAnimalFacade,
        IPoidsFacade poidsFacade, IFournisseurFacade fournisseurFacade, ICommandeFacade commandeFacade, IListeCommandeFacade listeCommandeFacade,
        IVaccinFacade vaccinFacade, IVeterinaireFacade veterinaireFacade, IListeAnimauxVaccinsFacade listeAnimauxVaccinsFacade, IListeAnimauxVeterinairesFacade listeAnimauxVeterinairesFacade,
        IUserFacade userFacade)
    {
        _statutFacade = statutFacade;
        _typeFacade = typeFacade;
        _raceFacade = raceFacade;
        _familleFacade = familleFacade;
        _porteeFacade = porteeFacade;
        _niveauFacade = niveauFacade;
        _animalFacade = animalFacade;
        _clientFacade = clientFacade;
        _clientAnimalFacade = clientAnimalFacade;
        _poidsFacade = poidsFacade;
        _fournisseurFacade = fournisseurFacade;
        _commandeFacade = commandeFacade;
        _listeCommandeFacade = listeCommandeFacade;
        _vaccinFacade = vaccinFacade;
        _veterinaireFacade = veterinaireFacade;
        _listeAnimauxVaccinsFacade = listeAnimauxVaccinsFacade;
        _listeAnimauxVeterinairesFacade = listeAnimauxVeterinairesFacade;
        _userFacade = userFacade;
    }
    
    public IStatutFacade StatutFacade()
    {
        return _statutFacade;
    }

    public ITypeFacade TypeFacade()
    {
        return _typeFacade;
    }

    public IRaceFacade RaceFacade()
    {
        return _raceFacade;
    }

    public IFamilleFacade FamilleFacade()
    {
        return _familleFacade;
    }

    public IPorteeFacade PorteeFacade()
    {
        return _porteeFacade;
    }

    public INiveauFacade NiveauFacade()
    {
        return _niveauFacade;
    }

    public IAnimalFacade AnimalFacade()
    {
        return _animalFacade;
    }

    public IClientFacade ClientFacade()
    {
        return _clientFacade;
    }

    public IClientAnimalFacade ClientAnimalFacade()
    {
        return _clientAnimalFacade;
    }

    public IPoidsFacade PoidsFacade()
    {
        return _poidsFacade;
    }

    public IFournisseurFacade FournisseurFacade()
    {
        return _fournisseurFacade;
    }

    public ICommandeFacade CommandeFacade()
    {
        return _commandeFacade;
    }

    public IListeCommandeFacade ListeCommandeFacade()
    {
        return _listeCommandeFacade;
    }

    public IVaccinFacade VaccinFacade()
    {
        return _vaccinFacade;
    }

    public IVeterinaireFacade VeterinaireFacade()
    {
        return _veterinaireFacade;
    }

    public IListeAnimauxVaccinsFacade ListeAnimauxVaccinsFacade()
    {
        return _listeAnimauxVaccinsFacade;
    }

    public IListeAnimauxVeterinairesFacade ListeAnimauxVeterinairesFacade()
    {
        return _listeAnimauxVeterinairesFacade;
    }

    public IUserFacade UserFacade()
    {
        return _userFacade;
    }

    public static IFacades GetInstance()
    {
        return _instance;
    }
    
    public static void Register(IStatutFacade statutFacade, ITypeFacade typeFacade, IRaceFacade raceFacade, 
        IFamilleFacade familleFacade, IPorteeFacade porteeFacade, INiveauFacade niveauFacade, IAnimalFacade animalFacade,
        IClientFacade clientFacade, IClientAnimalFacade clientAnimalFacade, IPoidsFacade poidsFacade,
        IFournisseurFacade fournisseurFacade, ICommandeFacade commandeFacade, IListeCommandeFacade listeCommandeFacade,
        IVaccinFacade vaccinFacade, IVeterinaireFacade veterinaireFacade, IListeAnimauxVaccinsFacade listeAnimauxVaccinsFacade, IListeAnimauxVeterinairesFacade listeAnimauxVeterinairesFacade,
        IUserFacade userFacade)
    {
        _instance = new FacadeProvider(statutFacade, typeFacade, raceFacade, familleFacade, porteeFacade, niveauFacade, animalFacade,
            clientFacade, clientAnimalFacade, poidsFacade, fournisseurFacade, commandeFacade, listeCommandeFacade,
            vaccinFacade, veterinaireFacade, listeAnimauxVaccinsFacade, listeAnimauxVeterinairesFacade, userFacade);
    }
}