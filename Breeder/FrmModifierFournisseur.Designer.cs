namespace Breeder
{
    partial class FrmModifierFournisseur
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
            this.tel = new System.Windows.Forms.Label();
            this.adresse = new System.Windows.Forms.Label();
            this.leLibelle = new System.Windows.Forms.TextBox();
            this.mail = new System.Windows.Forms.TextBox();
            this.boxAdresse = new System.Windows.Forms.TextBox();
            this.boxTel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lesFournisseurs = new System.Windows.Forms.ComboBox();
            this.leClient = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.libelle = new System.Windows.Forms.Label();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tel);
            this.panel1.Controls.Add(this.adresse);
            this.panel1.Controls.Add(this.leLibelle);
            this.panel1.Controls.Add(this.mail);
            this.panel1.Controls.Add(this.boxAdresse);
            this.panel1.Controls.Add(this.boxTel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lesFournisseurs);
            this.panel1.Controls.Add(this.leClient);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.libelle);
            this.panel1.Controls.Add(this.Modifier);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 395);
            this.panel1.TabIndex = 5;
            // 
            // tel
            // 
            this.tel.AutoSize = true;
            this.tel.Location = new System.Drawing.Point(482, 217);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(21, 15);
            this.tel.TabIndex = 28;
            this.tel.Text = "Tel";
            // 
            // adresse
            // 
            this.adresse.AutoSize = true;
            this.adresse.Location = new System.Drawing.Point(56, 212);
            this.adresse.Name = "adresse";
            this.adresse.Size = new System.Drawing.Size(48, 15);
            this.adresse.TabIndex = 27;
            this.adresse.Text = "Adresse";
            // 
            // leLibelle
            // 
            this.leLibelle.Location = new System.Drawing.Point(116, 129);
            this.leLibelle.Name = "leLibelle";
            this.leLibelle.Size = new System.Drawing.Size(121, 23);
            this.leLibelle.TabIndex = 26;
            this.leLibelle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leLibelle_KeyPress);
            // 
            // mail
            // 
            this.mail.Location = new System.Drawing.Point(564, 126);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(121, 23);
            this.mail.TabIndex = 25;
            // 
            // boxAdresse
            // 
            this.boxAdresse.Location = new System.Drawing.Point(116, 212);
            this.boxAdresse.Name = "boxAdresse";
            this.boxAdresse.Size = new System.Drawing.Size(168, 23);
            this.boxAdresse.TabIndex = 24;
            // 
            // boxTel
            // 
            this.boxTel.Location = new System.Drawing.Point(564, 209);
            this.boxTel.Name = "boxTel";
            this.boxTel.Size = new System.Drawing.Size(121, 23);
            this.boxTel.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(482, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Mail";
            // 
            // lesFournisseurs
            // 
            this.lesFournisseurs.FormattingEnabled = true;
            this.lesFournisseurs.Location = new System.Drawing.Point(116, 45);
            this.lesFournisseurs.Name = "lesFournisseurs";
            this.lesFournisseurs.Size = new System.Drawing.Size(121, 23);
            this.lesFournisseurs.TabIndex = 20;
            this.lesFournisseurs.SelectedIndexChanged += new System.EventHandler(this.lesFournisseurs_SelectedIndexChanged);
            // 
            // leClient
            // 
            this.leClient.AutoSize = true;
            this.leClient.Location = new System.Drawing.Point(36, 48);
            this.leClient.Name = "leClient";
            this.leClient.Size = new System.Drawing.Size(68, 15);
            this.leClient.TabIndex = 19;
            this.leClient.Text = "Fournisseur";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(698, 368);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // libelle
            // 
            this.libelle.AutoSize = true;
            this.libelle.Location = new System.Drawing.Point(56, 129);
            this.libelle.Name = "libelle";
            this.libelle.Size = new System.Drawing.Size(41, 15);
            this.libelle.TabIndex = 1;
            this.libelle.Text = "Libelle";
            // 
            // Modifier
            // 
            this.Modifier.Location = new System.Drawing.Point(309, 368);
            this.Modifier.Name = "Modifier";
            this.Modifier.Size = new System.Drawing.Size(75, 23);
            this.Modifier.TabIndex = 0;
            this.Modifier.Text = "Modifier";
            this.Modifier.UseVisualStyleBackColor = true;
            this.Modifier.Click += new System.EventHandler(this.Modifier_Click);
            // 
            // FrmModifierFournisseur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierFournisseur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierFournisseur";
            this.Load += new System.EventHandler(this.FrmModifierFournisseur_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label tel;
        private Label adresse;
        private TextBox leLibelle;
        private TextBox mail;
        private TextBox boxAdresse;
        private TextBox boxTel;
        private Label label1;
        private ComboBox lesFournisseurs;
        private Label leClient;
        private Button btnQuitter;
        private Label libelle;
        private Button Modifier;
    }
}