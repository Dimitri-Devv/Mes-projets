namespace Breeder
{
    partial class FrmSupprimerClient
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
            this.Client = new System.Windows.Forms.Label();
            this.lesClients = new System.Windows.Forms.ComboBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Supprimer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Client);
            this.panel1.Controls.Add(this.lesClients);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Supprimer);
            this.panel1.Location = new System.Drawing.Point(1, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 423);
            this.panel1.TabIndex = 2;
            // 
            // Client
            // 
            this.Client.AutoSize = true;
            this.Client.Location = new System.Drawing.Point(90, 90);
            this.Client.Name = "Client";
            this.Client.Size = new System.Drawing.Size(38, 15);
            this.Client.TabIndex = 13;
            this.Client.Text = "Client";
            // 
            // lesClients
            // 
            this.lesClients.FormattingEnabled = true;
            this.lesClients.Location = new System.Drawing.Point(191, 87);
            this.lesClients.Name = "lesClients";
            this.lesClients.Size = new System.Drawing.Size(258, 23);
            this.lesClients.TabIndex = 12;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(722, 397);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 11;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // Supprimer
            // 
            this.Supprimer.Location = new System.Drawing.Point(191, 164);
            this.Supprimer.Name = "Supprimer";
            this.Supprimer.Size = new System.Drawing.Size(137, 23);
            this.Supprimer.TabIndex = 0;
            this.Supprimer.Text = "Supprimer";
            this.Supprimer.UseVisualStyleBackColor = true;
            this.Supprimer.Click += new System.EventHandler(this.Supprimer_Click);
            // 
            // FrmSupprimerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmSupprimerClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSupprimerClient";
            this.Load += new System.EventHandler(this.FrmSupprimerClient_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label Client;
        private ComboBox lesClients;
        private Button btnQuitter;
        private Button Supprimer;
    }
}