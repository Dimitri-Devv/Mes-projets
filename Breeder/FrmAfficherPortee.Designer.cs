namespace Breeder
{
    partial class FrmAfficherPortee
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPortee = new System.Windows.Forms.Label();
            this.dgvPortee = new System.Windows.Forms.DataGridView();
            this.txtPortees = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.dgvPortees = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.boxLibelle = new System.Windows.Forms.TextBox();
            this.txtLibelle = new System.Windows.Forms.Label();
            this.btnModifier = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.Label();
            this.boxDate = new System.Windows.Forms.DateTimePicker();
            this.txtMere = new System.Windows.Forms.Label();
            this.boxMere = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortees)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(149, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 54);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(355, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Portée";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtPortee);
            this.panel3.Controls.Add(this.dgvPortee);
            this.panel3.Controls.Add(this.txtPortees);
            this.panel3.Controls.Add(this.btnQuitter);
            this.panel3.Controls.Add(this.dgvPortees);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(12, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1076, 576);
            this.panel3.TabIndex = 5;
            // 
            // txtPortee
            // 
            this.txtPortee.AutoSize = true;
            this.txtPortee.Location = new System.Drawing.Point(730, 171);
            this.txtPortee.Name = "txtPortee";
            this.txtPortee.Size = new System.Drawing.Size(41, 15);
            this.txtPortee.TabIndex = 19;
            this.txtPortee.Text = "Portée";
            // 
            // dgvPortee
            // 
            this.dgvPortee.AllowUserToAddRows = false;
            this.dgvPortee.AllowUserToDeleteRows = false;
            this.dgvPortee.AllowUserToResizeColumns = false;
            this.dgvPortee.AllowUserToResizeRows = false;
            this.dgvPortee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPortee.Location = new System.Drawing.Point(730, 189);
            this.dgvPortee.Name = "dgvPortee";
            this.dgvPortee.ReadOnly = true;
            this.dgvPortee.RowTemplate.Height = 25;
            this.dgvPortee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPortee.Size = new System.Drawing.Size(333, 353);
            this.dgvPortee.TabIndex = 18;
            this.dgvPortee.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPortee_CellClick);
            // 
            // txtPortees
            // 
            this.txtPortees.AutoSize = true;
            this.txtPortees.Location = new System.Drawing.Point(13, 171);
            this.txtPortees.Name = "txtPortees";
            this.txtPortees.Size = new System.Drawing.Size(94, 15);
            this.txtPortees.TabIndex = 17;
            this.txtPortees.Text = "Liste des portées";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(988, 550);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 3;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // dgvPortees
            // 
            this.dgvPortees.AllowUserToAddRows = false;
            this.dgvPortees.AllowUserToDeleteRows = false;
            this.dgvPortees.AllowUserToResizeColumns = false;
            this.dgvPortees.AllowUserToResizeRows = false;
            this.dgvPortees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPortees.Location = new System.Drawing.Point(13, 189);
            this.dgvPortees.Name = "dgvPortees";
            this.dgvPortees.ReadOnly = true;
            this.dgvPortees.RowTemplate.Height = 25;
            this.dgvPortees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPortees.Size = new System.Drawing.Size(711, 353);
            this.dgvPortees.TabIndex = 2;
            this.dgvPortees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPortees_CellClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.boxLibelle);
            this.panel4.Controls.Add(this.txtLibelle);
            this.panel4.Controls.Add(this.btnModifier);
            this.panel4.Controls.Add(this.txtDate);
            this.panel4.Controls.Add(this.boxDate);
            this.panel4.Controls.Add(this.txtMere);
            this.panel4.Controls.Add(this.boxMere);
            this.panel4.Location = new System.Drawing.Point(13, 19);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1050, 118);
            this.panel4.TabIndex = 0;
            // 
            // boxLibelle
            // 
            this.boxLibelle.Location = new System.Drawing.Point(611, 82);
            this.boxLibelle.Name = "boxLibelle";
            this.boxLibelle.Size = new System.Drawing.Size(100, 23);
            this.boxLibelle.TabIndex = 16;
            // 
            // txtLibelle
            // 
            this.txtLibelle.AutoSize = true;
            this.txtLibelle.Location = new System.Drawing.Point(611, 63);
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(41, 15);
            this.txtLibelle.TabIndex = 15;
            this.txtLibelle.Text = "Libéllé";
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(855, 81);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(166, 23);
            this.btnModifier.TabIndex = 6;
            this.btnModifier.Text = "Enregistrer les modifications";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // txtDate
            // 
            this.txtDate.AutoSize = true;
            this.txtDate.Location = new System.Drawing.Point(316, 63);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(31, 15);
            this.txtDate.TabIndex = 5;
            this.txtDate.Text = "Date";
            // 
            // boxDate
            // 
            this.boxDate.Location = new System.Drawing.Point(316, 81);
            this.boxDate.Name = "boxDate";
            this.boxDate.Size = new System.Drawing.Size(200, 23);
            this.boxDate.TabIndex = 4;
            // 
            // txtMere
            // 
            this.txtMere.AutoSize = true;
            this.txtMere.Location = new System.Drawing.Point(93, 63);
            this.txtMere.Name = "txtMere";
            this.txtMere.Size = new System.Drawing.Size(34, 15);
            this.txtMere.TabIndex = 1;
            this.txtMere.Text = "Mère";
            // 
            // boxMere
            // 
            this.boxMere.FormattingEnabled = true;
            this.boxMere.Location = new System.Drawing.Point(93, 81);
            this.boxMere.Name = "boxMere";
            this.boxMere.Size = new System.Drawing.Size(121, 23);
            this.boxMere.TabIndex = 0;
            // 
            // FrmAfficherPortee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAfficherPortee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAfficherPortée";
            this.Load += new System.EventHandler(this.FrmAfficherPortee_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortees)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel3;
        private DataGridView dgvPortees;
        private Panel panel4;
        private Label txtMere;
        private ComboBox boxMere;
        private Label txtDate;
        private DateTimePicker boxDate;
        private Button btnModifier;
        private Button btnQuitter;
        private TextBox boxLibelle;
        private Label txtLibelle;
        private Label txtPortees;
        private Label txtPortee;
        private DataGridView dgvPortee;
    }
}