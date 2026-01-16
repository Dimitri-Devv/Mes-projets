using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Stage;

namespace Breeder
{
    public partial class FrmAjouterAnimal : FrmOnglet
    {
        public FrmAjouterAnimal()
        {
            InitializeComponent();
            StyleUtils.AppliquerTheme(this);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {

            //Récupérer les valeurs des champs et les envoyer à la façade Animal
            string nom = textNom.Text;
            string prenom = textPrenom.Text;
            DateTime dateNaissance = DateTime.Parse(textDateNaissance.Text);
            Animal.SexeEnum sexe = sexeBox.SelectedIndex == 0 ? Animal.SexeEnum.MALE : Animal.SexeEnum.FEMELLE;
            decimal poids = textPoids.Value;
            Statut statut = (Statut)textStatut.SelectedItem;
            Animal pere = (Animal)textPere.SelectedItem;
            Animal mere = (Animal)textMere.SelectedItem;
            Race race = (Race)textRace.SelectedItem;
            TypeAnimal typeAnimal = (TypeAnimal)textType.SelectedItem;
            Client leClient = (Client)lesClients.SelectedItem;

            FacadeProvider.GetInstance().AnimalFacade().AjouterAnimal(nom, prenom, dateNaissance, sexe, poids, statut.Id,
                pere.Id, mere.Id, race.Id, typeAnimal.Id);

            if (statut.Libelle == "Vendu" || statut.Libelle == "Réservé")
            {
                //int idAnimal = FacadeProvider.GetInstance().AnimalFacade().GetLastAddedAnimal();
                //FacadeProvider.GetInstance().ClientAnimalFacade().AjouterClientAnimal(leClient.Id, idAnimal);
            }

            MessageBox.Show(this, "Animal ajouté");

            //Vider les champs
            textNom.Text = "";
            textPrenom.Text = "";
            textDateNaissance.Value = DateTime.Now;
            sexeBox.SelectedIndex = 0;
            textPere.SelectedIndex = 0;
            textMere.SelectedIndex = 0;
            textPoids.Value = 0;
            textRace.SelectedIndex = 0;
            textType.SelectedIndex = 0;
            textStatut.SelectedIndex = 0;

            sexeBox.Items.Clear();
            textPere.Items.Clear();
            textMere.Items.Clear();
            textRace.Items.Clear();
            textType.Items.Clear();
            textStatut.Items.Clear();
            FrmAjouterAnimal_Load(sender, e);
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void FrmAjouterAnimal_Load(object sender, EventArgs e)
        {
            textDateNaissance.Value = DateTime.Now;
            sexeBox.Items.AddRange(new object[] { "Mâle", "Femelle" });
            //Remplir les comboBox avec les données à jour
            foreach (Statut statut in FacadeProvider.GetInstance().StatutFacade().GetStatuts())
            {
                textStatut.Items.Add(statut);
            }
            //ComboBox Non modifiable
            textStatut.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (Race race in FacadeProvider.GetInstance().RaceFacade().GetRaces())
            {
                textRace.Items.Add(race);
            }
            textRace.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (TypeAnimal typeAnimal in FacadeProvider.GetInstance().TypeFacade().GetTypes())
            {
                textType.Items.Add(typeAnimal);
            }
            textType.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux())
            {
                switch (animal.Sexe)
                {
                    case Animal.SexeEnum.INCONNU:
                        textMere.Items.Add(animal);
                        textPere.Items.Add(animal);
                        break;
                    case Animal.SexeEnum.MALE:
                        textPere.Items.Add(animal);
                        break;
                    case Animal.SexeEnum.FEMELLE:
                        textMere.Items.Add(animal);
                        break;
                }
            }
            textMere.DropDownStyle = ComboBoxStyle.DropDownList;
            textPere.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
            {
                lesClients.Items.Add(client);
            }
            lesClients.DropDownStyle = ComboBoxStyle.DropDownList;

            sexeBox.SelectedIndex = 0;

            if (textStatut.Items.Count > 0)
            {
                textStatut.SelectedIndex = 0;
            }

            if (textRace.Items.Count > 0)
            {
                textRace.SelectedIndex = 0;
            }

            if (textType.Items.Count > 0)
            {
                textType.SelectedIndex = 0;
            }

            if (textMere.Items.Count > 0)
            {
                textMere.SelectedIndex = 0;
            }

            if (textPere.Items.Count > 0)
            {
                textPere.SelectedIndex = 0;
            }
        }

        private void textStatut_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Afficher la possibilité de choisir un propriétaire seulement si le statut de l'animal est réservé ou vendu
            Statut leStatut = (Statut)textStatut.SelectedItem;
            if (leStatut.Libelle == "Réservé" || leStatut.Libelle == "Vendu")
            {
                lesClients.Enabled = true;
            }
            else
                lesClients.Enabled = false;
        }

        private void textNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
                
        }

        private void textPrenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}