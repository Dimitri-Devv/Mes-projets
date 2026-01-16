namespace Breeder
{
    partial class FrmVaccination
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
            this.btnRetirer = new System.Windows.Forms.Button();
            this.btnToutRetirer = new System.Windows.Forms.Button();
            this.btnToutDeplacer = new System.Windows.Forms.Button();
            this.btnDeplacer = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lesVaccinsAttribués = new System.Windows.Forms.DataGridView();
            this.lesVaccins = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nomPrenom = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesVaccinsAttribués)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesVaccins)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRetirer);
            this.panel1.Controls.Add(this.btnToutRetirer);
            this.panel1.Controls.Add(this.btnToutDeplacer);
            this.panel1.Controls.Add(this.btnDeplacer);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lesVaccinsAttribués);
            this.panel1.Controls.Add(this.lesVaccins);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(951, 557);
            this.panel1.TabIndex = 0;
            // 
            // btnRetirer
            // 
            this.btnRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRetirer.Location = new System.Drawing.Point(438, 454);
            this.btnRetirer.Name = "btnRetirer";
            this.btnRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnRetirer.TabIndex = 39;
            this.btnRetirer.Text = "<";
            this.btnRetirer.UseVisualStyleBackColor = true;
            this.btnRetirer.Click += new System.EventHandler(this.btnRetirer_Click);
            // 
            // btnToutRetirer
            // 
            this.btnToutRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutRetirer.Location = new System.Drawing.Point(438, 405);
            this.btnToutRetirer.Name = "btnToutRetirer";
            this.btnToutRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnToutRetirer.TabIndex = 38;
            this.btnToutRetirer.Text = "<<";
            this.btnToutRetirer.UseVisualStyleBackColor = true;
            this.btnToutRetirer.Click += new System.EventHandler(this.btnToutRetirer_Click);
            // 
            // btnToutDeplacer
            // 
            this.btnToutDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutDeplacer.Location = new System.Drawing.Point(438, 170);
            this.btnToutDeplacer.Name = "btnToutDeplacer";
            this.btnToutDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnToutDeplacer.TabIndex = 37;
            this.btnToutDeplacer.Text = ">>";
            this.btnToutDeplacer.UseVisualStyleBackColor = true;
            this.btnToutDeplacer.Click += new System.EventHandler(this.btnToutDeplacer_Click);
            // 
            // btnDeplacer
            // 
            this.btnDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeplacer.Location = new System.Drawing.Point(438, 121);
            this.btnDeplacer.Name = "btnDeplacer";
            this.btnDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnDeplacer.TabIndex = 36;
            this.btnDeplacer.Text = ">";
            this.btnDeplacer.UseVisualStyleBackColor = true;
            this.btnDeplacer.Click += new System.EventHandler(this.btnDeplacer_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(873, 531);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 7;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(701, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Vaccins attribués";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Vaccins";
            // 
            // lesVaccinsAttribués
            // 
            this.lesVaccinsAttribués.AllowUserToAddRows = false;
            this.lesVaccinsAttribués.AllowUserToDeleteRows = false;
            this.lesVaccinsAttribués.AllowUserToResizeColumns = false;
            this.lesVaccinsAttribués.AllowUserToResizeRows = false;
            this.lesVaccinsAttribués.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesVaccinsAttribués.Location = new System.Drawing.Point(557, 121);
            this.lesVaccinsAttribués.Name = "lesVaccinsAttribués";
            this.lesVaccinsAttribués.ReadOnly = true;
            this.lesVaccinsAttribués.RowTemplate.Height = 25;
            this.lesVaccinsAttribués.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesVaccinsAttribués.Size = new System.Drawing.Size(366, 376);
            this.lesVaccinsAttribués.TabIndex = 4;
            // 
            // lesVaccins
            // 
            this.lesVaccins.AllowUserToAddRows = false;
            this.lesVaccins.AllowUserToDeleteRows = false;
            this.lesVaccins.AllowUserToResizeColumns = false;
            this.lesVaccins.AllowUserToResizeRows = false;
            this.lesVaccins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesVaccins.Location = new System.Drawing.Point(31, 121);
            this.lesVaccins.Name = "lesVaccins";
            this.lesVaccins.ReadOnly = true;
            this.lesVaccins.RowTemplate.Height = 25;
            this.lesVaccins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesVaccins.Size = new System.Drawing.Size(366, 376);
            this.lesVaccins.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nomPrenom);
            this.panel2.Location = new System.Drawing.Point(256, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 63);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(93, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vaccination de : ";
            // 
            // nomPrenom
            // 
            this.nomPrenom.AutoSize = true;
            this.nomPrenom.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nomPrenom.Location = new System.Drawing.Point(260, 25);
            this.nomPrenom.Name = "nomPrenom";
            this.nomPrenom.Size = new System.Drawing.Size(63, 25);
            this.nomPrenom.TabIndex = 1;
            this.nomPrenom.Text = "label2";
            // 
            // FrmVaccination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 603);
            this.Controls.Add(this.panel1);
            this.Name = "FrmVaccination";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVaccination";
            this.Load += new System.EventHandler(this.FrmVaccination_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesVaccinsAttribués)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesVaccins)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label label3;
        private Label label2;
        private DataGridView lesVaccinsAttribués;
        private DataGridView lesVaccins;
        private Panel panel2;
        private Label label1;
        private Label nomPrenom;
        private Button btnQuitter;
        private Button btnDeplacer;
        private Button btnToutDeplacer;
        private Button btnToutRetirer;
        private Button btnRetirer;
    }
}