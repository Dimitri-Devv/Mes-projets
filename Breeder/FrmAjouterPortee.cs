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
    public partial class FrmAjouterPortee : FrmOnglet
    {
        public FrmAjouterPortee()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Récupérer l'animal femelle sélectionné et l'envoyer à la façade portée
            Animal mere = (Animal)boxMere.SelectedItem;
            FacadeProvider.GetInstance().PorteeFacade().AjouterPortee(mere.Id, libelleBox.Text, datePortee.Value);
            
            MessageBox.Show("Portée Ajoutée");
            
            libelleBox.Text = "";
            datePortee.Value= DateTime.Now;
        }

        private void FrmAjouterPortee_Load(object sender, EventArgs e)
        {
            //Charge les données en affichant les femelles pour les mères
            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux().Where(animal => animal.Id != 1 && animal.Sexe == Animal.SexeEnum.FEMELLE))
            {
                boxMere.Items.Add(animal);
            }
            boxMere.DropDownStyle = ComboBoxStyle.DropDownList;

            if (boxMere.Items.Count > 0)
            {
                boxMere.SelectedIndex = 0;
            }
        }

        private void libelleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
