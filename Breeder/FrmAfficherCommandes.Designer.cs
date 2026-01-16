namespace Breeder
{
    partial class FrmAfficherCommandes
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
            this.btnToPdf = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lesCommandes = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesCommandes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnToPdf);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lesCommandes);
            this.panel1.Location = new System.Drawing.Point(3, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 444);
            this.panel1.TabIndex = 4;
            // 
            // btnToPdf
            // 
            this.btnToPdf.Location = new System.Drawing.Point(19, 397);
            this.btnToPdf.Name = "btnToPdf";
            this.btnToPdf.Size = new System.Drawing.Size(117, 23);
            this.btnToPdf.TabIndex = 4;
            this.btnToPdf.Text = "Exporter en PDF";
            this.btnToPdf.UseVisualStyleBackColor = true;
            this.btnToPdf.Click += new System.EventHandler(this.btnToPdf_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(715, 418);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 3;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(332, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Les commandes";
            // 
            // lesCommandes
            // 
            this.lesCommandes.AllowUserToAddRows = false;
            this.lesCommandes.AllowUserToDeleteRows = false;
            this.lesCommandes.AllowUserToResizeColumns = false;
            this.lesCommandes.AllowUserToResizeRows = false;
            this.lesCommandes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesCommandes.Location = new System.Drawing.Point(3, 36);
            this.lesCommandes.Name = "lesCommandes";
            this.lesCommandes.ReadOnly = true;
            this.lesCommandes.RowTemplate.Height = 25;
            this.lesCommandes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesCommandes.Size = new System.Drawing.Size(757, 343);
            this.lesCommandes.TabIndex = 1;
            this.lesCommandes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // FrmAfficherCommandes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 495);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAfficherCommandes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAfficherCommandes";
            this.Load += new System.EventHandler(this.FrmAfficherCommandes_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesCommandes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Button btnQuitter;
        private Label label1;
        private DataGridView lesCommandes;
        private Button btnToPdf;
    }
}