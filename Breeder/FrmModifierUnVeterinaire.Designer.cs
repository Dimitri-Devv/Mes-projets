namespace Breeder
{
    partial class FrmModifierUnVeterinaire
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
            this.mail = new System.Windows.Forms.Label();
            this.boxMail = new System.Windows.Forms.TextBox();
            this.tel = new System.Windows.Forms.Label();
            this.adresse = new System.Windows.Forms.Label();
            this.boxNom = new System.Windows.Forms.TextBox();
            this.boxAdresse = new System.Windows.Forms.TextBox();
            this.boxTel = new System.Windows.Forms.TextBox();
            this.lesVetos = new System.Windows.Forms.ComboBox();
            this.leVeto = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Nom = new System.Windows.Forms.Label();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mail);
            this.panel1.Controls.Add(this.boxMail);
            this.panel1.Controls.Add(this.tel);
            this.panel1.Controls.Add(this.adresse);
            this.panel1.Controls.Add(this.boxNom);
            this.panel1.Controls.Add(this.boxAdresse);
            this.panel1.Controls.Add(this.boxTel);
            this.panel1.Controls.Add(this.lesVetos);
            this.panel1.Controls.Add(this.leVeto);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.Modifier);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 395);
            this.panel1.TabIndex = 5;
            // 
            // mail
            // 
            this.mail.AutoSize = true;
            this.mail.Location = new System.Drawing.Point(473, 129);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(30, 15);
            this.mail.TabIndex = 30;
            this.mail.Text = "Mail";
            // 
            // boxMail
            // 
            this.boxMail.Location = new System.Drawing.Point(555, 126);
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
            // boxNom
            // 
            this.boxNom.Location = new System.Drawing.Point(116, 129);
            this.boxNom.Name = "boxNom";
            this.boxNom.Size = new System.Drawing.Size(121, 23);
            this.boxNom.TabIndex = 26;
            this.boxNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxNom_KeyPress);
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
            // lesVetos
            // 
            this.lesVetos.FormattingEnabled = true;
            this.lesVetos.Location = new System.Drawing.Point(124, 42);
            this.lesVetos.Name = "lesVetos";
            this.lesVetos.Size = new System.Drawing.Size(121, 23);
            this.lesVetos.TabIndex = 20;
            this.lesVetos.SelectedIndexChanged += new System.EventHandler(this.lesVetos_SelectedIndexChanged);
            // 
            // leVeto
            // 
            this.leVeto.AutoSize = true;
            this.leVeto.Location = new System.Drawing.Point(56, 45);
            this.leVeto.Name = "leVeto";
            this.leVeto.Size = new System.Drawing.Size(62, 15);
            this.leVeto.TabIndex = 19;
            this.leVeto.Text = "Vétérinaire";
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
            // FrmModifierUnVeterinaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierUnVeterinaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierUnVeterinaire";
            this.Load += new System.EventHandler(this.FrmModifierUnVeterinaire_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label mail;
        private TextBox boxMail;
        private Label tel;
        private Label adresse;
        private TextBox boxNom;
        private TextBox boxAdresse;
        private TextBox boxTel;
        private ComboBox lesVetos;
        private Label leVeto;
        private Button btnQuitter;
        private Label Nom;
        private Button Modifier;
    }
}