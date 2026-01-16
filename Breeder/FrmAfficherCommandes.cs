
using iTextSharp.text.pdf;
using iTextSharp.text;
using Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breeder
{
    public partial class FrmAfficherCommandes : FrmOnglet
    {
        public FrmAfficherCommandes()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void FrmAfficherCommandes_Load(object sender, EventArgs e)
        {
            //Affichage des Commandes dans le datagridView
            lesCommandes.RowHeadersVisible = false;
            lesCommandes.ColumnCount = 5;

            lesCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesCommandes.Columns[0].Visible = false;
            lesCommandes.Columns[0].Name = "Id";

            lesCommandes.Columns[1].Name = "Libelle";
            lesCommandes.Columns[1].Width = 100;
            lesCommandes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesCommandes.Columns[2].Name = "Date";
            lesCommandes.Columns[2].Width = 100;
            lesCommandes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesCommandes.Columns[3].Name = "Total";
            lesCommandes.Columns[3].Width = 100;
            lesCommandes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesCommandes.Columns[4].Name = "Fournisseur";
            lesCommandes.Columns[4].Width = 100;
            lesCommandes.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (Commande commande in FacadeProvider.GetInstance().CommandeFacade().GetCommandes())
            {
                Fournisseur? fournisseur = FacadeProvider.GetInstance().FournisseurFacade().GetFournisseur(commande.IdFournisseur);
                if (fournisseur != null)
                {
                    lesCommandes.Rows.Add(commande.Id, commande.Libelle, commande.Date.ToString("dd/MM/yyyy"), commande.Total + " EUR", fournisseur.Libelle);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = lesCommandes.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["Id"];

            if (cell?.Value == null)
            {
                return;
            }
            Commande? commande = FacadeProvider.GetInstance().CommandeFacade().GetCommande((int)cell.Value);
            if (commande == null)
            {
                return;
            }
            Program.SwitchMainForm(new FrmAfficherCommande(commande));
        }

        private void btnToPdf_Click(object sender, EventArgs e)
        {
            // Exportation du datagridview1 en pdf
            if (lesCommandes.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Titre.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Il n'était pas possible d'écrire les données sur le disque." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(lesCommandes.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in lesCommandes.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in lesCommandes.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Donnée exportée avec succès !", "Information");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun enregistrement à exporter", "Information");
            }
        }
    }
}
