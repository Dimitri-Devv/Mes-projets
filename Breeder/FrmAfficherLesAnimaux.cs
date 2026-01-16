namespace Breeder;

public partial class FrmAfficherLesAnimaux : FrmOnglet {
    public FrmAfficherLesAnimaux() {
        InitializeComponent();
    }

    private void FrmAfficherLesAnimaux_Load(object sender, EventArgs e) {
        dgvAnimaux.RowHeadersVisible = false;
        // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
        dgvAnimaux.ColumnCount = 4;

        //Personnalisation des colonnes
        dgvAnimaux.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        dgvAnimaux.Columns[0].Visible = false;
        dgvAnimaux.Columns[0].Name = "Id";

        dgvAnimaux.Columns[1].Name = "Nom";
        dgvAnimaux.Columns[1].Width = 100;
        dgvAnimaux.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        dgvAnimaux.Columns[2].Name = "Prenom";
        dgvAnimaux.Columns[2].Width = 100;
        dgvAnimaux.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        dgvAnimaux.Columns[3].Name = "Sexe";
        dgvAnimaux.Columns[3].Width = 100;
        dgvAnimaux.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //Ajoute chaque animaux avec ce qu'on a besoin sauf celui qui a un id = 1
        foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxSansInconnu()) {
            dgvAnimaux.Rows.Add(animal.Id, animal.Nom, animal.Prenom, animal.Sexe);
        }
    }

    private void lesAnimaux_CellClick(object sender, DataGridViewCellEventArgs e) {
        // Si on clique sur la ligne des colonnes (index = -1) on annule
        if (e.RowIndex <= -1) {
            return;
        }

        DataGridViewRow row = dgvAnimaux.Rows[e.RowIndex];
        DataGridViewCell cell = row.Cells["Id"];

        if (cell?.Value == null) {
            return;
        }

        Animal? animal = FacadeProvider.GetInstance().AnimalFacade().GetAnimal((int)cell.Value);
        if (animal == null) {
            return;
        }

        Program.SwitchMainForm(new FrmProfilAnimal(animal));
    }
}