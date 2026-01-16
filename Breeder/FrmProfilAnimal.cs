
using Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Breeder
{
    public partial class FrmProfilAnimal : FrmOnglet
    {
        private Animal _animal;

        public FrmProfilAnimal(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void FrmProfilAnimal_Load(object sender, EventArgs e)

        {
            parametrerDgvFamille();

            chargerDonneesGlobales();
            chargerDonneesAnimal(_animal);
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            _animal.Nom = leNom.Text;
            _animal.Prenom = Prenom.Text;
            _animal.Sexe = lesSexes.SelectedIndex == 0 ? Animal.SexeEnum.INCONNU : lesSexes.SelectedIndex == 1 ? Animal.SexeEnum.MALE : Animal.SexeEnum.FEMELLE;
            _animal.DateNaissance = dateNaissance.Value;
            _animal.Poids = PoidsActuel.Value;
            _animal.IdPere = ((Animal)lesPeres.SelectedItem).Id;
            _animal.IdMere = ((Animal)lesMeres.SelectedItem).Id;
            _animal.IdStatut = ((Statut)lesStatuts.SelectedItem).Id;
            _animal.IdRace = ((Race)lesRaces.SelectedItem).Id;
            _animal.IdType = ((TypeAnimal)lesTypes.SelectedItem).Id;

            FacadeProvider.GetInstance().AnimalFacade().ModifierAnimal(_animal);

            if (boxProprio.SelectedItem != null)
            {
                Client? ancienClient = FacadeProvider.GetInstance().ClientAnimalFacade().GetClientByAnimal(_animal.Id);
                Client nouveauClient = (Client)boxProprio.SelectedItem;

                if (ancienClient != null)
                {
                    ClientAnimal? clientAnimal = FacadeProvider.GetInstance().ClientAnimalFacade()
                        .GetClientAnimal(ancienClient.Id, _animal.Id);
                    if (clientAnimal != null)
                    {
                        FacadeProvider.GetInstance().ClientAnimalFacade().SupprimerClientAnimal(clientAnimal.Id);
                    }
                }

                FacadeProvider.GetInstance().ClientAnimalFacade().AjouterClientAnimal(nouveauClient.Id, _animal.Id);
            }

            MessageBox.Show("Animal modifié");
        }


        private void poids_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmCourbePoids(_animal));
        }

        private void lesStatuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Statut leStatut = (Statut)lesStatuts.SelectedItem;

            boxProprio.Enabled = leStatut.Libelle == "Réservé" || leStatut.Libelle == "Vendu";

            Client? client = FacadeProvider.GetInstance().ClientAnimalFacade().GetClientByAnimal(_animal.Id);
            if (client != null)
            {
                if (boxProprio.Enabled)
                {
                        boxProprio.SelectedItem = client;

                    return;
                }
                else
                {
                    ClientAnimal? clientAnimal = FacadeProvider.GetInstance().ClientAnimalFacade().GetClientAnimal(client.Id, _animal.Id);

                    if (clientAnimal != null)
                    {
                        FacadeProvider.GetInstance().ClientAnimalFacade().SupprimerClientAnimal(clientAnimal.Id);
                        boxProprio.SelectedItem = null;
                    }
                    return;


                }
            }




        }

        private void parametrerDgvFamille()
        {
            //Affichage de la famille dans le datagridView
            laFamille.RowHeadersVisible = false;
            laFamille.ColumnCount = 5;

            laFamille.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            /**
             * On stocke l'id de l'animal pour que lorsque l'on
             * clique sur une cell on puisse retrouver l'animal
             */
            laFamille.Columns[0].Visible = false;
            laFamille.Columns[0].Name = "Id";

            laFamille.Columns[1].Name = "Libelle";
            laFamille.Columns[1].Width = 50;
            laFamille.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            laFamille.Columns[2].Name = "Nom";
            laFamille.Columns[2].Width = 50;
            laFamille.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            laFamille.Columns[3].Name = "Prenom";
            laFamille.Columns[3].Width = 50;
            laFamille.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            laFamille.Columns[4].Name = "Role";
            laFamille.Columns[4].Width = 50;
            laFamille.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //Affichage du DataGridView pour les dépenses

            lesDepenses.RowHeadersVisible = false;
            lesDepenses.ColumnCount = 5;

            lesDepenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            /**
             * On stocke l'id de l'animal pour que lorsque l'on
             * clique sur une cell on puisse retrouver l'animal
             */
            lesDepenses.Columns[0].Visible = false;
            lesDepenses.Columns[0].Name = "Id";

            lesDepenses.Columns[1].Name = "Libelle";
            lesDepenses.Columns[1].Width = 100;
            lesDepenses.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesDepenses.Columns[2].Name = "Date";
            lesDepenses.Columns[2].Width = 100;
            lesDepenses.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesDepenses.Columns[3].Name = "Côut";
            lesDepenses.Columns[3].Width = 100;
            lesDepenses.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesDepenses.Columns[4].Name = "Fournisseur";
            lesDepenses.Columns[4].Width = 100;
            lesDepenses.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Commande commande in FacadeProvider.GetInstance().ListeCommandeFacade().GetCommandesByAnimal(_animal.Id))
            {
                Fournisseur? fournisseur = FacadeProvider.GetInstance().FournisseurFacade().GetFournisseur(commande.IdFournisseur);
                if (fournisseur != null)
                {
                    lesDepenses.Rows.Add(commande.Id, commande.Libelle, commande.Date.ToString("dd/MM/yyyy"), commande.Total + " EUR", fournisseur.Libelle);
                }
            }
        }

        private void chargerDonneesGlobales()
        {
            chargerClients();
            chargerStatuts();
            chargerSexes();
            chargerRaces();
            chargerTypes();

            lesPeres.DropDownStyle = ComboBoxStyle.DropDownList;
            lesMeres.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerClients()
        {
            //Chargement des clients
            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
                boxProprio.Items.Add(client);
            boxProprio.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerStatuts()
        {
            //Chargement des Statuts
            foreach (Statut statuts in FacadeProvider.GetInstance().StatutFacade().GetStatuts())
                lesStatuts.Items.Add(statuts);

            // Aucun statut trouvé
            if (lesStatuts.Items.Count == 0)
            {
                // todo générer les statuts
                foreach (Statut statuts in FacadeProvider.GetInstance().StatutFacade().GetStatuts())
                    lesStatuts.Items.Add(statuts);
            }

            lesStatuts.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerSexes()
        {
            //Chargement des Sexes
            foreach (Animal.SexeEnum sexeEnum in Enum.GetValues(typeof(Animal.SexeEnum)).Cast<Animal.SexeEnum>())
                lesSexes.Items.Add(sexeEnum);

            // Aucun sexe trouvé
            if (lesSexes.Items.Count == 0)
            {
                // todo générer les sexes
                foreach (Animal.SexeEnum sexeEnum in Enum.GetValues(typeof(Animal.SexeEnum)).Cast<Animal.SexeEnum>())
                    lesSexes.Items.Add(sexeEnum);
            }

            // L'animal a un sexe qui n'existe pas
            if (!lesSexes.Items.Contains(_animal.Sexe))
            {
                _animal.Sexe = (Animal.SexeEnum)lesSexes.Items[0];
            }

            lesSexes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerRaces()
        {
            foreach (Race races in FacadeProvider.GetInstance().RaceFacade().GetRaces())
                lesRaces.Items.Add(races);

            // Aucune race trouvée
            if (lesRaces.Items.Count == 0)
            {
                FacadeProvider.GetInstance().RaceFacade().AjouterInconnu();
                lesRaces.Items.Add(FacadeProvider.GetInstance().RaceFacade().GetInconnu());
            }

            lesRaces.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerTypes()
        {
            foreach (TypeAnimal types in FacadeProvider.GetInstance().TypeFacade().GetTypes())
                lesTypes.Items.Add(types);

            // Aucun type trouvé
            if (lesTypes.Items.Count == 0)
            {
                FacadeProvider.GetInstance().TypeFacade().AjouterInconnu();
                lesTypes.Items.Add(FacadeProvider.GetInstance().TypeFacade().GetInconnu());
            }

            lesTypes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void chargerDonneesAnimal(Animal animal)
        {
            _animal = animal;
            leNom.Text = animal.Nom;
            Prenom.Text = animal.Prenom;
            dateNaissance.Value = animal.DateNaissance;
            PoidsActuel.Value = animal.Poids;

            Client? client = FacadeProvider.GetInstance().ClientAnimalFacade().GetClientByAnimal(animal.Id);
            boxProprio.Enabled = client != null;
            boxProprio.Text = client == null ? "Aucun" : client.ToString();
            if (client != null)
            {
                boxProprio.SelectedItem = client;
            }

            lesSexes.SelectedItem = _animal.Sexe;

            Statut statut = FacadeProvider.GetInstance().StatutFacade().GetStatut(animal.IdStatut) ?? FacadeProvider.GetInstance().StatutFacade().GetInconnu();
            lesStatuts.SelectedItem = statut;

            Race race = FacadeProvider.GetInstance().RaceFacade().GetRace(animal.IdRace) ?? FacadeProvider.GetInstance().RaceFacade().GetInconnu();
            lesRaces.SelectedItem = race;

            TypeAnimal typeAnimal = FacadeProvider.GetInstance().TypeFacade().GetType(animal.IdType) ?? FacadeProvider.GetInstance().TypeFacade().GetInconnu();
            lesTypes.SelectedItem = typeAnimal;

            lesPeres.Items.Clear();
            lesMeres.Items.Clear();

            Animal animalInconnu = FacadeProvider.GetInstance().AnimalFacade().GetAnimal(1) ?? FacadeProvider.GetInstance().AnimalFacade().GetInconnu();
            lesPeres.Items.Add(animalInconnu);
            lesMeres.Items.Add(animalInconnu);

            foreach (Animal male in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxBySexe(Animal.SexeEnum.MALE).Where(x => x.Id != animal.Id))
                lesPeres.Items.Add(male);

            foreach (Animal male in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxBySexe(Animal.SexeEnum.FEMELLE).Where(x => x.Id != animal.Id))
                lesMeres.Items.Add(male);

            Animal pere = FacadeProvider.GetInstance().AnimalFacade().GetAnimal(animal.IdPere) ?? FacadeProvider.GetInstance().AnimalFacade().GetInconnu();
            Animal mere = FacadeProvider.GetInstance().AnimalFacade().GetAnimal(animal.IdMere) ?? FacadeProvider.GetInstance().AnimalFacade().GetInconnu();

            lesPeres.SelectedItem = pere;
            lesMeres.SelectedItem = mere;

            laFamille.Rows.Clear();
            laFamille.ClearSelection();

            string libelleFamille = pere.Nom + " " + mere.Nom;
            if (pere.Id != 1)
                laFamille.Rows.Add(pere.Id, libelleFamille, pere.Nom, pere.Prenom, "Père");

            if (mere.Id != 1)
                laFamille.Rows.Add(mere.Id, libelleFamille, mere.Nom, mere.Prenom, "Mère");

            foreach (Animal enfant in FacadeProvider.GetInstance().FamilleFacade().GetEnfants(pere.Id, mere.Id))
                laFamille.Rows.Add(enfant.Id, libelleFamille, enfant.Nom, enfant.Prenom, "Enfant");
        }

        private void dgvFamille_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = laFamille.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells[0];

            if (cell?.Value != null)
            {
                Animal? animal = FacadeProvider.GetInstance().AnimalFacade().GetAnimal((int)cell.Value);
                if (animal != null && animal.Id != _animal.Id)
                {
                    chargerDonneesAnimal(animal);
                }
            }
        }

        private void lesDepenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = lesDepenses.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["Id"];

            if (cell?.Value == null)
            {
                return;
            }
            Commande? commande = FacadeProvider.GetInstance().CommandeFacade().GetCommande((int)cell.Value);
            if (commande == null)
            {
                return;
            }
            Program.SwitchMainForm(new FrmAfficherCommande(commande));
        }

        private void DonneeVeto_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherDonneesVeterinaire(_animal));
        }

        private void leNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void Prenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
