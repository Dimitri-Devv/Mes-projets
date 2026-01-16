namespace Breeder
{
    partial class FrmAjouterVaccin
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
            this.textLibelle = new System.Windows.Forms.TextBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.nom = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textLibelle);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.nom);
            this.panel1.Controls.Add(this.btnAjouter);
            this.panel1.Location = new System.Drawing.Point(12, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 356);
            this.panel1.TabIndex = 3;
            // 
            // textLibelle
            // 
            this.textLibelle.Location = new System.Drawing.Point(116, 61);
            this.textLibelle.Name = "textLibelle";
            this.textLibelle.Size = new System.Drawing.Size(121, 23);
            this.textLibelle.TabIndex = 18;
            this.textLibelle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textLibelle_KeyPress);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(701, 330);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // nom
            // 
            this.nom.AutoSize = true;
            this.nom.Location = new System.Drawing.Point(48, 64);
            this.nom.Name = "nom";
            this.nom.Size = new System.Drawing.Size(41, 15);
            this.nom.TabIndex = 1;
            this.nom.Text = "Libelle";
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(162, 147);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(75, 23);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // FrmAjouterVaccin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjouterVaccin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAjouterVaccin";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private TextBox textLibelle;
        private Button btnQuitter;
        private Label nom;
        private Button btnAjouter;
    }
}