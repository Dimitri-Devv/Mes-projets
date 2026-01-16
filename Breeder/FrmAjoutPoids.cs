
using Stage;

namespace Breeder
{
    public partial class FrmAjoutPoids : FrmOnglet
    {
        private Animal _animal;
        public FrmAjoutPoids(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void btnAjout_Click(object sender, EventArgs e)
        {
            decimal poids = lePoids.Value;


            if (g.Checked)
            {
                FacadeProvider.GetInstance().PoidsFacade().AjouterPoids(_animal.Id,ConversionUtils.GrammesToKilogrammes(poids));
            }
            else
            {
                FacadeProvider.GetInstance().PoidsFacade().AjouterPoids(_animal.Id, poids);
            }
            _animal.Poids = poids;
            FacadeProvider.GetInstance().AnimalFacade().ModifierAnimal(_animal);

            MessageBox.Show("Poids enregistré");
            Program.SwitchMainForm(new FrmCourbePoids(_animal));

        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmCourbePoids(_animal));
        }

        private void FrmAjoutPoids_Load(object sender, EventArgs e)
        {
            g.Checked = true;
        }
    }
}
