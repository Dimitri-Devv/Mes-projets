namespace Breeder
{
    partial class FrmAfficherCommande
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
            this.btnQuitter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lesAnimauxConcernés = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.leLibelle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leFournisseur = new System.Windows.Forms.Label();
            this.fref = new System.Windows.Forms.Label();
            this.laDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.leTotal = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxConcernés)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lesAnimauxConcernés);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.leLibelle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.leFournisseur);
            this.panel1.Controls.Add(this.fref);
            this.panel1.Controls.Add(this.laDate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.leTotal);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1102, 663);
            this.panel1.TabIndex = 0;
            // 
            // btnQuitter
            // 
            this.btnQuitter.BackColor = System.Drawing.Color.Gray;
            this.btnQuitter.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnQuitter.ForeColor = System.Drawing.Color.White;
            this.btnQuitter.Location = new System.Drawing.Point(934, 618);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(165, 36);
            this.btnQuitter.TabIndex = 13;
            this.btnQuitter.Text = "QUITTER";
            this.btnQuitter.UseVisualStyleBackColor = false;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(486, 618);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "€";
            // 
            // lesAnimauxConcernés
            // 
            this.lesAnimauxConcernés.BackgroundColor = System.Drawing.Color.DimGray;
            this.lesAnimauxConcernés.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.lesAnimauxConcernés.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.lesAnimauxConcernés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesAnimauxConcernés.Location = new System.Drawing.Point(332, 120);
            this.lesAnimauxConcernés.Name = "lesAnimauxConcernés";
            this.lesAnimauxConcernés.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.lesAnimauxConcernés.RowTemplate.Height = 25;
            this.lesAnimauxConcernés.Size = new System.Drawing.Size(738, 471);
            this.lesAnimauxConcernés.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 77);
            this.panel2.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Fax", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(457, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(238, 43);
            this.label9.TabIndex = 0;
            this.label9.Text = "Commande";
            // 
            // leLibelle
            // 
            this.leLibelle.AutoSize = true;
            this.leLibelle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.leLibelle.Location = new System.Drawing.Point(203, 121);
            this.leLibelle.Name = "leLibelle";
            this.leLibelle.Size = new System.Drawing.Size(65, 20);
            this.leLibelle.TabIndex = 8;
            this.leLibelle.Text = "leLibelle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(49, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Libelle : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(49, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fournisseur : ";
            // 
            // leFournisseur
            // 
            this.leFournisseur.AutoSize = true;
            this.leFournisseur.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.leFournisseur.Location = new System.Drawing.Point(203, 197);
            this.leFournisseur.Name = "leFournisseur";
            this.leFournisseur.Size = new System.Drawing.Size(101, 20);
            this.leFournisseur.TabIndex = 3;
            this.leFournisseur.Text = "leFournisseur";
            // 
            // fref
            // 
            this.fref.AutoSize = true;
            this.fref.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fref.Location = new System.Drawing.Point(49, 279);
            this.fref.Name = "fref";
            this.fref.Size = new System.Drawing.Size(75, 21);
            this.fref.TabIndex = 4;
            this.fref.Text = "La date : ";
            // 
            // laDate
            // 
            this.laDate.AutoSize = true;
            this.laDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.laDate.Location = new System.Drawing.Point(203, 280);
            this.laDate.Name = "laDate";
            this.laDate.Size = new System.Drawing.Size(53, 20);
            this.laDate.TabIndex = 5;
            this.laDate.Text = "laDate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(332, 618);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Coût total : ";
            // 
            // leTotal
            // 
            this.leTotal.AutoSize = true;
            this.leTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.leTotal.Location = new System.Drawing.Point(426, 618);
            this.leTotal.Name = "leTotal";
            this.leTotal.Size = new System.Drawing.Size(54, 20);
            this.leTotal.TabIndex = 7;
            this.leTotal.Text = "leTotal";
            // 
            // FrmAfficherCommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1126, 702);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAfficherCommande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAfficherCommande";
            this.Load += new System.EventHandler(this.FrmAfficherCommande_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxConcernés)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label leLibelle;
        private Label label1;
        private Label label2;
        private Label leFournisseur;
        private Label fref;
        private Label laDate;
        private Label label6;
        private Label leTotal;
        private DataGridView lesAnimauxConcernés;
        private Panel panel2;
        private Label label9;
        private Label label3;
        private Button btnQuitter;
    }
}