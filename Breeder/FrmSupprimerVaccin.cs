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
    public partial class FrmSupprimerVaccin : FrmOnglet
    {
        public FrmSupprimerVaccin()
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
                Vaccin vaccin = (Vaccin)lesVaccins.SelectedItem;

                FacadeProvider.GetInstance().VaccinFacade().SupprimerVaccin(vaccin.Id);

                lesVaccins.Items.Remove(vaccin);
                lesVaccins.SelectedItem = null;
                lesVaccins.Text = "";

                MessageBox.Show(this, "Vaccin supprimé");
            }
        }

        private void FrmSupprimerVaccin_Load(object sender, EventArgs e)
        {
            //Charge les Vaccins
            foreach (Vaccin vaccin in FacadeProvider.GetInstance().VaccinFacade().GetVaccins().Where(vaccin => vaccin.Id != 1))
            {
                lesVaccins.Items.Add(vaccin);
            }
            lesVaccins.DropDownStyle = ComboBoxStyle.DropDownList;
        }

    }
}
