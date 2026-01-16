
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
    public partial class FrmModifierClient : FrmOnglet
    {
        public FrmModifierClient()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void Modifier_Click(object sender, EventArgs e)
        {
            Client client = (Client)lesClients.SelectedItem;
            client.Nom = leNom.Text;
            client.Prenom = Prenom.Text;
            client.Adresse = boxAdresse.Text;
            client.Mail = boxMail.Text;
            client.Telephone = boxTel.Text;
            client.IdNiveau = ((Niveau)lesNiveaux.SelectedItem).Id;

            int i = 0;
            if (boxTel.Text.Length == 10 && IsValidEmail(boxMail.Text) && int.TryParse(boxTel.Text, out i))
            {
                FacadeProvider.GetInstance().ClientFacade().ModifierClient(client);
                MessageBox.Show("Client modifié");

                leNom.Text = "";
                Prenom.Text = "";
                boxAdresse.Text = "";
                boxMail.Text = "";
                boxTel.Text = "";
                lesNiveaux.SelectedIndex = 0;
                lesClients.SelectedIndex = 0;

                FrmModifierClient_Load(sender, e);
            }
            else
                MessageBox.Show("Format du numéro de téléphone ou de l'email incorrect");

        }

        private void FrmModifierClient_Load(object sender, EventArgs e)
        {
            lesClients.Items.Clear();
            lesNiveaux.Items.Clear();
            //Charge les Clients
            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
            {
                lesClients.Items.Add(client);
            }
            lesClients.DropDownStyle = ComboBoxStyle.DropDownList;

            //Charge les Niveaux
            foreach (Niveau niveau in FacadeProvider.GetInstance().NiveauFacade().GetNiveaux())
            {
                lesNiveaux.Items.Add(niveau);
            }
            lesNiveaux.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void lesClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client client = (Client)lesClients.SelectedItem;
            string nom = FacadeProvider.GetInstance().ClientFacade().GetClient(client.Id).Nom;
            string prenom = FacadeProvider.GetInstance().ClientFacade().GetClient(client.Id).Prenom;
            string adresse = FacadeProvider.GetInstance().ClientFacade().GetClient(client.Id).Adresse;
            string mail = FacadeProvider.GetInstance().ClientFacade().GetClient(client.Id).Mail;
            string telephone = FacadeProvider.GetInstance().ClientFacade().GetClient(client.Id).Telephone;
            Niveau leNiveau = FacadeProvider.GetInstance().NiveauFacade().GetNiveau(client.IdNiveau);

            leNom.Text = nom;
            Prenom.Text = prenom;
            boxAdresse.Text = adresse;
            boxMail.Text = mail;
            boxTel.Text = telephone;
            lesNiveaux.SelectedItem = leNiveau;

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

        private void leNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

        private void Prenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
                e.Handled = true;
        }

    }
}
