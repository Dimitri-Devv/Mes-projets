
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
    public partial class FrmModifierStatut : FrmOnglet
    {
        public FrmModifierStatut()
        {
            InitializeComponent();
        }

        private void FrmModifierStatut_Load(object sender, EventArgs e)
        {
            lesAnimaux.DropDownStyle = ComboBoxStyle.DropDownList;
            lesStatuts.DropDownStyle = ComboBoxStyle.DropDownList;
            
            lesAnimaux.Items.Clear();
            lesStatuts.Items.Clear();

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxSansInconnu())
            {
                lesAnimaux.Items.Add(animal);
            }

            foreach (Statut statut in FacadeProvider.GetInstance().StatutFacade().GetStatuts())
            {
                lesStatuts.Items.Add(statut);
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            Animal animal = (Animal)lesAnimaux.SelectedItem;
            Statut statut = (Statut)lesStatuts.SelectedItem;
            
            FacadeProvider.GetInstance().AnimalFacade().ModifierStatut(animal.Id, statut.Id);
            
            FrmModifierStatut_Load(sender, e);
            MessageBox.Show("Statut modifié !");
        }

        private void lesAnimaux_SelectedIndexChanged(object sender, EventArgs e)
        {
            Animal animal = (Animal)lesAnimaux.SelectedItem;
            Statut statut = FacadeProvider.GetInstance().StatutFacade().GetStatut(animal.IdStatut) ?? FacadeProvider.GetInstance().StatutFacade().GetInconnu();

            lesStatuts.SelectedItem = statut;
        }
    }
}