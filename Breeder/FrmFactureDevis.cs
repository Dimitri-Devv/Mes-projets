using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using Font = iTextSharp.text.Font;

namespace Breeder
{
    public partial class FrmFactureDevis : FrmOnglet
    {
        public FrmFactureDevis()
        {
            InitializeComponent();
        }

        private void FrmFactureDevis_Load(object sender, EventArgs e)
        {
            btnFacture.Checked = true;

            //DataGridView Clients
            lesClients.RowHeadersVisible = false;
            lesClients.ColumnCount = 3;

            lesClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lesClients.Columns[0].Name = "Client";
            lesClients.Columns[0].Visible = false;

            lesClients.Columns[1].Name = "Nom";
            lesClients.Columns[1].Width = 100;
            lesClients.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            lesClients.Columns[2].Name = "Prenom";
            lesClients.Columns[2].Width = 100;
            lesClients.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Client client in FacadeProvider.GetInstance().ClientFacade().GetClients())
            {
                lesClients.Rows.Add(client, client.Nom, client.Prenom);
            }

            //DataGridView Animaux
            Animal.RowHeadersVisible = false;
            Animal.ColumnCount = 4;

            Animal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Animal.Columns[0].Visible = false;
            Animal.Columns[0].Name = "Animal";

            Animal.Columns[1].Name = "Nom";
            Animal.Columns[1].Width = 100;
            Animal.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Animal.Columns[2].Name = "Prenom";
            Animal.Columns[2].Width = 100;
            Animal.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Animal.Columns[3].Name = "Sexe";
            Animal.Columns[3].Width = 100;
            Animal.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (Animal animal in FacadeProvider.GetInstance().AnimalFacade().GetAnimauxSansInconnu().Where(animal => animal.IdStatut != 2 && animal.IdStatut != 3 && animal.IdStatut != 7))
            {
                Animal.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }


            //Affichage des animaux concernés dans le datagridView
            AnimalConcerné.RowHeadersVisible = false;
            AnimalConcerné.ColumnCount = 5;

            AnimalConcerné.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            AnimalConcerné.Columns[0].Visible = false;
            AnimalConcerné.Columns[0].Name = "Animal";

            AnimalConcerné.Columns[1].Name = "Nom";
            AnimalConcerné.Columns[1].Width = 100;
            AnimalConcerné.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AnimalConcerné.Columns[1].ReadOnly = true;

            AnimalConcerné.Columns[2].Name = "Prenom";
            AnimalConcerné.Columns[2].Width = 100;
            AnimalConcerné.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AnimalConcerné.Columns[2].ReadOnly = true;

            AnimalConcerné.Columns[3].Name = "Sexe";
            AnimalConcerné.Columns[3].Width = 100;
            AnimalConcerné.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            AnimalConcerné.Columns[3].ReadOnly = true;

            AnimalConcerné.Columns[4].Name = "Prix";
            AnimalConcerné.Columns[4].Width = 100;
            AnimalConcerné.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Animal.RowHeadersVisible = false;


        }

        private void btnExporter_Click(object sender, EventArgs e)
        {
            string titre = btnDevis.Checked ? "DEVIS" : "FACTURE";




            DataGridViewRow row1 = lesClients.SelectedRows[0];
            Client leClient = (Client)row1.Cells["Client"].Value;




            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (*.pdf)|*.pdf";
            sfd.FileName = titre + ".pdf";
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
                        // On crée le FileStream qui va permettre d'écrire dans le fichier
                        using FileStream stream = new FileStream(sfd.FileName, FileMode.Create);
                        // Le document qui sera transformer en données dans le fichier
                        Document doc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                        // Pour écrire en pdf dans le fichier à partir du stream (données)
                        PdfWriter.GetInstance(doc, stream);
                        doc.Open();

                        // Palette de couleurs
                        BaseColor blue = new BaseColor(0, 75, 155);
                        BaseColor gris = new BaseColor(240, 240, 240);
                        BaseColor blanc = new BaseColor(255, 255, 255);
                        BaseColor noir = new BaseColor(0, 0, 0);

                        //Polices d'écriture
                        Font policeTitre = new Font(iTextSharp.text.Font.HELVETICA, 25f, iTextSharp.text.Font.BOLD,
                            blue);
                        Font policeTh = new Font(iTextSharp.text.Font.HELVETICA, 16f, iTextSharp.text.Font.BOLD,
                            blanc);
                        Font policeText = new Font(iTextSharp.text.Font.HELVETICA, 10f, iTextSharp.text.Font.BOLD,
                            noir);
                        Font policeSousTitre = new Font(iTextSharp.text.Font.HELVETICA, 15f, iTextSharp.text.Font.BOLD,
                            blue);

                        doc.NewPage();
                        //Ecriture des paragraphes
                        Paragraph p1 = new Paragraph("MON LOGO", policeTitre);
                        p1.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p1);


                        Paragraph p2 = new Paragraph(titre, policeTitre);
                        p2.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(p2);

                        Paragraph p3 = new Paragraph(DateTime.Today.ToString("dd/MM/yyyy"), policeText);
                        p3.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(p3);

                        Paragraph p4 = new Paragraph("Alain Gérard \n" + "1, rue du diamant doré \n" + "75018 Paris \n"
                            + "alain.gerard@email.com \n" + "SIREN : 010101010 \n" + "RCS PARIS 821 XXX UUU \n", policeText);
                        p4.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p4);

                        Paragraph p5 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, noir,
                            Element.ALIGN_LEFT, 1)));
                        doc.Add(p5);

                        doc.Add(new Phrase("\n"));




                        Paragraph p6 = new Paragraph("À l’attention de \n" + leClient.Nom + " " + leClient.Prenom + "\n" + leClient.Adresse +
                                                      "\n", policeText);
                        p6.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p6);

                        Paragraph p7 = new Paragraph("Date : \n" + DateTime.Today.ToString("dd/MM/yyyy"), policeText);
                        p7.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(p7);

                        doc.Add(new Phrase("\n"));

                        Paragraph pSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, noir,
                            Element.ALIGN_LEFT, 1)));
                        doc.Add(pSeparator);

                        int prixTotalAnimauxTTC = 0;


                        foreach (DataGridViewRow row in AnimalConcerné.Rows)
                        {
                            doc.NewPage();
                            DataGridViewCell cell = row.Cells["Animal"];
                            Animal animal = (Animal)cell.Value;

                            if (btnReserve.Checked)
                            {
                                FacadeProvider.GetInstance().AnimalFacade().ModifierStatut(animal.Id, 7);
                                FacadeProvider.GetInstance().ClientAnimalFacade().AjouterClientAnimal(leClient.Id, animal.Id);
                            }else if(btnVente.Checked) {
                                FacadeProvider.GetInstance().AnimalFacade().ModifierStatut(animal.Id, 3);
                                FacadeProvider.GetInstance().ClientAnimalFacade().AjouterClientAnimal(leClient.Id, animal.Id);
                            }


                            //Le prix de l'animal
                            int lePrix = Int32.Parse(row.Cells["Prix"].Value.ToString());


                            Paragraph pTableauAnimaux = new Paragraph("ANIMAUX CONCERNES", policeSousTitre);
                            pTableauAnimaux.Alignment = Element.ALIGN_CENTER;
                            doc.Add(pTableauAnimaux);

                            doc.Add(new Phrase("\n"));

                            //Création du tableau des animaux concernés
                            PdfPTable tableauAnimaux = new PdfPTable(2);
                            tableauAnimaux.WidthPercentage = 100;

                            //Cel du tableau
                            PdfPCell nomCellAnimaux = new PdfPCell(new Phrase("Nom et prénom", policeTh));
                            nomCellAnimaux.Padding = 7;
                            nomCellAnimaux.BackgroundColor = blue;
                            nomCellAnimaux.BorderColor = blue;
                            nomCellAnimaux.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableauAnimaux.AddCell(nomCellAnimaux);

                            PdfPCell prixCellAnimaux = new PdfPCell(new Phrase("Prix", policeTh));
                            prixCellAnimaux.Padding = 7;
                            prixCellAnimaux.BackgroundColor = blue;
                            prixCellAnimaux.BorderColor = blue;
                            prixCellAnimaux.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableauAnimaux.AddCell(prixCellAnimaux);


                            //Remplir le tableau

                            PdfPCell cellAnimaux = new PdfPCell(new Phrase(animal.Nom + " " + animal.Prenom));
                            cellAnimaux.Padding = 7;
                            cellAnimaux.BackgroundColor = gris;
                            cellAnimaux.BorderColor = gris;
                            cellAnimaux.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableauAnimaux.AddCell(cellAnimaux);

                            PdfPCell cellPrix =
                            new PdfPCell(new Phrase(lePrix + " EUR"));
                            cellPrix.Padding = 7;
                            cellPrix.BackgroundColor = gris;
                            cellPrix.BorderColor = gris;
                            cellPrix.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableauAnimaux.AddCell(cellPrix);
                            doc.Add(tableauAnimaux);

                           

                            doc.Add(new Phrase("\n"));

                            Paragraph ptableau = new Paragraph("DEPENSES DE L'ANIMAL \n", policeSousTitre);
                            ptableau.Alignment = Element.ALIGN_CENTER;
                            doc.Add(ptableau);

                            doc.Add(new Phrase("\n"));

                            //Création du tableau des dépenses
                            PdfPTable tableau = new PdfPTable(2);
                            tableau.WidthPercentage = 100;

                            //Cel du tableau
                            PdfPCell libelleCell = new PdfPCell(new Phrase("Commande", policeTh));
                            libelleCell.Padding = 7;
                            libelleCell.BackgroundColor = blue;
                            libelleCell.BorderColor = blue;
                            libelleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableau.AddCell(libelleCell);

                            PdfPCell prixCell = new PdfPCell(new Phrase("Prix", policeTh));
                            prixCell.Padding = 7;
                            prixCell.BackgroundColor = blue;
                            prixCell.BorderColor = blue;
                            prixCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableau.AddCell(prixCell);

                            //prix total des dépenses
                            decimal totalDepense = 0;

                            //Remplir le tableau
                            foreach (DepenseAnimal depenseAnimal in FacadeProvider.GetInstance().ListeCommandeFacade()
                                         .GetDepensesAnimal(animal.Id))
                            {
                                PdfPCell cell3 = new PdfPCell(new Phrase(depenseAnimal.Commande.Libelle));
                                cell3.Padding = 7;
                                cell3.BackgroundColor = gris;
                                cell3.BorderColor = gris;
                                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableau.AddCell(cell3);

                                PdfPCell cell4 =
                                    new PdfPCell(new Phrase(depenseAnimal.Cout.ToString(CultureInfo.InvariantCulture) + " EUR"));
                                cell4.Padding = 7;
                                cell4.BackgroundColor = gris;
                                cell4.BorderColor = gris;
                                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableau.AddCell(cell4);

                                totalDepense += depenseAnimal.Cout;
                            }


                            doc.Add(tableau);

                            //Calcul
                            decimal prixHTC = lePrix + totalDepense;

                            decimal prixTTC = prixHTC * (decimal)1.2;

                            prixTotalAnimauxTTC += (int)prixTTC;

                            Paragraph p8 = new Paragraph("Prix total HTC : " + prixHTC + " EUR \n" + "TVA : 20% \n" + "Prix total TTC : " +
                                                         prixTTC + " EUR \n" + "TVA non applicable, article 293B du code général des impôts \n", policeText);
                            p8.Alignment = Element.ALIGN_RIGHT;
                            doc.Add(p8);



                            doc.Add(new Phrase("\n"));


                            List<Vaccination> vaccins = FacadeProvider.GetInstance().ListeAnimauxVaccinsFacade()
                             .GetVaccinsByAnimal(animal.Id);

                            if (vaccins.Count > 0)
                            {



                                Paragraph p10 = new Paragraph("VACCIN DE L'ANIMAL \n", policeSousTitre);
                                p10.Alignment = Element.ALIGN_CENTER;
                                doc.Add(p10);

                                PdfPTable tableauVaccin = new PdfPTable(2);
                                tableauVaccin.WidthPercentage = 100;

                                //Cel du tableau
                                PdfPCell nomVaccin = new PdfPCell(new Phrase("Nom du vaccin", policeTh));
                                nomVaccin.Padding = 7;
                                nomVaccin.BackgroundColor = blue;
                                nomVaccin.BorderColor = blue;
                                nomVaccin.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableauVaccin.AddCell(nomVaccin);

                                PdfPCell dateVaccin = new PdfPCell(new Phrase("Date de vaccination", policeTh));
                                dateVaccin.Padding = 7;
                                dateVaccin.BackgroundColor = blue;
                                dateVaccin.BorderColor = blue;
                                dateVaccin.HorizontalAlignment = Element.ALIGN_CENTER;
                                tableauVaccin.AddCell(dateVaccin);

                                foreach (Vaccination vaccination in vaccins)
                                {
                                    PdfPCell nomVaccinValue = new PdfPCell(new Phrase(vaccination.Vaccin.Libelle));
                                    nomVaccinValue.Padding = 7;
                                    nomVaccinValue.BackgroundColor = gris;
                                    nomVaccinValue.BorderColor = gris;
                                    nomVaccinValue.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableauVaccin.AddCell(nomVaccinValue);

                                    PdfPCell dateVaccinValue = new PdfPCell(new Phrase(vaccination.Date.ToString("dd/MM/yyyy")));
                                    dateVaccinValue.Padding = 7;
                                    dateVaccinValue.BackgroundColor = gris;
                                    dateVaccinValue.BorderColor = gris;
                                    dateVaccinValue.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tableauVaccin.AddCell(dateVaccinValue);
                                }

                                doc.Add(new Phrase("\n"));

                                doc.Add(tableauVaccin);
                                doc.NewPage();


                            }


                        }



                        Paragraph pligne = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, noir,
                            Element.ALIGN_LEFT, 1)));
                        doc.Add(pligne);

                        Paragraph pTotalPrix = new Paragraph("Prix total : \n" + prixTotalAnimauxTTC + " EUR", policeSousTitre);
                        pTotalPrix.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(pTotalPrix);

                        doc.Add(new Phrase("\n"));


                        Paragraph p11 = new Paragraph("Echeance", policeSousTitre);
                        p11.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p11);

                        Paragraph p12 = new Paragraph("Immédiate", policeText);
                        p12.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p12);


                        Paragraph p13 = new Paragraph("Conditions", policeSousTitre);
                        p13.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p13);

                        Paragraph p14 = new Paragraph("Paiement à réception \n" + "En cas de retard de paiement, " +
                                                      "application d'une indemnité forfaitaire pour frais de recouvrement de 40€ selon " +
                                                      "l'article D 441-5 du code du commerce ", policeText);
                        p14.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p14);
                        doc.Add(new Phrase("\n"));

                        Paragraph p15 = new Paragraph("Détails bancaires", policeSousTitre);
                        p15.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p15);


                        Paragraph p16 = new Paragraph("Banque : MA BANQUE PARIS \n" + "IBAN : FR76 ABCDEFABCDEABCDE III", policeText);
                        p16.Alignment = Element.ALIGN_LEFT;
                        doc.Add(p16);




                        MessageBox.Show("Devis/Facture exporté(e) avec succès !", "Devis/Facture");

                        doc.Close();
                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur :" + ex.Message);
                    }
                }
            }
            AnimalConcerné.Rows.Clear();
            Animal.Rows.Clear();
            lesClients.Rows.Clear();
            FrmFactureDevis_Load(sender, e);


        }

        private void btnToutDeplacer_Click(object sender, EventArgs e)
        {
            //Lors du clique , cela enlève les lignes du dataGridView2 et les ajoute dans l'autre dataGridView
            foreach (DataGridViewRow row in Animal.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                AnimalConcerné.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe, 500);
            }

            Animal.Rows.Clear();
        }

        private void btnDeplacer_Click(object sender, EventArgs e)
        {
            //Lors du clique, cela enlève la ligne du datagridView1 et l'ajoute dans l'autre dataGridView
            DataGridViewSelectedRowCollection selectedRows = Animal.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                Animal.Rows.Remove(row);

                AnimalConcerné.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe, 500);
            }
        }

        private void btnToutRetirer_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in AnimalConcerné.Rows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                Animal.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }

            AnimalConcerné.Rows.Clear();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = AnimalConcerné.SelectedRows;

            foreach (DataGridViewRow row in selectedRows)
            {
                Animal animal = (Animal)row.Cells["Animal"].Value;
                AnimalConcerné.Rows.Remove(row);
                Animal.Rows.Add(animal, animal.Nom, animal.Prenom, animal.Sexe);
            }
        }
    }
}