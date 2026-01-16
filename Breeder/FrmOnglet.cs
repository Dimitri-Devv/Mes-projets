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
    public partial class FrmOnglet : Form
    {
        public FrmOnglet()
        {
            InitializeComponent();
        }

        private void accueilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void ajouterUnAnimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterAnimal());
        }

        private void supprimerUnAnimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerAnimal());
        }

        private void ajouterUnePortéeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterPortee());
        }

        private void afficherLesPortéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherPortee());
        }

        private void ajouterUnClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterClient());
        }

        private void modifierUnClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierClient());
        }

        private void supprimerUnClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerClient());
        }

        private void afficherLesClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherClient());
        }

        private void ajouterUnTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterType());
        }

        private void modifierUnTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierType());
        }

        private void supprimerUnTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerType());
        }

        private void ajouterUneRaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterRace());
        }

        private void modifierUneRaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierRace());
        }

        private void supprimerUneRaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerRace());
        }

        private void ajouterUnStatutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterStatut());
        }

        private void modifierUnStatutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierStatut());
        }

        private void supprimerUnStatutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerStatut());
        }

        private void ajouterUneCommandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterCommande());
        }

        private void modifierUneCommandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierCommande());
        }

        private void supprimerUneCommandeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerCommande());
        }

        private void afficherLesCommandesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherCommandes());
        }

        private void ajouterUnFournisseurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterFournisseur());
        }

        private void modifierUnFournisseurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierFournisseur());
        }

        private void supprimerUnFournisseurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerFournisseur());
        }

        private void afficherLesFournisseursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherFournisseurs());
        }

        private void ajouterUnVeterinaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterUnVeterinaire());
        }

        private void modifierUnVeterinaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierUnVeterinaire());
        }

        private void supprimerUnVeterinaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerUnVeterinaire());
        }

        private void afficherLesVétérinairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherLesVeterinaires());
        }

        private void ajouterUnVaccinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAjouterVaccin());
        }

        private void modifierUnVaccinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmModifierVaccin());
        }

        private void supprimerUnVaccinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmSupprimerVaccin());
        }

        private void factureDevisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmFactureDevis());
        }

        private void afficherLesAnimauxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAfficherLesAnimaux());
        }

        private void FrmOnglet_Load(object sender, EventArgs e)
        {

        }
    }
}
