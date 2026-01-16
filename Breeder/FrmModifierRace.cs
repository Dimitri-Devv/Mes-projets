
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
    public partial class FrmModifierRace : FrmOnglet
    {
        public FrmModifierRace()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void FrmModifierRace_Load(object sender, EventArgs e)
        {
            lesAnimaux.DropDownStyle = ComboBoxStyle.DropDownList;
            lesRaces.DropDownStyle = ComboBoxStyle.DropDownList;
            
            lesAnimaux.Items.Clear();
            lesRaces.Items.Clear();
            
            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxSansInconnu())
            {
                lesAnimaux.Items.Add(animal);
            }
            
            foreach (Race race in FacadeProvider.GetInstance().RaceFacade().GetRaces())
            {
                lesRaces.Items.Add(race);
            }
        }

        private void lesAnimaux_SelectedIndexChanged(object sender, EventArgs e)
        {
            Animal animal = (Animal)lesAnimaux.SelectedItem;
            lesRaces.SelectedItem = FacadeProvider.GetInstance().RaceFacade().GetRace(animal.IdRace);
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Animal animal = (Animal)lesAnimaux.SelectedItem;
            Race race = (Race)lesRaces.SelectedItem;
            FacadeProvider.GetInstance().AnimalFacade().ModifierRace(animal.Id, race.Id);
            
            FrmModifierRace_Load(sender, e);
            MessageBox.Show("Race Modifiée");
        }
    }
}