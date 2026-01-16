namespace Breeder
{
    partial class FrmModifierType
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
            this.lesTypes = new System.Windows.Forms.ComboBox();
            this.lesAnimaux = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Nom = new System.Windows.Forms.Label();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lesTypes);
            this.panel1.Controls.Add(this.lesAnimaux);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.Modifier);
            this.panel1.Location = new System.Drawing.Point(12, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 354);
            this.panel1.TabIndex = 2;
            // 
            // lesTypes
            // 
            this.lesTypes.FormattingEnabled = true;
            this.lesTypes.Location = new System.Drawing.Point(116, 129);
            this.lesTypes.Name = "lesTypes";
            this.lesTypes.Size = new System.Drawing.Size(121, 23);
            this.lesTypes.TabIndex = 21;
            // 
            // lesAnimaux
            // 
            this.lesAnimaux.FormattingEnabled = true;
            this.lesAnimaux.Location = new System.Drawing.Point(116, 45);
            this.lesAnimaux.Name = "lesAnimaux";
            this.lesAnimaux.Size = new System.Drawing.Size(121, 23);
            this.lesAnimaux.TabIndex = 20;
            this.lesAnimaux.SelectedIndexChanged += new System.EventHandler(this.lesAnimaux_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Animal";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(698, 328);
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
            this.Nom.Size = new System.Drawing.Size(31, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Type";
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
            // FrmModifierType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierType";
            this.Load += new System.EventHandler(this.FrmModifierType_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ComboBox lesTypes;
        private ComboBox lesAnimaux;
        private Label label1;
        private Button btnQuitter;
        private Label Nom;
        private Button Modifier;
    }
}