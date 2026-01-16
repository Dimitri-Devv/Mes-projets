
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
using System.Windows.Documents;
using System.Windows.Forms;

namespace Breeder
{
    public partial class FrmModifierCommande : FrmOnglet
    {
        public FrmModifierCommande()
        {
            InitializeComponent();
        }

        private void FrmModifierCommande_Load(object sender, EventArgs e)
        {
            parametrerDgv();
            chargerLesDonnees();
        }

        private void parametrerDgv()
        {
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


            //Affichage des animaux concernés dans le datagridView
            lesAnimauxConcernés.RowHeadersVisible = false;
            lesAnimauxConcernés.ColumnCount = 4;

            lesAnimauxConcernés.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesAnimauxConcernés.Columns[0].Visible = false;
            lesAnimauxConcernés.Columns[0].Name = "Animal";

            lesAnimauxConcernés.Columns[1].Name = "Nom";
            lesAnimauxConcernés.Columns[1].Width = 100;
            lesAnimauxConcernés.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcernés.Columns[2].Name = "Prenom";
            lesAnimauxConcernés.Columns[2].Width = 100;
            lesAnimauxConcernés.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcernés.Columns[3].Name = "Sexe";
            lesAnimauxConcernés.Columns[3].Width = 100;
            lesAnimauxConcernés.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void chargerLesDonnees()
        {
            foreach (Commande commande in FacadeProvider.GetInstance().CommandeFacade().GetCommandes())
            {
                lesCommandes.Items.Add(commande);
            }

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux().Where(animal => animal.Id != 1))
            {
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            foreach (Fournisseur fournisseur in FacadeProvider.GetInstance().FournisseurFacade().GetFournisseurs())
            {
                lesFournisseurs.Items.Add(fournisseur);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Commande laCommande = (Commande)lesCommandes.SelectedItem;

            Fournisseur? fournisseur = FacadeProvider.GetInstance().FournisseurFacade()
                .GetFournisseur(laCommande.IdFournisseur);

            if (fournisseur == null)
            {
                MessageBox.Show(this, "Fournisseur introuvable");
                return;
            }

            leLibelle.Text = laCommande.Libelle;
            numericUpDown1.Value = laCommande.Total;
            lesFournisseurs.SelectedItem = fournisseur;

            lesAnimauxConcernés.Rows.Clear();
            lesAnimaux.Rows.Clear();

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux().Where(animal => animal.Id != 1))
            {
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            foreach (Animal animal in FacadeProvider.GetInstance().ListeCommandeFacade().GetAnimauxByCommande(laCommande.Id))
            {
                lesAnimauxConcernés.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);

                foreach (DataGridViewRow row in lesAnimaux.Rows)
                {
                    Animal otherAnimal = (Animal)row.Cells["Animal"].Value;
                    if (animal.Id == otherAnimal.Id)
                    {
                        lesAnimaux.Rows.Remove(row);
                    }
                }
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            Commande commande = (Commande)lesCommandes.SelectedItem;
            commande.Libelle = leLibelle.Text;
            commande.Total = numericUpDown1.Value;
            commande.IdFournisseur = ((Fournisseur)lesFournisseurs.SelectedItem).Id;
            FacadeProvider.GetInstance().CommandeFacade().ModifierCommande(commande);

            DataGridViewRowCollection rows = lesAnimauxConcernés.Rows;

            List<int> idAnimaux = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                idAnimaux.Add(animal.Id);
            }

            FacadeProvider.GetInstance().ListeCommandeFacade().ModifierListeCommandes(commande.Id, idAnimaux);
            MessageBox.Show(this, "Commande modifiée !");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnDeplacer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesAnimaux.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimaux.Rows.Remove(row);

                lesAnimauxConcernés.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }

        private void btnToutDeplacer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesAnimaux.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;

                lesAnimauxConcernés.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            lesAnimaux.Rows.Clear();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesAnimauxConcernés.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimauxConcernés.Rows.Remove(row);
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }

        private void btnToutRetirer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesAnimauxConcernés.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;

                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            lesAnimauxConcernés.Rows.Clear();
        }

        private void leLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}