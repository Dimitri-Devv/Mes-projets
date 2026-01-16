namespace Breeder
{
    partial class FrmAjoutPoids
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
            this.btnAjout = new System.Windows.Forms.Button();
            this.g = new System.Windows.Forms.RadioButton();
            this.kg = new System.Windows.Forms.RadioButton();
            this.lePoids = new System.Windows.Forms.NumericUpDown();
            this.label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lePoids)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.btnAjout);
            this.panel1.Controls.Add(this.g);
            this.panel1.Controls.Add(this.kg);
            this.panel1.Controls.Add(this.lePoids);
            this.panel1.Controls.Add(this.label);
            this.panel1.Location = new System.Drawing.Point(12, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 304);
            this.panel1.TabIndex = 0;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(509, 281);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 13;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // btnAjout
            // 
            this.btnAjout.Location = new System.Drawing.Point(257, 211);
            this.btnAjout.Name = "btnAjout";
            this.btnAjout.Size = new System.Drawing.Size(75, 23);
            this.btnAjout.TabIndex = 4;
            this.btnAjout.Text = "Ajouter";
            this.btnAjout.UseVisualStyleBackColor = true;
            this.btnAjout.Click += new System.EventHandler(this.btnAjout_Click);
            // 
            // g
            // 
            this.g.AutoSize = true;
            this.g.Location = new System.Drawing.Point(389, 143);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(32, 19);
            this.g.TabIndex = 3;
            this.g.TabStop = true;
            this.g.Text = "g";
            this.g.UseVisualStyleBackColor = true;
            // 
            // kg
            // 
            this.kg.AutoSize = true;
            this.kg.Location = new System.Drawing.Point(389, 84);
            this.kg.Name = "kg";
            this.kg.Size = new System.Drawing.Size(39, 19);
            this.kg.TabIndex = 2;
            this.kg.TabStop = true;
            this.kg.Text = "Kg";
            this.kg.UseVisualStyleBackColor = true;
            // 
            // lePoids
            // 
            this.lePoids.Location = new System.Drawing.Point(237, 111);
            this.lePoids.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.lePoids.Name = "lePoids";
            this.lePoids.Size = new System.Drawing.Size(120, 23);
            this.lePoids.TabIndex = 1;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(133, 113);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(71, 15);
            this.label.TabIndex = 0;
            this.label.Text = "Poids actuel";
            // 
            // FrmAjoutPoids
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 409);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "FrmAjoutPoids";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAjoutPoids";
            this.Load += new System.EventHandler(this.FrmAjoutPoids_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lePoids)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Button btnAjout;
        private RadioButton g;
        private RadioButton kg;
        private NumericUpDown lePoids;
        private Label label;
        private Button btnQuitter;
    }
}