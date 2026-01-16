namespace Breeder
{
    partial class FrmAjouterClient
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
            this.Niveau = new System.Windows.Forms.Label();
            this.boxTel = new System.Windows.Forms.TextBox();
            this.boxMail = new System.Windows.Forms.TextBox();
            this.boxAdresse = new System.Windows.Forms.TextBox();
            this.textNom = new System.Windows.Forms.TextBox();
            this.textPrenom = new System.Windows.Forms.TextBox();
            this.textNiveau = new System.Windows.Forms.ComboBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.tel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Pere = new System.Windows.Forms.Label();
            this.Prenom = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Niveau);
            this.panel1.Controls.Add(this.boxTel);
            this.panel1.Controls.Add(this.boxMail);
            this.panel1.Controls.Add(this.boxAdresse);
            this.panel1.Controls.Add(this.textNom);
            this.panel1.Controls.Add(this.textPrenom);
            this.panel1.Controls.Add(this.textNiveau);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.tel);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Pere);
            this.panel1.Controls.Add(this.Prenom);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.btnAjouter);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 411);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Niveau
            // 
            this.Niveau.AutoSize = true;
            this.Niveau.Location = new System.Drawing.Point(32, 281);
            this.Niveau.Name = "Niveau";
            this.Niveau.Size = new System.Drawing.Size(44, 15);
            this.Niveau.TabIndex = 26;
            this.Niveau.Text = "Niveau";
            // 
            // boxTel
            // 
            this.boxTel.Location = new System.Drawing.Point(487, 113);
            this.boxTel.Name = "boxTel";
            this.boxTel.Size = new System.Drawing.Size(121, 23);
            this.boxTel.TabIndex = 25;
            // 
            // boxMail
            // 
            this.boxMail.Location = new System.Drawing.Point(104, 187);
            this.boxMail.Name = "boxMail";
            this.boxMail.Size = new System.Drawing.Size(188, 23);
            this.boxMail.TabIndex = 24;
            // 
            // boxAdresse
            // 
            this.boxAdresse.Location = new System.Drawing.Point(104, 113);
            this.boxAdresse.Name = "boxAdresse";
            this.boxAdresse.Size = new System.Drawing.Size(188, 23);
            this.boxAdresse.TabIndex = 23;
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
            // textNiveau
            // 
            this.textNiveau.FormattingEnabled = true;
            this.textNiveau.Location = new System.Drawing.Point(104, 278);
            this.textNiveau.Name = "textNiveau";
            this.textNiveau.Size = new System.Drawing.Size(121, 23);
            this.textNiveau.TabIndex = 15;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(698, 385);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // tel
            // 
            this.tel.AutoSize = true;
            this.tel.Location = new System.Drawing.Point(424, 116);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(21, 15);
            this.tel.TabIndex = 9;
            this.tel.Text = "Tél";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Mail";
            // 
            // Pere
            // 
            this.Pere.AutoSize = true;
            this.Pere.Location = new System.Drawing.Point(32, 116);
            this.Pere.Name = "Pere";
            this.Pere.Size = new System.Drawing.Size(48, 15);
            this.Pere.TabIndex = 4;
            this.Pere.Text = "Adresse";
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
            this.btnAjouter.Location = new System.Drawing.Point(336, 351);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(75, 23);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // FrmAjouterClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjouterClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAjouterClient";
            this.Load += new System.EventHandler(this.FrmAjouterClient_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private TextBox textNom;
        private TextBox textPrenom;
        private ComboBox textNiveau;
        private Button btnQuitter;
        private Label tel;
        private Label label7;
        private Label Pere;
        private Label Prenom;
        private Label Nom;
        private Button btnAjouter;
        private TextBox boxMail;
        private TextBox boxAdresse;
        private Label Niveau;
        private TextBox boxTel;
    }
}