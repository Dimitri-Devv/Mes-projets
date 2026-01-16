using Stage;

namespace Breeder
{
    public partial class FrmModifierVaccin : FrmOnglet
    {
        public FrmModifierVaccin()
        {
            InitializeComponent();
        }

        private void FrmModifierVaccin_Load(object sender, EventArgs e)
        {
            lesVaccins.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (Vaccin vaccin in FacadeProvider.GetInstance().VaccinFacade().GetVaccins().Where(vaccin => vaccin.Id != 1))
            {
                lesVaccins.Items.Add(vaccin);
            }

        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Vaccin vaccin = (Vaccin)lesVaccins.SelectedItem;
            vaccin.Libelle = boxLibelle.Text;
            FacadeProvider.GetInstance().VaccinFacade().ModifierVaccin(vaccin);
            MessageBox.Show("Vaccin modifié !");
            
            lesVaccins.Items.Clear();

            FrmModifierVaccin_Load(sender, e);
        }

        private void lesVaccins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vaccin vaccin = (Vaccin)lesVaccins.SelectedItem;
            boxLibelle.Text = vaccin.Libelle;
        }

        private void boxLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
