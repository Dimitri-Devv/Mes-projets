
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
    public partial class FrmModifierFournisseur : FrmOnglet
    {
        public FrmModifierFournisseur()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {

        }

        private void FrmModifierFournisseur_Load(object sender, EventArgs e)
        {
            lesFournisseurs.Items.Clear();
            //Charge les Fournisseurs
            foreach (Fournisseur fournisseur in FacadeProvider.GetInstance().FournisseurFacade().GetFournisseurs().Where(fournisseur => fournisseur.Id != 1))
            {
                lesFournisseurs.Items.Add(fournisseur);
            }
            lesFournisseurs.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Fournisseur fournisseur = (Fournisseur)lesFournisseurs.SelectedItem;
            fournisseur.Libelle = leLibelle.Text;
            fournisseur.Adresse = boxAdresse.Text;
            fournisseur.Mail = mail.Text;
            fournisseur.Telephone = boxTel.Text;

            int i = 0;
            if (boxTel.Text.Length == 10 && IsValidEmail(mail.Text) && int.TryParse(boxTel.Text, out i))
            {
                FacadeProvider.GetInstance().FournisseurFacade().ModifierFournisseur(fournisseur);
                MessageBox.Show("Fournisseur modifié");

                leLibelle.Text = "";
                boxAdresse.Text = "";
                boxTel.Text = "";
                mail.Text = "";
              

                FrmModifierFournisseur_Load(sender, e);
            }
            else
                MessageBox.Show("Format du numéro de téléphone ou de l'email incorrect");

        }

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

        private void lesFournisseurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fournisseur fournisseur = (Fournisseur)lesFournisseurs.SelectedItem;


            leLibelle.Text = fournisseur.Libelle;
            boxAdresse.Text = fournisseur.Adresse;
            mail.Text = fournisseur.Mail;
            boxTel.Text = fournisseur.Telephone;
        }

        private void leLibelle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }
    }


}