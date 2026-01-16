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
    public partial class FrmAjouterType : FrmOnglet
    {
        public FrmAjouterType()
        {
            InitializeComponent();
        }

        private void FrmAjouterType_Load(object sender, EventArgs e)
        {
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            FacadeProvider.GetInstance().TypeFacade().AjouterType(textLibelle.Text);

            textLibelle.Text = "";
            MessageBox.Show("Type ajouté !");
        }

        private void textLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}