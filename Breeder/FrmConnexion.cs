using Breeder;

namespace Stage
{
    public partial class FrmConnexion : Form
    {
        public FrmConnexion()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string identifiant = textIdentifiant.Text;
            string motDePasse = textMdp.Text;

            // Vérification de l'identifiant et du mot de passe
            if(FacadeProvider.GetInstance().UserFacade().VerifierConnexion(identifiant, motDePasse))
            {
                Program.SwitchMainForm(new FrmAccueil());
            }
            else
            {
                MessageBox.Show("Identifiant/Mot de passe incorrecte");
            }

            
        }

        private void FrmConnexion_Load(object sender, EventArgs e)
        {
            textMdp.PasswordChar = '*';
            textIdentifiant.Text = "admin";
            textMdp.Text = "admin"; 
        }
    }
}