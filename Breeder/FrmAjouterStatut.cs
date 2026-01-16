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
    public partial class FrmAjouterStatut : FrmOnglet
    {
        public FrmAjouterStatut()
        {
            InitializeComponent();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            string libelle = textLibelle.Text;
            // Contrôles

            FacadeProvider.GetInstance().StatutFacade().AjouterStatut(libelle);

            textLibelle.Text = ""; // On clear le champ Libelle

            MessageBox.Show(this, "Statut ajouté !");
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void textLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
