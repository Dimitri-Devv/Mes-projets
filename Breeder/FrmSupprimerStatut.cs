
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
    public partial class FrmSupprimerStatut : FrmOnglet
    {
        public FrmSupprimerStatut()
        {
            InitializeComponent();
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                //FacadeProvider.GetInstance().StatutFacade().SupprimerStatut();
            }
        }

        private void btnQuitter_Click_1(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void FrmSupprimerStatut_Load(object sender, EventArgs e)
        {
            foreach (Statut statut in FacadeProvider.GetInstance().StatutFacade().GetStatuts())
            {
                lesStatuts.Items.Add(statut);
            }
            lesStatuts.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Supprimer_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Statut statut = (Statut)lesStatuts.SelectedItem;

                FacadeProvider.GetInstance().StatutFacade().SupprimerStatut(statut.Id);

                lesStatuts.Items.Remove(statut);
                lesStatuts.SelectedItem = null;
                lesStatuts.Text = "";

                MessageBox.Show(this, "Statut supprimé");      
            }
        }
    }
}
