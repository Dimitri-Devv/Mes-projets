using Stage;

namespace Breeder
{
    public partial class FrmSupprimerUnVeterinaire : FrmOnglet
    {
        public FrmSupprimerUnVeterinaire()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            //Crée un dialogResult permettant d'ajouter une sécurité pour éviter les suppressions par erreur
            DialogResult result = MessageBox.Show("Êtes-vous sur ?", "Suppresion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Veterinaire veterinaire = (Veterinaire)lesVetos.SelectedItem;

                FacadeProvider.GetInstance().VeterinaireFacade().SupprimerVeterinaire(veterinaire.Id);

                lesVetos.Items.Remove(veterinaire);
                lesVetos.SelectedItem = null;
                lesVetos.Text = "";

                MessageBox.Show(this, "Vétérinaire supprimé");
            }
        }

        private void FrmSupprimerUnVeterinaire_Load(object sender, EventArgs e)
        {
            //Charge les Vétérinaires
            foreach (Veterinaire veterinaire in FacadeProvider.GetInstance().VeterinaireFacade().getVeterinaires().Where(veterinaire => veterinaire.Id != 1))
            {
                lesVetos.Items.Add(veterinaire);
            }
            lesVetos.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
