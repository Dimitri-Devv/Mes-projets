
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
    public partial class FrmAfficherDonneesVeterinaire : FrmOnglet
    {
        private Animal _animal;
        public FrmAfficherDonneesVeterinaire(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void FrmAfficherDonneesVeterinaires_Load(object sender, EventArgs e)
        {
            label4.Text = _animal.Nom;
            label5.Text = _animal.Prenom;

            //Affichage des vaccins de l'animal
            sesVaccins.RowHeadersVisible = false;
            sesVaccins.ColumnCount = 2;

            sesVaccins.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            sesVaccins.Columns[0].Name = "Libelle";
            sesVaccins.Columns[0].Width = 100;
            sesVaccins.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            sesVaccins.Columns[1].Name = "Date de vaccination";
            sesVaccins.Columns[1].Width = 100;
            sesVaccins.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Vaccination vaccination in FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade().GetVaccinsByAnimal(_animal.Id))
            {
                    sesVaccins.Rows.Add(vaccination.Vaccin.Libelle, vaccination.Date.ToString("dd/MM/yyyy"));
                
            }

            //Affichage les vétérinaires de l'animal
            lesVetos.RowHeadersVisible = false;
            lesVetos.ColumnCount = 2;

            lesVetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            lesVetos.Columns[0].Name = "Veterinaire";
            lesVetos.Columns[0].Visible = false;

            lesVetos.Columns[1].Name = "Nom";
            lesVetos.Columns[1].Width = 100;
            lesVetos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (Veterinaire? veterinaire in FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().GetVeterinairesByAnimal(_animal.Id))
            {
                if(veterinaire != null)
                {
                    lesVetos.Rows.Add(veterinaire, veterinaire.Nom);
                }

            }

        }

        private void btnAfficherVeto_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesVetos.SelectedRows;
             
            foreach (DataGridViewRow row in selectedRows)
            {
                Veterinaire veterinaire = (Veterinaire)row.Cells["Veterinaire"].Value;
                Console.WriteLine(veterinaire.Nom);
                Program.SwitchMainForm(new FrmAfficherVeterinaire(veterinaire));
            }
            

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesVetos.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Veterinaire veterinaire = (Veterinaire)row.Cells["Veterinaire"].Value;

                FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().ModifierObservations(veterinaire.Id, _animal.Id, textObservation.Text);

                MessageBox.Show("Modification enregistré");
            }


        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmProfilAnimal(_animal));
        }

        private void btnAjoutDuVaccin_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmVaccination(_animal));
        }

        private void lesVetos_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = lesVetos.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Veterinaire veterinaire = (Veterinaire)row.Cells["Veterinaire"].Value;
                string observations = FacadeProvider.GetInstance().ListeAnimauxVeterinairesFacade().GetObservations(veterinaire.Id, _animal.Id);
                textObservation.Text = observations;

                    
            }
        }
    }
}