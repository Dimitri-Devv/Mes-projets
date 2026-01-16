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
    public partial class FrmSupprimerCommande : FrmOnglet
    {
        public FrmSupprimerCommande()
        {
            InitializeComponent();
        }

        private void FrmSupprimerCommande_Load(object sender, EventArgs e)
        {
            boxCommandes.DropDownStyle = ComboBoxStyle.DropDownList;
            
            foreach (Commande commande in FacadeProvider.GetInstance().CommandeFacade().GetCommandes())
            {
                boxCommandes.Items.Add(commande);
            }

            if (boxCommandes.Items.Count > 0)
            {
                boxCommandes.SelectedIndex = 0;
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (boxCommandes.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Aucune commande selectionnée !");
                return;
            }
            
            //Crée un dialogResult permettant d'ajouter une sécurité pour éviter les suppressions par erreur
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }
            
            Commande commande = (Commande)boxCommandes.SelectedItem;
            FacadeProvider.GetInstance().CommandeFacade().SupprimerCommande(commande.Id);
            
            boxCommandes.Items.Remove(commande);
            boxCommandes.SelectedItem = null;
            boxCommandes.Text = "";
            
            MessageBox.Show(this, "Commande supprimée");
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
    }
}