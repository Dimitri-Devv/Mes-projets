
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
    public partial class FrmSupprimerFournisseur : FrmOnglet
    {
        public FrmSupprimerFournisseur()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            //Crée un dialogResult permettant d'ajouter une sécurité pour éviter les suppressions par erreur
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Fournisseur fournisseur = (Fournisseur)lesFournisseurs.SelectedItem;

                FacadeProvider.GetInstance().FournisseurFacade().SupprimerFournisseur(fournisseur.Id);

                lesFournisseurs.Items.Remove(fournisseur);
                lesFournisseurs.SelectedItem = null;
                lesFournisseurs.Text = "";

                MessageBox.Show(this, "Fournisseur supprimé");
            }
        }

        private void FrmSupprimerFournisseur_Load(object sender, EventArgs e)
        {
            //Charge les Fournisseurs
            foreach (Fournisseur fournisseur in FacadeProvider.GetInstance().FournisseurFacade().GetFournisseurs().Where(fournisseur => fournisseur.Id != 1))
            {
                lesFournisseurs.Items.Add(fournisseur);
            }
            lesFournisseurs.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
