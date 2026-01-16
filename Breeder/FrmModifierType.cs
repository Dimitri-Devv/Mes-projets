
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
    public partial class FrmModifierType : FrmOnglet
    {
        public FrmModifierType()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
        private void FrmModifierType_Load(object sender, EventArgs e)
        {
            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxSansInconnu())
            {
                lesAnimaux.Items.Add(animal);
            }
            lesAnimaux.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (TypeAnimal type in FacadeProvider.GetInstance().TypeFacade().GetTypes())
            {
                lesTypes.Items.Add(type);
            }
            lesTypes.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void lesAnimaux_SelectedIndexChanged(object sender, EventArgs e)
        {
            Animal animal = (Animal) lesAnimaux.SelectedItem;
            lesTypes.SelectedItem = FacadeProvider.GetInstance().TypeFacade().GetType(animal.IdType);
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Animal animal = (Animal)lesAnimaux.SelectedItem;
            TypeAnimal typeAnimal = (TypeAnimal)lesTypes.SelectedItem;
            FacadeProvider.GetInstance().AnimalFacade().ModifierType(animal.Id, typeAnimal.Id);
            MessageBox.Show("Type modifié");
        }
    }
}
