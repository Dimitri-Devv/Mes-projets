
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
    public partial class FrmVaccination : FrmOnglet
    {
        private Animal _animal;
        public FrmVaccination(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void FrmVaccination_Load(object sender, EventArgs e)
        {
            nomPrenom.Text = _animal.Nom + " " + _animal.Prenom;

            //DataGridView Vaccins
            lesVaccins.RowHeadersVisible = false;
            lesVaccins.ColumnCount = 2;

            lesVaccins.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesVaccins.Columns[0].Visible = false;
            lesVaccins.Columns[0].Name = "Vaccin";

            lesVaccins.Columns[1].Name = "Libelle";
            lesVaccins.Columns[1].Width = 100;
            lesVaccins.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            //Affichage des animaux concernés dans le datagridView
            lesVaccinsAttribués.RowHeadersVisible = false;
            lesVaccinsAttribués.ColumnCount = 2;

            lesVaccinsAttribués.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesVaccinsAttribués.Columns[0].Visible = false;
            lesVaccinsAttribués.Columns[0].Name = "Vaccin";

            lesVaccinsAttribués.Columns[1].Name = "Libelle";
            lesVaccinsAttribués.Columns[1].Width = 100;
            lesVaccinsAttribués.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Vaccin vaccin in FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().GetVaccinsManquantsAnimal(_animal.Id))
            {
                lesVaccins.Rows.Add(vaccin, vaccin.Libelle);
            }
            
            foreach (Vaccination vaccination in FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().GetVaccinsByAnimal(_animal.Id))
            {
                   lesVaccinsAttribués.Rows.Add(vaccination.Vaccin, vaccination.Vaccin.Libelle);
            }
        }

        private void btnDeplacer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesVaccins.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                Vaccin vaccin = (Vaccin)row.Cells["Vaccin"].Value;
                lesVaccins.Rows.Remove(row);
                lesVaccinsAttribués.Rows.Add(vaccin, vaccin.Libelle);

                FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().AjouterAnimalVaccin(vaccin.Id, _animal.Id);
            }
        }

        private void btnToutDeplacer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesVaccins.Rows)
            {
                Vaccin vaccin = (Vaccin)row.Cells["Vaccin"].Value;

                lesVaccinsAttribués.Rows.Add(vaccin, vaccin.Libelle);

                FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().AjouterAnimalVaccin(vaccin.Id, _animal.Id);
            }

            lesVaccins.Rows.Clear();
        }

        private void btnToutRetirer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in lesVaccinsAttribués.Rows)
            {
                Vaccin vaccin = (Vaccin)row.Cells["Vaccin"].Value;

                lesVaccins.Rows.Add(vaccin, vaccin.Libelle);

                FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().RetirerAnimalVaccin(vaccin.Id, _animal.Id);
            }

            lesVaccinsAttribués.Rows.Clear();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesVaccinsAttribués.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Vaccin vaccin = (Vaccin)row.Cells["Vaccin"].Value;
                lesVaccinsAttribués.Rows.Remove(row);
                lesVaccins.Rows.Add(vaccin, vaccin.Libelle);

                FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().RetirerAnimalVaccin(vaccin.Id, _animal.Id);
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherDonneesVeterinaire(_animal));
        }
    }
}
