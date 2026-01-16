namespace Breeder
{
    partial class FrmSupprimerCommande
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
            this.boxCommandes = new System.Windows.Forms.ComboBox();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.txtCommandes = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boxCommandes
            // 
            this.boxCommandes.FormattingEnabled = true;
            this.boxCommandes.Location = new System.Drawing.Point(323, 158);
            this.boxCommandes.Name = "boxCommandes";
            this.boxCommandes.Size = new System.Drawing.Size(121, 23);
            this.boxCommandes.TabIndex = 0;
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(344, 239);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(75, 23);
            this.btnSupprimer.TabIndex = 1;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // txtCommandes
            // 
            this.txtCommandes.AutoSize = true;
            this.txtCommandes.Location = new System.Drawing.Point(187, 161);
            this.txtCommandes.Name = "txtCommandes";
            this.txtCommandes.Size = new System.Drawing.Size(121, 15);
            this.txtCommandes.TabIndex = 2;
            this.txtCommandes.Text = "Liste des commandes";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(713, 415);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 11;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // FrmSupprimerCommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.txtCommandes);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.boxCommandes);
            this.Name = "FrmSupprimerCommande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSupprimerCommande";
            this.Load += new System.EventHandler(this.FrmSupprimerCommande_Load);
            this.Controls.SetChildIndex(this.boxCommandes, 0);
            this.Controls.SetChildIndex(this.btnSupprimer, 0);
            this.Controls.SetChildIndex(this.txtCommandes, 0);
            this.Controls.SetChildIndex(this.btnQuitter, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox boxCommandes;
        private Button btnSupprimer;
        private Label txtCommandes;
        private Button btnQuitter;
    }
}