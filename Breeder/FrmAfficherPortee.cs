using Stage;

namespace Breeder
{
    public partial class FrmAfficherPortee : FrmOnglet
    {
        public FrmAfficherPortee()
        {
            InitializeComponent();
        }
        
        private int IdPortee { get; set; }
        
        private void FrmAfficherPortee_Load(object sender, EventArgs e)
        {
            //Charge les mères d'une portée
            foreach (Animal mere in FacadeProvider.GetInstance().AnimalFacade().GetMeresPortees())
            {
                boxMere.Items.Add(mere);
            }

            if (boxMere.Items.Count > 0)
            {
                Animal mere = (Animal)boxMere.SelectedItem;
                boxMere.SelectedIndex = 0;
            }

            parametrerDgvPortees();
            chargerDgvPortees();

            parametrerDgvPortee();
        }

        private void parametrerDgvPortees()
        {
            dgvPortees.RowHeadersVisible = false;
            dgvPortees.ColumnCount = 4;
            dgvPortees.Rows.Clear();

            dgvPortees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPortees.Columns[0].Name = "Mère";
            dgvPortees.Columns[0].Width = 100;
            dgvPortees.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPortees.Columns[1].Name = "Libellé";
            dgvPortees.Columns[1].Width = 100;
            dgvPortees.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPortees.Columns[2].Name = "Date";
            dgvPortees.Columns[2].Width = 100;
            dgvPortees.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /**
             * author: Hugo Ilarraz
             * 
             * Ajouter une colonne "portee" qui contient l'objet Portee
             * On retire la visiblité pour la cacher mais pour autant récupérer la valeur 
             * 
             */
            dgvPortees.Columns[3].Visible = false;
            dgvPortees.Columns[3].Name = "idPortee";
        }

        private void parametrerDgvPortee()
        {
            dgvPortee.RowHeadersVisible = false;
            dgvPortee.ColumnCount = 4;
            dgvPortee.Rows.Clear();
            dgvPortee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPortee.Columns[0].Visible = false;
            dgvPortee.Columns[0].Name = "id";

            dgvPortee.Columns[1].Name = "Nom";
            dgvPortee.Columns[1].Width = 100;
            dgvPortee.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPortee.Columns[2].Name = "Prenom";
            dgvPortee.Columns[2].Width = 100;
            dgvPortee.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPortee.Columns[3].Name = "Sexe";
            dgvPortee.Columns[3].Width = 100;
            dgvPortee.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void chargerDgvPortees()
        {
            //On vide les lignes du datagridView portées pour les remplir
            dgvPortees.Rows.Clear();
            foreach (Portee portee in FacadeProvider.GetInstance().PorteeFacade().GetPortees())
            {
                Animal? mere = FacadeProvider.GetInstance().AnimalFacade().GetAnimal(portee.IdAnimal);
                if (mere == null)
                {
                    continue;
                }
                dgvPortees.Rows.Add(mere.ToString(), portee.Libelle, portee.Date.ToString("dd/MM/yyyy"), portee.Id);
            }
        }

        private void chargerDgvPortee()
        {
            //On vide les lignes du datagridView portée pour les remplir
            dgvPortee.Rows.Clear();
            foreach (Animal enfant in FacadeProvider.GetInstance().AnimalFacade().GetEnfantsPortees(IdPortee))
            {
                dgvPortee.Rows.Add(enfant.Id, enfant.Nom, enfant.Prenom, enfant.Sexe.ToString());
            }
        }

        private void chargerMere(Animal animal, Portee portee)
        {
            IdPortee = portee.Id;
            boxMere.SelectedItem = animal;
            boxDate.Value = portee.Date;
            boxLibelle.Text = portee.Libelle;

            chargerDgvPortee();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            //Recuperer les données et les envoyer à la Façade portée
            Portee? portee = FacadeProvider.GetInstance().PorteeFacade().GetPortee(IdPortee);
            portee.Libelle = boxLibelle.Text;

            dgvPortee.Rows.Clear();
            chargerDgvPortees();

            FacadeProvider.GetInstance().PorteeFacade().ModifierPortee(portee);
            MessageBox.Show(this, "Modifications enregistrées !");
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void dgvPortees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = dgvPortees.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["idPortee"];

            if (cell?.Value != null)
            {
                Portee portee = FacadeProvider.GetInstance().PorteeFacade().GetPortee((int)cell.Value);
                Animal mere = FacadeProvider.GetInstance().AnimalFacade().GetAnimal(portee.IdAnimal);
                boxMere.SelectedItem = mere;
                boxDate.Value = portee.Date;

                chargerMere(mere, portee);
            }
        }

        private void dgvPortee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = dgvPortee.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["id"];

            if (cell?.Value != null)
            {
                Animal enfant = FacadeProvider.GetInstance().AnimalFacade().GetAnimal((int)cell.Value);
                Program.SwitchMainForm(new FrmProfilAnimal(enfant));
            }
        }
    }
}
