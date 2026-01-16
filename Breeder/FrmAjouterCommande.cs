
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;
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
    public partial class FrmAjouterCommande : FrmOnglet
    {
        public FrmAjouterCommande()
        {
            InitializeComponent();
        }

        private void FrmAjouterCommande_Load(object sender, EventArgs e)
        {
            //Charge les Fournisseurs
            foreach (Fournisseur fournisseur in FacadeProvider.GetInstance().FournisseurFacade().GetFournisseursSansInconnu())
            {
                lesFournisseurs.Items.Add(fournisseur);
            }

            lesFournisseurs.DropDownStyle = ComboBoxStyle.DropDownList;

            //DataGridView Animaux
            lesAnimaux.RowHeadersVisible = false;
            lesAnimaux.ColumnCount = 4;

            lesAnimaux.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesAnimaux.Columns[0].Visible = false;
            lesAnimaux.Columns[0].Name = "Animal";

            lesAnimaux.Columns[1].Name = "Nom";
            lesAnimaux.Columns[1].Width = 100;
            lesAnimaux.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimaux.Columns[2].Name = "Prenom";
            lesAnimaux.Columns[2].Width = 100;
            lesAnimaux.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimaux.Columns[3].Name = "Sexe";
            lesAnimaux.Columns[3].Width = 100;
            lesAnimaux.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux().Where(animal => animal.Id != 1))
            {
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }


            //Affichage des animaux concernés dans le datagridView
            lesAnimauxConcerenés.RowHeadersVisible = false;
            lesAnimauxConcerenés.ColumnCount = 4;

            lesAnimauxConcerenés.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesAnimauxConcerenés.Columns[0].Visible = false;
            lesAnimauxConcerenés.Columns[0].Name = "Animal";

            lesAnimauxConcerenés.Columns[1].Name = "Nom";
            lesAnimauxConcerenés.Columns[1].Width = 100;
            lesAnimauxConcerenés.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcerenés.Columns[2].Name = "Prenom";
            lesAnimauxConcerenés.Columns[2].Width = 100;
            lesAnimauxConcerenés.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcerenés.Columns[3].Name = "Sexe";
            lesAnimauxConcerenés.Columns[3].Width = 100;
            lesAnimauxConcerenés.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            string libelle = leLibelle.Text;
            decimal total = numericUpDown1.Value;
            Fournisseur fournisseur = (Fournisseur)lesFournisseurs.SelectedItem;

            FacadeProvider.GetInstance().CommandeFacade().AjouterCommande(libelle, DateTime.Today, total, fournisseur.Id);
            int idCommande = FacadeProvider.GetInstance().CommandeFacade().GetLastAddedIdCommande();

            List<int> idAnimaux = new List<int>();
            foreach (DataGridViewRow row in lesAnimauxConcerenés.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                idAnimaux.Add(animal.Id);
            }

            FacadeProvider.GetInstance().ListeCommandeFacade().Commander(idCommande, idAnimaux);

            MessageBox.Show("Commande ajoutée");

            leLibelle.Text = "";
            lesFournisseurs.SelectedItem = null;
            numericUpDown1.Value = 0;
            lesAnimaux.Rows.Clear();
            lesAnimauxConcerenés.Rows.Clear();
            FrmAjouterCommande_Load(sender, e);
           
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnDeplacer_Click(object sender, EventArgs e)
        {
            //Lors du clique, cela enlève la ligne du datagridView1 et l'ajoute dans l'autre dataGridView
            DataGridViewSelectedRowCollection selectedRows = lesAnimaux.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimaux.Rows.Remove(row);
                
                lesAnimauxConcerenés.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }

        private void btnToutDeplacer_Click(object sender, EventArgs e)
        {
            //Lors du clique , cela enlève les lignes du dataGridView2 et les ajoute dans l'autre dataGridView
            foreach (DataGridViewRow row in lesAnimaux.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimauxConcerenés.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
            
            lesAnimaux.Rows.Clear();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesAnimauxConcerenés.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimauxConcerenés.Rows.Remove(row);
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }

        private void btnToutRetirer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesAnimauxConcerenés.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
            
            lesAnimauxConcerenés.Rows.Clear();
        }

        private void leLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}