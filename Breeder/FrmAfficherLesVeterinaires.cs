
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
    public partial class FrmAfficherLesVeterinaires : FrmOnglet
    {
        public FrmAfficherLesVeterinaires()
        {
            InitializeComponent();
        }

        private void FrmAfficherLesVeterinaires_Load(object sender, EventArgs e)
        {
            //Affichage des Commandes dans le datagridView
            lesVetos.RowHeadersVisible = false;
            lesVetos.ColumnCount = 5;

            lesVetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesVetos.Columns[0].Visible = false;
            lesVetos.Columns[0].Name = "Id";

            lesVetos.Columns[1].Name = "Nom";
            lesVetos.Columns[1].Width = 100;
            lesVetos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesVetos.Columns[2].Name = "Mail";
            lesVetos.Columns[2].Width = 100;
            lesVetos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesVetos.Columns[3].Name = "Telephone";
            lesVetos.Columns[3].Width = 100;
            lesVetos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesVetos.Columns[4].Name = "Adresse";
            lesVetos.Columns[4].Width = 100;
            lesVetos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            foreach (Veterinaire veterinaire in FacadeProvider.GetInstance().VeterinaireFacade().getVeterinaires().Where(veterinaire => veterinaire.Id != 1))
            {
                lesVetos.Rows.Add(veterinaire.Id, veterinaire.Nom, veterinaire.Mail, veterinaire.Telephone, veterinaire.Adresse);
            }
        }

        private void btnToPdf_Click(object sender, EventArgs e)
        {
            // Exportation du datagridview1 en pdf
            if (lesVetos.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(lesVetos.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in lesVetos.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in lesVetos.Rows)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si on clique sur la ligne des colonnes (index = -1) on annule
            if (e.RowIndex <= -1)
            {
                return;
            }

            DataGridViewRow row = lesVetos.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells["Id"];

            if (cell?.Value == null)
            {
                return;
            } 
            Veterinaire? veterinaire = FacadeProvider.GetInstance().VeterinaireFacade().GetVeterinaire((int)cell.Value);
            if (veterinaire == null)
            {
                return;
            }
            Program.SwitchMainForm(new FrmAfficherVeterinaire(veterinaire));
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }
    }
}
