using Stage;

namespace Breeder
{
    partial class FrmAccueil
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.commandesRecentes = new Breeder.FrmDgv();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lesAjoutsRecents = new Breeder.FrmDgv();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.derniersClientsAjoutes = new Breeder.FrmDgv();
            this.label4 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.animauxNonAJour = new Breeder.FrmDgv();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandesRecentes)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAjoutsRecents)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.derniersClientsAjoutes)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.animauxNonAJour)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.commandesRecentes);
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(502, 133);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 314);
            this.panel3.TabIndex = 0;
            // 
            // commandesRecentes
            // 
            this.commandesRecentes.AllowUserToAddRows = false;
            this.commandesRecentes.AllowUserToDeleteRows = false;
            this.commandesRecentes.AllowUserToResizeColumns = false;
            this.commandesRecentes.AllowUserToResizeRows = false;
            this.commandesRecentes.BackgroundColor = System.Drawing.Color.DimGray;
            this.commandesRecentes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.commandesRecentes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.commandesRecentes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.commandesRecentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commandesRecentes.Location = new System.Drawing.Point(19, 3);
            this.commandesRecentes.MaximumSize = new System.Drawing.Size(339, 299);
            this.commandesRecentes.Name = "commandesRecentes";
            this.commandesRecentes.ReadOnly = true;
            this.commandesRecentes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.commandesRecentes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.commandesRecentes.RowTemplate.Height = 25;
            this.commandesRecentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.commandesRecentes.Size = new System.Drawing.Size(339, 299);
            this.commandesRecentes.TabIndex = 2;
            this.commandesRecentes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.commandes_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(95, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Les commandes récentes";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Location = new System.Drawing.Point(1878, 1073);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(185, 51);
            this.panel4.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(621, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 51);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(330, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "ACCUEIL";
            // 
            // lesAjoutsRecents
            // 
            this.lesAjoutsRecents.AllowUserToAddRows = false;
            this.lesAjoutsRecents.AllowUserToDeleteRows = false;
            this.lesAjoutsRecents.AllowUserToResizeColumns = false;
            this.lesAjoutsRecents.AllowUserToResizeRows = false;
            this.lesAjoutsRecents.BackgroundColor = System.Drawing.Color.DimGray;
            this.lesAjoutsRecents.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lesAjoutsRecents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.lesAjoutsRecents.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.lesAjoutsRecents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesAjoutsRecents.Location = new System.Drawing.Point(13, 3);
            this.lesAjoutsRecents.Name = "lesAjoutsRecents";
            this.lesAjoutsRecents.ReadOnly = true;
            this.lesAjoutsRecents.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.lesAjoutsRecents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.lesAjoutsRecents.RowTemplate.Height = 25;
            this.lesAjoutsRecents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesAjoutsRecents.Size = new System.Drawing.Size(332, 299);
            this.lesAjoutsRecents.TabIndex = 1;
            this.lesAjoutsRecents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.animauxRecents_CellClick);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lesAjoutsRecents);
            this.panel5.ForeColor = System.Drawing.Color.Black;
            this.panel5.Location = new System.Drawing.Point(86, 508);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(360, 308);
            this.panel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(64, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Les derniers animaux ajoutés";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.derniersClientsAjoutes);
            this.panel6.ForeColor = System.Drawing.Color.Black;
            this.panel6.Location = new System.Drawing.Point(501, 508);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(372, 308);
            this.panel6.TabIndex = 0;
            // 
            // derniersClientsAjoutes
            // 
            this.derniersClientsAjoutes.AllowUserToAddRows = false;
            this.derniersClientsAjoutes.AllowUserToDeleteRows = false;
            this.derniersClientsAjoutes.AllowUserToResizeColumns = false;
            this.derniersClientsAjoutes.AllowUserToResizeRows = false;
            this.derniersClientsAjoutes.BackgroundColor = System.Drawing.Color.DimGray;
            this.derniersClientsAjoutes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.derniersClientsAjoutes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.derniersClientsAjoutes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.derniersClientsAjoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.derniersClientsAjoutes.Location = new System.Drawing.Point(20, 3);
            this.derniersClientsAjoutes.Name = "derniersClientsAjoutes";
            this.derniersClientsAjoutes.ReadOnly = true;
            this.derniersClientsAjoutes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.derniersClientsAjoutes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.derniersClientsAjoutes.RowTemplate.Height = 25;
            this.derniersClientsAjoutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.derniersClientsAjoutes.Size = new System.Drawing.Size(332, 299);
            this.derniersClientsAjoutes.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(95, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Les derniers clients ajoutés";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.animauxNonAJour);
            this.panel7.ForeColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(86, 133);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(360, 314);
            this.panel7.TabIndex = 4;
            // 
            // animauxNonAJour
            // 
            this.animauxNonAJour.AllowUserToAddRows = false;
            this.animauxNonAJour.AllowUserToDeleteRows = false;
            this.animauxNonAJour.AllowUserToResizeColumns = false;
            this.animauxNonAJour.AllowUserToResizeRows = false;
            this.animauxNonAJour.BackgroundColor = System.Drawing.Color.DimGray;
            this.animauxNonAJour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.animauxNonAJour.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.animauxNonAJour.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.animauxNonAJour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.animauxNonAJour.Location = new System.Drawing.Point(13, 3);
            this.animauxNonAJour.MultiSelect = false;
            this.animauxNonAJour.Name = "animauxNonAJour";
            this.animauxNonAJour.ReadOnly = true;
            this.animauxNonAJour.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.animauxNonAJour.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.animauxNonAJour.RowTemplate.Height = 25;
            this.animauxNonAJour.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.animauxNonAJour.Size = new System.Drawing.Size(332, 299);
            this.animauxNonAJour.TabIndex = 0;
            this.animauxNonAJour.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.animauxNonAJour_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(118, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Animaux non à jour";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(86, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 51);
            this.panel2.TabIndex = 8;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label4);
            this.panel8.ForeColor = System.Drawing.Color.White;
            this.panel8.Location = new System.Drawing.Point(501, 453);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(372, 52);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label5);
            this.panel9.ForeColor = System.Drawing.Color.White;
            this.panel9.Location = new System.Drawing.Point(501, 81);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(372, 49);
            this.panel9.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label2);
            this.panel10.ForeColor = System.Drawing.Color.White;
            this.panel10.Location = new System.Drawing.Point(86, 453);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(360, 52);
            this.panel10.TabIndex = 0;
            // 
            // FrmAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(1609, 755);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "FrmAccueil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAccueil";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.FrmAccueil_Load);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.panel7, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel10, 0);
            this.Controls.SetChildIndex(this.panel9, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel8, 0);
            this.Controls.SetChildIndex(this.panel6, 0);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commandesRecentes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAjoutsRecents)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.derniersClientsAjoutes)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.animauxNonAJour)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private FontDialog fontDialog1;
        private Panel panel3;
        private Panel panel4;
        private Panel panel1;
        private Label label1;
        private Panel panel5;
        private Label label2;
        private Panel panel6;
        private Label label5;
        private Label label4;
        private Panel panel7;
        private Label label6;
        private Panel panel2;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private FrmBtnQuitter btnQuitter;
        private FrmDgv commandesRecentes;
        private FrmDgv lesAjoutsRecents;
        private FrmDgv derniersClientsAjoutes;
        private FrmDgv animauxNonAJour;
    }
}