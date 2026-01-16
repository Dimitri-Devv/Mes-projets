
using Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breeder
{
    public partial class FrmAjouterClient : FrmOnglet
    {
        public FrmAjouterClient()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FrmAjouterClient_Load(object sender, EventArgs e)
        {
            //Charge les différents niveau que peuvent avoir un client dans un combobox
            foreach (Niveau niveau in FacadeProvider.GetInstance().NiveauFacade().GetNiveaux())
            {
                textNiveau.Items.Add(niveau);
            }
            textNiveau.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            //Récupérer les valeurs des champs et les envoyer à la façade Animal
            string nom = textNom.Text;
            string prenom = textPrenom.Text;
            string adresse = boxAdresse.Text;
            string mail = boxMail.Text;
            string tel = boxTel.Text;
            Niveau leNiveau = (Niveau)textNiveau.SelectedItem;

            int i = 0;
            if (tel.Length == 10 && IsValidEmail(mail) && int.TryParse(tel, out i))
            {
                //Envoyer les données à la façade Client en utilisant la méthode AjouterClient
                FacadeProvider.GetInstance().ClientFacade().AjouterClient(nom, prenom, adresse, mail, tel, leNiveau.Id);
                MessageBox.Show(this, "Client ajouté");

                //Vider les champs
                textNom.Text = "";
                textPrenom.Text = "";
                boxAdresse.Text = "";
                boxMail.Text = "";
                boxTel.Text = "";
                textNiveau.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Format du numéro de téléphone ou de l'email incorrect");
            }

        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        //Fonction qui vérifie l'authenticité de l'adresse mail
        public static bool IsValidEmail(string email)
        {
            //Vérifie si l'adresse mail n'est pas vide ou juste un espace
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void textNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void textPrenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
