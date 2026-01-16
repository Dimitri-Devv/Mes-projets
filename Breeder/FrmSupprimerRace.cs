
using MySqlX.XDevAPI;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Breeder
{
    public partial class FrmSupprimerRace : FrmOnglet
    {
        public FrmSupprimerRace()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
        private void FrmSupprimerRace_Load(object sender, EventArgs e)
        {
            //Charge les Races
            foreach (Race race in FacadeProvider.GetInstance().RaceFacade().GetRacesSansInconnu())
            {
                lesRaces.Items.Add(race);
            }
            lesRaces.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void Supprimer_Click(object sender, EventArgs e)
        {
            //Crée un dialogResult permettant d'ajouter une sécurité pour éviter les suppressions par erreur
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Race race = (Race)lesRaces.SelectedItem;
                FacadeProvider.GetInstance().RaceFacade().SupprimerRace(race.Id);

                lesRaces.Items.Remove(race);
                lesRaces.SelectedItem = null;
                lesRaces.Text = "";

                MessageBox.Show(this, "Race supprimée");
            }
        }

        
    }
}
