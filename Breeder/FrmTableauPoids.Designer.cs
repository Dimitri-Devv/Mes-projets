namespace Breeder
{
    partial class FrmTableauPoids
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
            this.lesPoids = new System.Windows.Forms.DataGridView();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnToPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lesPoids)).BeginInit();
            this.SuspendLayout();
            // 
            // lesPoids
            // 
            this.lesPoids.AllowUserToAddRows = false;
            this.lesPoids.AllowUserToDeleteRows = false;
            this.lesPoids.AllowUserToResizeColumns = false;
            this.lesPoids.AllowUserToResizeRows = false;
            this.lesPoids.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesPoids.Location = new System.Drawing.Point(24, 52);
            this.lesPoids.Name = "lesPoids";
            this.lesPoids.ReadOnly = true;
            this.lesPoids.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.lesPoids.RowTemplate.Height = 25;
            this.lesPoids.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesPoids.Size = new System.Drawing.Size(944, 625);
            this.lesPoids.TabIndex = 0;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(1071, 654);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 2;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // btnToPdf
            // 
            this.btnToPdf.Location = new System.Drawing.Point(1008, 61);
            this.btnToPdf.Name = "btnToPdf";
            this.btnToPdf.Size = new System.Drawing.Size(116, 23);
            this.btnToPdf.TabIndex = 4;
            this.btnToPdf.Text = "Exporter en PDF";
            this.btnToPdf.UseVisualStyleBackColor = true;
            this.btnToPdf.Click += new System.EventHandler(this.btnToPdf_Click);
            // 
            // FrmTableauPoids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 689);
            this.ControlBox = false;
            this.Controls.Add(this.btnToPdf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.lesPoids);
            this.Name = "FrmTableauPoids";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTableauPoids";
            this.Load += new System.EventHandler(this.FrmTableauPoids_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lesPoids)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView lesPoids;
        private Button btnQuitter;
        private Label label1;
        private Button btnToPdf;
    }
}