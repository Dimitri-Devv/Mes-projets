namespace Breeder
{
    partial class FrmModifierClient
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
            this.niveau = new System.Windows.Forms.Label();
            this.mail = new System.Windows.Forms.Label();
            this.boxMail = new System.Windows.Forms.TextBox();
            this.tel = new System.Windows.Forms.Label();
            this.adresse = new System.Windows.Forms.Label();
            this.leNom = new System.Windows.Forms.TextBox();
            this.Prenom = new System.Windows.Forms.TextBox();
            this.boxAdresse = new System.Windows.Forms.TextBox();
            this.boxTel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lesNiveaux = new System.Windows.Forms.ComboBox();
            this.lesClients = new System.Windows.Forms.ComboBox();
            this.leClient = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Nom = new System.Windows.Forms.Label();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.niveau);
            this.panel1.Controls.Add(this.mail);
            this.panel1.Controls.Add(this.boxMail);
            this.panel1.Controls.Add(this.tel);
            this.panel1.Controls.Add(this.adresse);
            this.panel1.Controls.Add(this.leNom);
            this.panel1.Controls.Add(this.Prenom);
            this.panel1.Controls.Add(this.boxAdresse);
            this.panel1.Controls.Add(this.boxTel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lesNiveaux);
            this.panel1.Controls.Add(this.lesClients);
            this.panel1.Controls.Add(this.leClient);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.Modifier);
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 395);
            this.panel1.TabIndex = 4;
            // 
            // niveau
            // 
            this.niveau.AutoSize = true;
            this.niveau.Location = new System.Drawing.Point(482, 305);
            this.niveau.Name = "niveau";
            this.niveau.Size = new System.Drawing.Size(44, 15);
            this.niveau.TabIndex = 31;
            this.niveau.Text = "Niveau";
            // 
            // mail
            // 
            this.mail.AutoSize = true;
            this.mail.Location = new System.Drawing.Point(56, 310);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(30, 15);
            this.mail.TabIndex = 30;
            this.mail.Text = "Mail";
            // 
            // boxMail
            // 
            this.boxMail.Location = new System.Drawing.Point(116, 302);
            this.boxMail.Name = "boxMail";
            this.boxMail.Size = new System.Drawing.Size(168, 23);
            this.boxMail.TabIndex = 29;
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
            // leNom
            // 
            this.leNom.Location = new System.Drawing.Point(116, 129);
            this.leNom.Name = "leNom";
            this.leNom.Size = new System.Drawing.Size(121, 23);
            this.leNom.TabIndex = 26;
            this.leNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leNom_KeyPress);
            // 
            // Prenom
            // 
            this.Prenom.Location = new System.Drawing.Point(564, 126);
            this.Prenom.Name = "Prenom";
            this.Prenom.Size = new System.Drawing.Size(121, 23);
            this.Prenom.TabIndex = 25;
            this.Prenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Prenom_KeyPress);
            // 
            // boxAdresse
            // 
            this.boxAdresse.Location = new System.Drawing.Point(116, 204);
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
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Prenom";
            // 
            // lesNiveaux
            // 
            this.lesNiveaux.FormattingEnabled = true;
            this.lesNiveaux.Location = new System.Drawing.Point(564, 302);
            this.lesNiveaux.Name = "lesNiveaux";
            this.lesNiveaux.Size = new System.Drawing.Size(121, 23);
            this.lesNiveaux.TabIndex = 21;
            // 
            // lesClients
            // 
            this.lesClients.FormattingEnabled = true;
            this.lesClients.Location = new System.Drawing.Point(116, 45);
            this.lesClients.Name = "lesClients";
            this.lesClients.Size = new System.Drawing.Size(121, 23);
            this.lesClients.TabIndex = 20;
            this.lesClients.SelectedIndexChanged += new System.EventHandler(this.lesClients_SelectedIndexChanged);
            // 
            // leClient
            // 
            this.leClient.AutoSize = true;
            this.leClient.Location = new System.Drawing.Point(56, 45);
            this.leClient.Name = "leClient";
            this.leClient.Size = new System.Drawing.Size(38, 15);
            this.leClient.TabIndex = 19;
            this.leClient.Text = "Client";
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
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Location = new System.Drawing.Point(56, 129);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(34, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Nom";
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
            // FrmModifierClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierClient";
            this.Load += new System.EventHandler(this.FrmModifierClient_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ComboBox lesNiveaux;
        private ComboBox lesClients;
        private Label leClient;
        private Button btnQuitter;
        private Label Nom;
        private Button Modifier;
        private Label niveau;
        private Label mail;
        private TextBox boxMail;
        private Label tel;
        private Label adresse;
        private TextBox leNom;
        private TextBox Prenom;
        private TextBox boxAdresse;
        private TextBox boxTel;
        private Label label1;
    }
}