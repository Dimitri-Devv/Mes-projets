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
    public partial class FrmAfficherClient : FrmOnglet
    {
        public FrmAfficherClient()
        {
            InitializeComponent();
        }

        private void FrmAfficherClient_Load(object sender, EventArgs e)
        {
            //Affichage des clients dans le datagridView
            lesClients.RowHeadersVisible = false;
            lesClients.ColumnCount = 6;

            lesClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesClients.Columns[0].Name = "Niveau";
            lesClients.Columns[0].Width = 100;
            lesClients.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[1].Name = "Nom";
            lesClients.Columns[1].Width = 100;
            lesClients.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[2].Name = "Prenom";
            lesClients.Columns[2].Width = 100;
            lesClients.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[3].Name = "Adresse";
            lesClients.Columns[3].Width = 100;
            lesClients.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[4].Name = "Mail";
            lesClients.Columns[4].Width = 100;
            lesClients.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[5].Name = "Telephone";
            lesClients.Columns[5].Width = 100;
            lesClients.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
            {
                Niveau niveau = FacadeProvider.GetInstance().NiveauFacade().GetNiveau(client.IdNiveau);
                lesClients.Rows.Add(niveau.Libelle, client.Nom, client.Prenom, client.Adresse, client.Mail,client.Telephone);
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
    }
}
