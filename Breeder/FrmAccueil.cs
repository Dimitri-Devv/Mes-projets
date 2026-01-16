using Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breeder
{
    public partial class FrmAccueil : FrmOnglet
    {
        public FrmAccueil()
        {
            InitializeComponent();
            StyleUtils.AppliquerTheme(this);
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmConnexion());
        }

        private void FrmAccueil_Load(object sender, EventArgs e)
        {
            #region AjoutsRecents

            lesAjoutsRecents.RowHeadersVisible = false;
            // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
            lesAjoutsRecents.ColumnCount = 4;


            //Personnalisation des colonnes
            lesAjoutsRecents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesAjoutsRecents.Columns[0].Visible = false;
            lesAjoutsRecents.Columns[0].Name = "Id";
            
            lesAjoutsRecents.Columns[1].Name = "Nom";
            lesAjoutsRecents.Columns[1].Width = 100;
            lesAjoutsRecents.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAjoutsRecents.Columns[2].Name = "Prenom";
            lesAjoutsRecents.Columns[2].Width = 100;
            lesAjoutsRecents.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAjoutsRecents.Columns[3].Name = "Sexe";
            lesAjoutsRecents.Columns[3].Width = 100;
            lesAjoutsRecents.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Ajoute chaque animaux avec ce qu'on a besoin sauf celui qui a un id = 1
            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxRecents())
            {
                lesAjoutsRecents.Rows.Add(animal.Id, animal.Nom, animal.Prenom, animal.Sexe);
            }

            #endregion
            #region CommandesRecentes
            
            commandesRecentes.RowHeadersVisible = false;
            // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
            commandesRecentes.ColumnCount = 4;


            //Personnalisation des colonnes
            commandesRecentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            commandesRecentes.Columns[0].Visible = false;
            commandesRecentes.Columns[0].Name = "Id";
            
            commandesRecentes.Columns[1].Name = "Libelle";
            commandesRecentes.Columns[1].Width = 100;
            commandesRecentes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            commandesRecentes.Columns[2].Name = "Date";
            commandesRecentes.Columns[2].Width = 100;
            commandesRecentes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            commandesRecentes.Columns[3].Name = "Coût";
            commandesRecentes.Columns[3].Width = 100;
            commandesRecentes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            foreach (Commande commande in FacadeProvider.GetInstance().CommandeFacade().GetCommandesRecentes())
            {
                commandesRecentes.Rows.Add(commande.Id, commande.Libelle, commande.Date.ToString("dd/MM/yyyy"), commande.Total + " EUR");
            }

            #endregion
            #region AnimauxNonAJour
            animauxNonAJour.RowHeadersVisible = false;
            // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
            animauxNonAJour.ColumnCount = 4;


            //Personnalisation des colonnes
            animauxNonAJour.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            animauxNonAJour.Columns[0].Visible = false;
            animauxNonAJour.Columns[0].Name = "Id";
            
            animauxNonAJour.Columns[1].Name = "Nom";
            animauxNonAJour.Columns[1].Width = 100;
            animauxNonAJour.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            animauxNonAJour.Columns[2].Name = "Prenom";
            animauxNonAJour.Columns[2].Width = 100;
            animauxNonAJour.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            animauxNonAJour.Columns[3].Name = "Sexe";
            animauxNonAJour.Columns[3].Width = 100;
            animauxNonAJour.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Ajoute chaque animaux
            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxPoidsPasAJour())
            {
                animauxNonAJour.Rows.Add(animal.Id, animal.Nom, animal.Prenom, animal.Sexe);
            }

            #endregion
           
            #region LesDerniersClients
            //Affichage de la famille dans le datagridView
            derniersClientsAjoutes.RowHeadersVisible = false;
            derniersClientsAjoutes.ColumnCount = 2;

            derniersClientsAjoutes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            derniersClientsAjoutes.Columns[0].Name = "Nom";
            derniersClientsAjoutes.Columns[0].Width = 100;
            derniersClientsAjoutes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            derniersClientsAjoutes.Columns[1].Name = "Prenom";
            derniersClientsAjoutes.Columns[1].Width = 100;
            derniersClientsAjoutes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClientsRecents())
            {
                derniersClientsAjoutes.Rows.Add(client.Nom, client.Prenom);
            }
            #endregion

        }
        
        private void animauxRecents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = lesAjoutsRecents.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["Id"];

            if (cell?.Value == null)
            {
                return;
            }
            Animal? animal = FacadeProvider.GetInstance().AnimalFacade().GetAnimal((int)cell.Value);
            if (animal == null)
            {
                return;
            }
            Program.SwitchMainForm(new FrmProfilAnimal(animal));
        }
        
        
        // lorsqu'on clique sur une ligne du datagridView cela nous amène sur le profil de l'animal sélectionné
        private void animauxNonAJour_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = animauxNonAJour.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["Id"];

            if (cell?.Value == null)
            {
                return;
            }
            Animal? animal = FacadeProvider.GetInstance().AnimalFacade().GetAnimal((int)cell.Value);
            if (animal == null)
            {
                return;
            }
            Program.SwitchMainForm(new FrmCourbePoids(animal));
        }
        
        private void commandes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = commandesRecentes.Rows[e.RowIndex];
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
    }
}

