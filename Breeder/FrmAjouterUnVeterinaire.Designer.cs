namespace Breeder
{
    partial class FrmAjouterUnVeterinaire
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textTel = new System.Windows.Forms.TextBox();
            this.textAdresse = new System.Windows.Forms.TextBox();
            this.textNom = new System.Windows.Forms.TextBox();
            this.textMail = new System.Windows.Forms.TextBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Prenom = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textTel);
            this.panel1.Controls.Add(this.textAdresse);
            this.panel1.Controls.Add(this.textNom);
            this.panel1.Controls.Add(this.textMail);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Prenom);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.btnAjouter);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 410);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label2.Location = new System.Drawing.Point(32, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Tel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label1.Location = new System.Drawing.Point(32, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Adresse";
            // 
            // textTel
            // 
            this.textTel.Location = new System.Drawing.Point(104, 290);
            this.textTel.Name = "textTel";
            this.textTel.Size = new System.Drawing.Size(121, 23);
            this.textTel.TabIndex = 20;
            // 
            // textAdresse
            // 
            this.textAdresse.Location = new System.Drawing.Point(104, 197);
            this.textAdresse.Name = "textAdresse";
            this.textAdresse.Size = new System.Drawing.Size(121, 23);
            this.textAdresse.TabIndex = 19;
            // 
            // textNom
            // 
            this.textNom.Location = new System.Drawing.Point(104, 26);
            this.textNom.Name = "textNom";
            this.textNom.Size = new System.Drawing.Size(121, 23);
            this.textNom.TabIndex = 18;
            this.textNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textNom_KeyPress);
            // 
            // textMail
            // 
            this.textMail.Location = new System.Drawing.Point(104, 110);
            this.textMail.Name = "textMail";
            this.textMail.Size = new System.Drawing.Size(121, 23);
            this.textMail.TabIndex = 17;
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
            // Prenom
            // 
            this.Prenom.AutoSize = true;
            this.Prenom.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Prenom.Location = new System.Drawing.Point(32, 113);
            this.Prenom.Name = "Prenom";
            this.Prenom.Size = new System.Drawing.Size(30, 15);
            this.Prenom.TabIndex = 2;
            this.Prenom.Text = "Mail";
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
            this.btnAjouter.Location = new System.Drawing.Point(311, 359);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(75, 23);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // FrmAjouterUnVeterinaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjouterUnVeterinaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAjouterUnVeterinaire";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label label2;
        private Label label1;
        private TextBox textTel;
        private TextBox textAdresse;
        private TextBox textNom;
        private TextBox textMail;
        private Button btnQuitter;
        private Label Prenom;
        private Label Nom;
        private Button btnAjouter;
    }
}