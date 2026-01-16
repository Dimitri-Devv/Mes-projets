namespace Breeder
{
    partial class FrmAfficherDonneesVeterinaire
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lesVetos = new System.Windows.Forms.DataGridView();
            this.btnAjoutDuVaccin = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnAfficherVeto = new System.Windows.Forms.Button();
            this.textObservation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sesVaccins = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesVetos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sesVaccins)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(92, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 89);
            this.panel2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(583, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "label4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(457, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(348, 50);
            this.label9.TabIndex = 0;
            this.label9.Text = "Données vétérinaire";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(627, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "label5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lesVetos);
            this.panel1.Controls.Add(this.btnAjoutDuVaccin);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Controls.Add(this.btnModifier);
            this.panel1.Controls.Add(this.btnAfficherVeto);
            this.panel1.Controls.Add(this.textObservation);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.sesVaccins);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 781);
            this.panel1.TabIndex = 1;
            // 
            // lesVetos
            // 
            this.lesVetos.AllowUserToAddRows = false;
            this.lesVetos.AllowUserToDeleteRows = false;
            this.lesVetos.AllowUserToResizeColumns = false;
            this.lesVetos.AllowUserToResizeRows = false;
            this.lesVetos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesVetos.Location = new System.Drawing.Point(657, 166);
            this.lesVetos.Name = "lesVetos";
            this.lesVetos.ReadOnly = true;
            this.lesVetos.RowTemplate.Height = 25;
            this.lesVetos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesVetos.Size = new System.Drawing.Size(323, 196);
            this.lesVetos.TabIndex = 18;
            this.lesVetos.SelectionChanged += new System.EventHandler(this.lesVetos_SelectionChanged);
            // 
            // btnAjoutDuVaccin
            // 
            this.btnAjoutDuVaccin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAjoutDuVaccin.Location = new System.Drawing.Point(477, 303);
            this.btnAjoutDuVaccin.Name = "btnAjoutDuVaccin";
            this.btnAjoutDuVaccin.Size = new System.Drawing.Size(27, 193);
            this.btnAjoutDuVaccin.TabIndex = 2;
            this.btnAjoutDuVaccin.Text = "+";
            this.btnAjoutDuVaccin.UseVisualStyleBackColor = true;
            this.btnAjoutDuVaccin.Click += new System.EventHandler(this.btnAjoutDuVaccin_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(1202, 755);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 17;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(782, 669);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(115, 23);
            this.btnModifier.TabIndex = 16;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // btnAfficherVeto
            // 
            this.btnAfficherVeto.Location = new System.Drawing.Point(1138, 258);
            this.btnAfficherVeto.Name = "btnAfficherVeto";
            this.btnAfficherVeto.Size = new System.Drawing.Size(115, 23);
            this.btnAfficherVeto.TabIndex = 15;
            this.btnAfficherVeto.Text = "Afficher son profil";
            this.btnAfficherVeto.UseVisualStyleBackColor = true;
            this.btnAfficherVeto.Click += new System.EventHandler(this.btnAfficherVeto_Click);
            // 
            // textObservation
            // 
            this.textObservation.Location = new System.Drawing.Point(597, 419);
            this.textObservation.Multiline = true;
            this.textObservation.Name = "textObservation";
            this.textObservation.Size = new System.Drawing.Size(480, 225);
            this.textObservation.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(597, 382);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Observations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(782, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vétérinaire(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(201, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Ses Vaccins";
            // 
            // sesVaccins
            // 
            this.sesVaccins.AllowUserToAddRows = false;
            this.sesVaccins.AllowUserToDeleteRows = false;
            this.sesVaccins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sesVaccins.Location = new System.Drawing.Point(22, 148);
            this.sesVaccins.Name = "sesVaccins";
            this.sesVaccins.ReadOnly = true;
            this.sesVaccins.RowTemplate.Height = 25;
            this.sesVaccins.Size = new System.Drawing.Size(438, 544);
            this.sesVaccins.TabIndex = 11;
            // 
            // FrmAfficherDonneesVeterinaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 820);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAfficherDonneesVeterinaire";
            this.Text = "FrmAfficherDonneesVeterinaires";
            this.Load += new System.EventHandler(this.FrmAfficherDonneesVeterinaires_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesVetos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sesVaccins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel2;
        private Label label9;
        private Panel panel1;
        private Label label1;
        private DataGridView sesVaccins;
        private Label label3;
        private Label label2;
        private Button btnModifier;
        private Button btnAfficherVeto;
        private TextBox textObservation;
        private Button btnQuitter;
        private Label label4;
        private Label label5;
        private Button btnAjoutDuVaccin;
        private DataGridView lesVetos;
    }
}