
using iTextSharp.text;
using iTextSharp.text.pdf;
using Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;

namespace Breeder
{
    public partial class FrmTableauPoids : Form
    {
        private Animal _animal;
        public FrmTableauPoids(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void FrmTableauPoids_Load(object sender, EventArgs e)
        {
            label1.Text = "Animal : " + _animal.Nom + " " + _animal.Prenom;
            label1.Visible = true;

            lesPoids.RowHeadersVisible = false;
            // Nombre de colonne sans compter les colonnes ajoutées par la méthode Add
            lesPoids.ColumnCount = 2;


            //Personnalisation des colonnes
            lesPoids.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lesPoids.Columns[0].Name = "Date";
            lesPoids.Columns[0].Width = 100;
            lesPoids.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesPoids.Columns[1].Name = "Poids";
            lesPoids.Columns[1].Width = 100;
            lesPoids.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (CourbePoids unPoids in FacadeProvider.GetInstance().PoidsFacade().GetCourbesPoids(_animal.Id))
                lesPoids.Rows.Add(unPoids.DateSaisie.ToString("dd/MM/yyyy"), unPoids.Poids + " Kg");
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmCourbePoids(_animal));
        }

        private void btnToPdf_Click(object sender, EventArgs e)
        {
            if (lesPoids.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(lesPoids.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in lesPoids.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in lesPoids.Rows)
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

