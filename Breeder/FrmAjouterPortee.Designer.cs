namespace Breeder
{
    partial class FrmAjouterPortee
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
            this.labelMere = new System.Windows.Forms.Label();
            this.boxMere = new System.Windows.Forms.ComboBox();
            this.datePortee = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.libelleBox = new System.Windows.Forms.TextBox();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Nom = new System.Windows.Forms.Label();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelMere);
            this.panel1.Controls.Add(this.boxMere);
            this.panel1.Controls.Add(this.datePortee);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.libelleBox);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.Nom);
            this.panel1.Controls.Add(this.btnAjouter);
            this.panel1.Location = new System.Drawing.Point(12, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 375);
            this.panel1.TabIndex = 3;
            // 
            // labelMere
            // 
            this.labelMere.AutoSize = true;
            this.labelMere.Location = new System.Drawing.Point(418, 64);
            this.labelMere.Name = "labelMere";
            this.labelMere.Size = new System.Drawing.Size(34, 15);
            this.labelMere.TabIndex = 26;
            this.labelMere.Text = "Mère";
            // 
            // boxMere
            // 
            this.boxMere.FormattingEnabled = true;
            this.boxMere.Location = new System.Drawing.Point(458, 61);
            this.boxMere.Name = "boxMere";
            this.boxMere.Size = new System.Drawing.Size(121, 23);
            this.boxMere.TabIndex = 25;
            // 
            // datePortee
            // 
            this.datePortee.Location = new System.Drawing.Point(116, 138);
            this.datePortee.Name = "datePortee";
            this.datePortee.Size = new System.Drawing.Size(200, 23);
            this.datePortee.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Date";
            // 
            // libelleBox
            // 
            this.libelleBox.Location = new System.Drawing.Point(116, 61);
            this.libelleBox.Name = "libelleBox";
            this.libelleBox.Size = new System.Drawing.Size(121, 23);
            this.libelleBox.TabIndex = 18;
            this.libelleBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.libelleBox_KeyPress);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(698, 348);
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
            this.Nom.Location = new System.Drawing.Point(48, 64);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(41, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Libelle";
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(353, 301);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(75, 23);
            this.btnAjouter.TabIndex = 0;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmAjouterPortee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjouterPortee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAjouterPortee";
            this.Load += new System.EventHandler(this.FrmAjouterPortee_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private DateTimePicker datePortee;
        private Label label1;
        private TextBox libelleBox;
        private Button btnQuitter;
        private Label Nom;
        private Button btnAjouter;
        private Label labelMere;
        private ComboBox boxMere;
    }
}