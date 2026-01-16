namespace Breeder
{
    partial class FrmAjouterAnimal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lesClients = new System.Windows.Forms.ComboBox();
            this.proprietaire = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textPoids = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sexeBox = new System.Windows.Forms.ComboBox();
            this.textDateNaissance = new System.Windows.Forms.DateTimePicker();
            this.textNom = new System.Windows.Forms.TextBox();
            this.textPrenom = new System.Windows.Forms.TextBox();
            this.textStatut = new System.Windows.Forms.ComboBox();
            this.textType = new System.Windows.Forms.ComboBox();
            this.textRace = new System.Windows.Forms.ComboBox();
            this.textPere = new System.Windows.Forms.ComboBox();
            this.textMere = new System.Windows.Forms.ComboBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Pere = new System.Windows.Forms.Label();
            this.dateNaissance = new System.Windows.Forms.Label();
            this.Prenom = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textPoids)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lesClients);
            this.panel1.Controls.Add(this.proprietaire);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textPoids);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.sexeBox);
            this.panel1.Controls.Add(this.textDateNaissance);
            this.panel1.Controls.Add(this.textNom);
            this.panel1.Controls.Add(this.textPrenom);
            this.panel1.Controls.Add(this.textStatut);
            this.panel1.Controls.Add(this.textType);
            this.panel1.Controls.Add(this.textRace);
            this.panel1.Controls.Add(this.textPere);
            this.panel1.Controls.Add(this.textMere);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Pere);
            this.panel1.Controls.Add(this.dateNaissance);
            this.panel1.Controls.Add(this.Prenom);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.btnAjouter);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 410);
            this.panel1.TabIndex = 0;
            // 
            // lesClients
            // 
            this.lesClients.Enabled = false;
            this.lesClients.FormattingEnabled = true;
            this.lesClients.Location = new System.Drawing.Point(487, 334);
            this.lesClients.Name = "lesClients";
            this.lesClients.Size = new System.Drawing.Size(121, 23);
            this.lesClients.TabIndex = 24;
            // 
            // proprietaire
            // 
            this.proprietaire.AutoSize = true;
            this.proprietaire.Location = new System.Drawing.Point(405, 342);
            this.proprietaire.Name = "proprietaire";
            this.proprietaire.Size = new System.Drawing.Size(68, 15);
            this.proprietaire.TabIndex = 23;
            this.proprietaire.Text = "Propriétaire";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kg";
            // 
            // textPoids
            // 
            this.textPoids.DecimalPlaces = 3;
            this.textPoids.Location = new System.Drawing.Point(120, 194);
            this.textPoids.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.textPoids.Name = "textPoids";
            this.textPoids.Size = new System.Drawing.Size(120, 23);
            this.textPoids.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Sexe";
            // 
            // sexeBox
            // 
            this.sexeBox.FormattingEnabled = true;
            this.sexeBox.Location = new System.Drawing.Point(487, 77);
            this.sexeBox.Name = "sexeBox";
            this.sexeBox.Size = new System.Drawing.Size(121, 23);
            this.sexeBox.TabIndex = 20;
            // 
            // textDateNaissance
            // 
            this.textDateNaissance.Location = new System.Drawing.Point(156, 71);
            this.textDateNaissance.Name = "textDateNaissance";
            this.textDateNaissance.Size = new System.Drawing.Size(200, 23);
            this.textDateNaissance.TabIndex = 19;
            // 
            // textNom
            // 
            this.textNom.Location = new System.Drawing.Point(104, 26);
            this.textNom.Name = "textNom";
            this.textNom.Size = new System.Drawing.Size(121, 23);
            this.textNom.TabIndex = 18;
            this.textNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNom_KeyPress);
            // 
            // textPrenom
            // 
            this.textPrenom.Location = new System.Drawing.Point(487, 23);
            this.textPrenom.Name = "textPrenom";
            this.textPrenom.Size = new System.Drawing.Size(121, 23);
            this.textPrenom.TabIndex = 17;
            this.textPrenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPrenom_KeyPress);
            // 
            // textStatut
            // 
            this.textStatut.FormattingEnabled = true;
            this.textStatut.Location = new System.Drawing.Point(104, 342);
            this.textStatut.Name = "textStatut";
            this.textStatut.Size = new System.Drawing.Size(121, 23);
            this.textStatut.TabIndex = 15;
            this.textStatut.SelectedIndexChanged += new System.EventHandler(this.textStatut_SelectedIndexChanged);
            // 
            // textType
            // 
            this.textType.FormattingEnabled = true;
            this.textType.Location = new System.Drawing.Point(487, 252);
            this.textType.Name = "textType";
            this.textType.Size = new System.Drawing.Size(121, 23);
            this.textType.TabIndex = 14;
            // 
            // textRace
            // 
            this.textRace.FormattingEnabled = true;
            this.textRace.Location = new System.Drawing.Point(104, 260);
            this.textRace.Name = "textRace";
            this.textRace.Size = new System.Drawing.Size(121, 23);
            this.textRace.TabIndex = 13;
            // 
            // textPere
            // 
            this.textPere.FormattingEnabled = true;
            this.textPere.Location = new System.Drawing.Point(104, 130);
            this.textPere.Name = "textPere";
            this.textPere.Size = new System.Drawing.Size(121, 23);
            this.textPere.TabIndex = 12;
            // 
            // textMere
            // 
            this.textMere.FormattingEnabled = true;
            this.textMere.Location = new System.Drawing.Point(487, 133);
            this.textMere.Name = "textMere";
            this.textMere.Size = new System.Drawing.Size(121, 23);
            this.textMere.TabIndex = 11;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(698, 384);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 345);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "Statut";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(424, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 15);
            this.label8.TabIndex = 8;
            this.label8.Text = "Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Race";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Poids actuel";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(424, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Mère";
            // 
            // Pere
            // 
            this.Pere.AutoSize = true;
            this.Pere.Location = new System.Drawing.Point(32, 133);
            this.Pere.Name = "Pere";
            this.Pere.Size = new System.Drawing.Size(30, 15);
            this.Pere.TabIndex = 4;
            this.Pere.Text = "Père";
            // 
            // dateNaissance
            // 
            this.dateNaissance.AutoSize = true;
            this.dateNaissance.Location = new System.Drawing.Point(32, 77);
            this.dateNaissance.Name = "dateNaissance";
            this.dateNaissance.Size = new System.Drawing.Size(103, 15);
            this.dateNaissance.TabIndex = 3;
            this.dateNaissance.Text = "Date de Naissance";
            // 
            // Prenom
            // 
            this.Prenom.AutoSize = true;
            this.Prenom.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Prenom.Location = new System.Drawing.Point(424, 29);
            this.Prenom.Name = "Prenom";
            this.Prenom.Size = new System.Drawing.Size(49, 15);
            this.Prenom.TabIndex = 2;
            this.Prenom.Text = "Prenom";
            // 
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Location = new System.Drawing.Point(32, 26);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(34, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Nom";
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(310, 384);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(75, 23);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // FrmAjouterAnimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjouterAnimal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AjouterAnimal";
            this.Load += new System.EventHandler(this.FrmAjouterAnimal_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textPoids)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private DateTimePicker textDateNaissance;
        private TextBox textNom;
        private TextBox textPrenom;
        private ComboBox textStatut;
        private ComboBox textType;
        private ComboBox textRace;
        private ComboBox textPere;
        private ComboBox textMere;
        private Button btnQuitter;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label Pere;
        private Label dateNaissance;
        private Label Prenom;
        private Label Nom;
        private Button btnAjouter;
        private Label label1;
        private ComboBox sexeBox;
        private NumericUpDown textPoids;
        private Label label2;
        private ComboBox lesClients;
        private Label proprietaire;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}