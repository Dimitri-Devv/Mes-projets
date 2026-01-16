
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
    public partial class FrmAfficherFournisseurs : FrmOnglet
    {
        public FrmAfficherFournisseurs()
        {
            InitializeComponent();
        }

        private void FrmAfficherFournisseurs_Load(object sender, EventArgs e)
        {
            //Affichage des Fournisseurs dans le datagridView
            lesFournisseurs.RowHeadersVisible = false;
            lesFournisseurs.ColumnCount = 4;

            lesFournisseurs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesFournisseurs.Columns[0].Name = "Libelle";
            lesFournisseurs.Columns[0].Width = 100;
            lesFournisseurs.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesFournisseurs.Columns[1].Name = "Adresse";
            lesFournisseurs.Columns[1].Width = 100;
            lesFournisseurs.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesFournisseurs.Columns[2].Name = "Mail";
            lesFournisseurs.Columns[2].Width = 100;
            lesFournisseurs.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesFournisseurs.Columns[3].Name = "Numero";
            lesFournisseurs.Columns[3].Width = 100;
            lesFournisseurs.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            foreach (Fournisseur fournisseur in FacadeProvider.GetInstance().FournisseurFacade().GetFournisseurs())
            {
                lesFournisseurs.Rows.Add(fournisseur.Libelle, fournisseur.Adresse, fournisseur.Mail, fournisseur.Telephone);
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
    }
}
