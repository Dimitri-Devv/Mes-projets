
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
    public partial class FrmSupprimerAnimal : FrmOnglet
    {
        public FrmSupprimerAnimal()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            DataGridViewSelectedRowCollection selectedRows = lesAnimaux.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                int i = 0;
                foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux())
                {

                    if (i == row.Index)
                    {
                        FacadeProvider.GetInstance().AnimalFacade().SupprimerAnimal(animal.Id);
                    }
                    i++;


                }
            }

            if (selectedRows.Count > 1)
            {
                MessageBox.Show(this, "Animaux supprimés");
            }
            else
            {
                MessageBox.Show(this, "Animal supprimé");
            }

            FrmSupprimerAnimal_Load(sender, e);
        }

        private void FrmSupprimerAnimal_Load(object sender, EventArgs e)
        {
            //Vider les colonnes
            lesAnimaux.Columns.Clear();
            //Vider les lignes
            lesAnimaux.Rows.Clear();

            lesAnimaux.RowHeadersVisible = false;
            // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
            lesAnimaux.ColumnCount = 3;



            // faut-il ajuster automatiquement la taille des colonnes à leur contenu (commenter la ligne si non)
            // faut-il ajuster automatiquement la taille des colonnes par un ajustement proportionnel à la largeur totale (commenter la ligne si non)
            lesAnimaux.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lesAnimaux.Columns[0].Name = "Nom";
            lesAnimaux.Columns[0].Width = 100;
            lesAnimaux.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimaux.Columns[1].Name = "Prenom";
            lesAnimaux.Columns[1].Width = 100;
            lesAnimaux.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimaux.Columns[2].Name = "Sexe";
            lesAnimaux.Columns[2].Width = 100;
            lesAnimaux.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimaux())
            {
                if (animal.IdStatut != 1)
                {
                    lesAnimaux.Rows.Add(animal.Nom, animal.Prenom, animal.Sexe);
                }
            }
        }
    }
}
