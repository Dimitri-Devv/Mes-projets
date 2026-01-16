namespace Breeder
{
    partial class FrmModifierVaccin
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
            this.boxLibelle = new System.Windows.Forms.TextBox();
            this.lesVaccins = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Nom = new System.Windows.Forms.Label();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.boxLibelle);
            this.panel1.Controls.Add(this.lesVaccins);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.Modifier);
            this.panel1.Location = new System.Drawing.Point(12, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 355);
            this.panel1.TabIndex = 4;
            // 
            // boxLibelle
            // 
            this.boxLibelle.Location = new System.Drawing.Point(116, 129);
            this.boxLibelle.Name = "boxLibelle";
            this.boxLibelle.Size = new System.Drawing.Size(121, 23);
            this.boxLibelle.TabIndex = 21;
            this.boxLibelle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxLibelle_KeyPress);
            // 
            // lesVaccins
            // 
            this.lesVaccins.FormattingEnabled = true;
            this.lesVaccins.Location = new System.Drawing.Point(116, 45);
            this.lesVaccins.Name = "lesVaccins";
            this.lesVaccins.Size = new System.Drawing.Size(121, 23);
            this.lesVaccins.TabIndex = 20;
            this.lesVaccins.SelectedIndexChanged += new System.EventHandler(this.lesVaccins_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Vaccin";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(701, 327);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 10;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Location = new System.Drawing.Point(56, 129);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(41, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Libelle";
            // 
            // Modifier
            // 
            this.Modifier.Location = new System.Drawing.Point(162, 197);
            this.Modifier.Name = "Modifier";
            this.Modifier.Size = new System.Drawing.Size(75, 23);
            this.Modifier.TabIndex = 0;
            this.Modifier.Text = "Modifier";
            this.Modifier.UseVisualStyleBackColor = true;
            this.Modifier.Click += new System.EventHandler(this.Modifier_Click);
            // 
            // FrmModifierVaccin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierVaccin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierVaccin";
            this.Load += new System.EventHandler(this.FrmModifierVaccin_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ComboBox lesVaccins;
        private Label label1;
        private Button btnQuitter;
        private Label Nom;
        private Button Modifier;
        private TextBox boxLibelle;
    }
}