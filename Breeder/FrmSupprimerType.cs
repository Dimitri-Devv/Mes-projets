
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
    public partial class FrmSupprimerType : FrmOnglet
    {
        public FrmSupprimerType()
        {
            InitializeComponent();
        }

        private void FrmSupprimerType_Load(object sender, EventArgs e)
        {
            lesTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            
            foreach (TypeAnimal type in FacadeProvider.GetInstance().TypeFacade().GetTypesSansInconnu())
            {
                lesTypes.Items.Add(type);
            }
        }

        private void btnQuitter_Click_1(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
        
        private void Supprimer_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;
            
            TypeAnimal type = (TypeAnimal)lesTypes.SelectedItem;

            FacadeProvider.GetInstance().TypeFacade().SupprimerType(type.Id);

            lesTypes.Items.Remove(type);
            lesTypes.SelectedItem = null;
            lesTypes.Text = "";

            MessageBox.Show(this, "Type supprimé");
        }
    }
}