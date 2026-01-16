
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
    public partial class FrmAjouterFournisseur : FrmOnglet
    {
        public FrmAjouterFournisseur()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            //Récupérer les valeurs des champs et les envoyer à la façade Fournisseur
            string leLibelle = libelle.Text;
            string Ladresse = adresse.Text;
            string leMail = mail.Text;
            string tel = telephone.Text;

            int i = 0;
            if (tel.Length == 10 && IsValidEmail(leMail) && int.TryParse(tel, out i))
            {
                FacadeProvider.GetInstance().FournisseurFacade().AjouterFournisseur(leLibelle, Ladresse, leMail, tel);
                MessageBox.Show(this, "Fournisseur ajouté");

                //Vider les champs
                libelle.Text = "";
                adresse.Text = "";
                mail.Text = "";
                telephone.Text = "";
               
            }
            else
            {
                MessageBox.Show("Format du numéro de téléphone ou de l'email incorrect");
            }
        }
        //Fonction de vérification de l'authenticité de l'adresse mail
        public static bool IsValidEmail(string email)
        {
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

        private void libelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }
}
