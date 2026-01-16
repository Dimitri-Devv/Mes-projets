using Breeder.Filter;
using MySql.Data.MySqlClient;
using Stage;

namespace Breeder
{
    internal static class Program
    {
        public static ApplicationContext AppContext { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IDatabaseFactory databaseFactory = new DatabaseFactoryImpl();
            MySqlConnection connection = databaseFactory.NewConnection();

            IFilterFieldAdapter<FilterBehaviour> filterBehaviourAdapter = new FilterBehaviourAdapterImpl();
            IFilterFieldAdapter<FilterOperation> filterOperationAdapter = new FilterOperationAdapterImpl();

            IStatutDao statutDao = new StatutDaoImpl(connection);
            IStatutFacade statutFacade = new StatutFacadeImpl(statutDao);

            ITypeDao typeDao = new TypeDaoImpl(connection);
            ITypeFacade typeFacade = new TypeFacadeImpl(typeDao);

            IRaceDao raceRaceDao = new RaceDaoImpl(connection);
            IRaceFacade raceRaceFacade = new RaceFacadeImpl(raceRaceDao);

            IRoleDao roleDao = new RoleDaoImpl(connection);
            IRoleFacade roleFacade = new RoleFacadeImpl(roleDao);

            IPorteeDao porteeDao = new PorteeDaoImpl(connection);
            IPorteeFacade porteeFacade = new PorteeFacadeImpl(porteeDao);

            INiveauDao niveauDao = new NiveauDaoImpl(connection);
            INiveauFacade niveauFacade = new NiveauFacadeImpl(niveauDao);

            IPoidsDao poidsDao = new PoidsDaoImpl(connection);
            IPoidsFacade poidsFacade = new PoidsFacadeImpl(poidsDao);

            IFilterReadStrategy<Animal> animalFilterReadStrategy = new AnimalFilterReadStrategy();
            IFilterFieldAdapter<AnimalFilter> animalFilterFieldAdapter = new AnimalFilterFieldAdapter();
            IFilterProvider<Animal, AnimalFilter> animalFilter =
                new FilterProviderImpl<Animal, AnimalFilter>(connection, animalFilterReadStrategy,
                    animalFilterFieldAdapter,
                    filterOperationAdapter, filterBehaviourAdapter);
            IAnimalDao animalDao = new AnimalDaoImpl(connection, animalFilter);
            IAnimalFacade animalFacade = new AnimalFacadeImpl(animalDao, porteeFacade, poidsFacade);

            IFilterReadStrategy<Famille> familleFilterReadStrategy = new FamilleFilterReadStrategy();
            IFilterFieldAdapter<FamilleFilter> familleFilterFieldAdapter = new FamilleFilterFieldAdapter();
            IFilterProvider<Famille, FamilleFilter> familleFilterProvider =
                new FilterProviderImpl<Famille, FamilleFilter>(connection, familleFilterReadStrategy,
                    familleFilterFieldAdapter, filterOperationAdapter, filterBehaviourAdapter);
            IFamilleDao familleDao = new FamilleDaoImpl(connection, familleFilterProvider);
            IFamilleFacade familleFacade = new FamilleFacadeImpl(familleDao, animalFacade, roleFacade);
            
            IClientDao clientDao = new ClientDaoImpl(connection);
            IClientFacade clientFacade = new ClientFacadeImpl(clientDao);

            IClientAnimalDao clientAnimalDao = new ClientAnimalDaoImpl(connection);
            IClientAnimalFacade clientAnimalFacade =
                new ClientAnimalFacadeImpl(clientAnimalDao, animalFacade, clientFacade);

            IFournisseurDao fournisseurDao = new FournisseurDaoImpl(connection);
            IFournisseurFacade fournisseurFacade = new FournisseurFacadeImpl(fournisseurDao);

            ICommandeDao commandeDao = new CommandeDaoImpl(connection);
            ICommandeFacade commandeFacade = new CommandeFacadeImpl(commandeDao);

            IFilterReadStrategy<ListeCommande> listeCommandeReadStrategy = new ListeCommandeFilterReadStrategy();
            IFilterFieldAdapter<ListeCommandeFilter> listeCommandeFilterFieldAdapter =
                new ListeCommandeFilterFieldAdapter();
            IFilterProvider<ListeCommande, ListeCommandeFilter> listeCommandeFilterProvider =
                new FilterProviderImpl<ListeCommande, ListeCommandeFilter>(connection,
                    listeCommandeReadStrategy, listeCommandeFilterFieldAdapter, filterOperationAdapter,
                    filterBehaviourAdapter);
            IListeCommandeDao listeCommandeDao = new ListeCommandeDaoImpl(connection, listeCommandeFilterProvider);
            IListeCommandeFacade listeCommandeFacade =
                new ListeCommandeFacadeImpl(listeCommandeDao, commandeFacade, animalFacade);

            IVaccinDao vaccinDao = new VaccinDaoImpl(connection);
            IVaccinFacade vaccinFacade = new VaccinFacadeImpl(vaccinDao);

            IVeterinaireDao veterinaireDao = new VeterinaireDaoImpl(connection);
            IVeterinaireFacade veterinaireFacade = new VeterinaireFacadeImpl(veterinaireDao);

            IListeAnimauxVaccinsDao listeAnimauxVaccinsDao = new ListeAnimauxVaccinsDaoImpl(connection);
            IListeAnimauxVaccinsFacade listeAnimauxVaccinsFacade =
                new ListeAnimauxVaccinsFacadeImpl(listeAnimauxVaccinsDao, vaccinFacade);

            IListeAnimauxVeterinairesDao listeAnimauxVeterinairesDao = new ListeAnimauxVeterinairesDaoImpl(connection);
            IListeAnimauxVeterinairesFacade listeAnimauxVeterinairesFacade =
                new ListeAnimauxVeterinairesFacadeImpl(listeAnimauxVeterinairesDao, animalFacade, veterinaireFacade);

            IUserDao userDao = new UserDaoImpl(connection);
            IUserFacade userFacade = new UserFacadeImpl(userDao);

            FacadeProvider.Register(statutFacade, typeFacade, raceRaceFacade, familleFacade, porteeFacade,
                niveauFacade, animalFacade, clientFacade
                , clientAnimalFacade, poidsFacade, fournisseurFacade, commandeFacade, listeCommandeFacade,
                vaccinFacade, veterinaireFacade, listeAnimauxVaccinsFacade, listeAnimauxVeterinairesFacade, userFacade);

            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();

            AppContext = new ApplicationContext(new FrmConnexion());
            Application.Run(AppContext);
        }

        public static void SwitchMainForm(Form newForm)
        {
            var oldMainForm = AppContext.MainForm;
            AppContext.MainForm = newForm;
            oldMainForm?.Close();
            newForm.Show();
        }
    }
}