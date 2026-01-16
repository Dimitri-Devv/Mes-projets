
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
    public partial class FrmAfficherCommande : FrmOnglet
    {
        private Commande _commande;

        public FrmAfficherCommande(Commande commande)
        {
            InitializeComponent();

            _commande = commande;
        }

        private void FrmAfficherCommande_Load(object sender, EventArgs e)
        {
            //Affichage des animaux avec le côut par animal
            lesAnimauxConcernés.RowHeadersVisible = false;
            lesAnimauxConcernés.ColumnCount = 4;

            lesAnimauxConcernés.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            lesAnimauxConcernés.Columns[0].Name = "Nom";
            lesAnimauxConcernés.Columns[0].Width = 100;
            lesAnimauxConcernés.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcernés.Columns[1].Name = "Prenom";
            lesAnimauxConcernés.Columns[1].Width = 100;
            lesAnimauxConcernés.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcernés.Columns[2].Name = "Sexe";
            lesAnimauxConcernés.Columns[2].Width = 100;
            lesAnimauxConcernés.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesAnimauxConcernés.Columns[3].Name = "Côut";
            lesAnimauxConcernés.Columns[3].Width = 100;
            lesAnimauxConcernés.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            List<Animal> animals = FacadeProvider.GetInstance().ListeCommandeFacade().GetAnimauxByCommande(_commande.Id).ToList();
            decimal totalParAnimal = Math.Ceiling(_commande.Total / (animals.Count > 0 ? animals.Count : 1));

            foreach (Animal animal in animals)
            {
                lesAnimauxConcernés.Rows.Add(animal.Nom, animal.Prenom, animal.Sexe, totalParAnimal + " EUR");
            }

            leLibelle.Text = _commande.Libelle;

            Fournisseur? fournisseur = FacadeProvider.GetInstance().FournisseurFacade().GetFournisseur(_commande.IdFournisseur);
            if (fournisseur != null)
            {

                leFournisseur.Text = fournisseur.Libelle;
            }
            else
            {
                leFournisseur.Text = "Aucun";
            }

            laDate.Text = _commande.Date.ToString("dd/MM/yyyy");

            leTotal.Text = _commande.Total.ToString();


        }

        private void btnQuitter_Click_1(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherCommandes());
        }
    }
}
