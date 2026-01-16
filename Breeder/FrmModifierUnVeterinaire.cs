
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
    public partial class FrmModifierUnVeterinaire : FrmOnglet
    {
        public FrmModifierUnVeterinaire()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Veterinaire veterinaire = (Veterinaire)lesVetos.SelectedItem;
            veterinaire.Nom = boxNom.Text;
            veterinaire.Adresse = boxAdresse.Text;
            veterinaire.Mail = boxMail.Text;
            veterinaire.Telephone = boxTel.Text;

            int i = 0;
            if (boxTel.Text.Length == 10 && IsValidEmail(boxMail.Text) && int.TryParse(boxTel.Text, out i))
            {
                FacadeProvider.GetInstance().VeterinaireFacade().ModifierVeterinaire(veterinaire);
                MessageBox.Show("Client modifié");

                boxNom.Text = "";
                boxAdresse.Text = "";
                boxMail.Text = "";
                boxTel.Text = "";

                FrmModifierUnVeterinaire_Load(sender, e);
            }
            else
                MessageBox.Show("Format du numéro de téléphone ou de l'email incorrect");

        }

        private void FrmModifierUnVeterinaire_Load(object sender, EventArgs e)
        {
            lesVetos.Items.Clear();
            //Charge les Vétérinaires
            foreach (Veterinaire veterinaire in FacadeProvider.GetInstance().VeterinaireFacade().getVeterinaires().Where(veterinaire => veterinaire.Id != 1))
            {
                lesVetos.Items.Add(veterinaire);
            }
            lesVetos.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private void lesVetos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Veterinaire veterinaire = (Veterinaire)lesVetos.SelectedItem;
            string nom = FacadeProvider.GetInstance().VeterinaireFacade().GetVeterinaire(veterinaire.Id).Nom;
            string adresse = FacadeProvider.GetInstance().VeterinaireFacade().GetVeterinaire(veterinaire.Id).Adresse;
            string mail = FacadeProvider.GetInstance().VeterinaireFacade().GetVeterinaire(veterinaire.Id).Mail;
            string telephone = FacadeProvider.GetInstance().VeterinaireFacade().GetVeterinaire(veterinaire.Id).Telephone;

            boxNom.Text = nom;
            boxAdresse.Text = adresse;
            boxMail.Text = mail;
            boxTel.Text = telephone;
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

        private void boxNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

    }
}
