
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
    public partial class FrmSupprimerClient : FrmOnglet
    {
        public FrmSupprimerClient()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void FrmSupprimerClient_Load(object sender, EventArgs e)
        {
            //Charge les Clients
            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
            {
                lesClients.Items.Add(client);
            }
            lesClients.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            //Crée un dialogResult permettant d'ajouter une sécurité pour éviter les suppressions par erreur
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Client client = (Client)lesClients.SelectedItem;

                FacadeProvider.GetInstance().ClientFacade().SupprimerClient(client.Id);

                lesClients.Items.Remove(client);
                lesClients.SelectedItem = null;
                lesClients.Text = "";

                MessageBox.Show(this, "Client supprimé");
            }
        }
    }
}
