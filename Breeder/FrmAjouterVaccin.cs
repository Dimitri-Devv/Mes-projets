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
    public partial class FrmAjouterVaccin : FrmOnglet
    {
        public FrmAjouterVaccin()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            string libelle = textLibelle.Text;

            FacadeProvider.GetInstance().VaccinFacade().AjouterVaccin(libelle);

            textLibelle.Text = ""; // On clear le champ Libelle

            MessageBox.Show(this, "Vaccin ajouté !");
        }

        private void textLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
