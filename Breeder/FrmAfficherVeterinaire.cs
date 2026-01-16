
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
    public partial class FrmAfficherVeterinaire : FrmOnglet
    {
        private Veterinaire _veterinaire;
        public FrmAfficherVeterinaire(Veterinaire veterinaire)
        {
            InitializeComponent();
            _veterinaire = veterinaire;
        }

        private void FrmAfficherVeterinaire_Load(object sender, EventArgs e)
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


            //Affichage des animaux à charges dans le datagridView
            lesAnimauxACharges.RowHeadersVisible = false;
            lesAnimauxACharges.ColumnCount = 4;

            lesAnimauxACharges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesAnimauxACharges.Columns[0].Visible = false;
            lesAnimauxACharges.Columns[0].Name = "Animal";

            lesAnimauxACharges.Columns[1].Name = "Nom";
            lesAnimauxACharges.Columns[1].Width = 100;
            lesAnimauxACharges.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxACharges.Columns[2].Name = "Prenom";
            lesAnimauxACharges.Columns[2].Width = 100;
            lesAnimauxACharges.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxACharges.Columns[3].Name = "Sexe";
            lesAnimauxACharges.Columns[3].Width = 100;

            lesAnimauxACharges.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            leNom.Text = _veterinaire.Nom;
            leMail.Text = _veterinaire.Mail;
            leTel.Text = _veterinaire.Telephone;
            lAdresse.Text = _veterinaire.Adresse;

            //Remplir les datagridView avec les données correspondantes
            foreach (Animal animal in FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().GetAnimauxPasCeVeterinaire(_veterinaire.Id))
            {
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            foreach(Animal animal in FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().GetAnimauxDeVeterinaire(_veterinaire.Id))
                {
                lesAnimauxACharges.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherLesVeterinaires());
        }

        private void btnDeplacer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesAnimaux.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimaux.Rows.Remove(row);

                lesAnimauxACharges.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);

                FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().AjouterAnimalVeterinaire(_veterinaire.Id, animal.Id, "");
            }
        }

        private void btnToutDeplacer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesAnimaux.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;

                lesAnimauxACharges.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);

                FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().AjouterAnimalVeterinaire(_veterinaire.Id, animal.Id, "");
            }

            lesAnimaux.Rows.Clear();
        }

        private void btnToutRetirer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesAnimauxACharges.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;

                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);

                FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().SupprimerAnimalVeterinaire(_veterinaire.Id, animal.Id);
            }

            lesAnimauxACharges.Rows.Clear();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesAnimauxACharges.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                lesAnimauxACharges.Rows.Remove(row);
                lesAnimaux.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);

                FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().SupprimerAnimalVeterinaire(_veterinaire.Id, animal.Id);
            }
        }
    }
}
