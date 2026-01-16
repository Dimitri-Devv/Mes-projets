namespace Breeder
{
    partial class FrmAfficherLesAnimaux
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAnimaux = new System.Windows.Forms.DataGridView();
            this.btnQuitter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimaux)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Les animaux";
            // 
            // dgvAnimaux
            // 
            this.dgvAnimaux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimaux.Location = new System.Drawing.Point(12, 57);
            this.dgvAnimaux.Name = "dgvAnimaux";
            this.dgvAnimaux.RowTemplate.Height = 25;
            this.dgvAnimaux.Size = new System.Drawing.Size(776, 359);
            this.dgvAnimaux.ReadOnly = true;
            this.dgvAnimaux.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnimaux.TabIndex = 1;
            this.dgvAnimaux.AllowUserToAddRows = false;
            this.dgvAnimaux.AllowUserToDeleteRows = false;
            this.dgvAnimaux.AllowUserToOrderColumns = false;
            this.dgvAnimaux.AllowUserToResizeColumns = false;
            this.dgvAnimaux.AllowUserToResizeRows = false;
            this.dgvAnimaux.MultiSelect = false;
            this.dgvAnimaux.CellClick += new DataGridViewCellEventHandler(this.lesAnimaux_CellClick);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(703, 422);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 2;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            // 
            // FrmAfficherLesAnimaux
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.dgvAnimaux);
            this.Controls.Add(this.label1);
            this.Name = "FrmAfficherLesAnimaux";
            this.Text = "FrmAfficherLesAnimaux";
            this.Load += new System.EventHandler(this.FrmAfficherLesAnimaux_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimaux)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private DataGridView dgvAnimaux;
        private Button btnQuitter;
    }
}