namespace Breeder
{
    partial class FrmAfficherVeterinaire
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lesAnimauxACharges = new System.Windows.Forms.DataGridView();
            this.btnRetirer = new System.Windows.Forms.Button();
            this.btnToutRetirer = new System.Windows.Forms.Button();
            this.btnToutDeplacer = new System.Windows.Forms.Button();
            this.btnDeplacer = new System.Windows.Forms.Button();
            this.lesAnimaux = new System.Windows.Forms.DataGridView();
            this.leTel = new System.Windows.Forms.Label();
            this.lAdresse = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.leNom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leMail = new System.Windows.Forms.Label();
            this.fref = new System.Windows.Forms.Label();
            this.tel = new System.Windows.Forms.Label();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxACharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimaux)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lesAnimauxACharges);
            this.panel1.Controls.Add(this.btnRetirer);
            this.panel1.Controls.Add(this.btnToutRetirer);
            this.panel1.Controls.Add(this.btnToutDeplacer);
            this.panel1.Controls.Add(this.btnDeplacer);
            this.panel1.Controls.Add(this.lesAnimaux);
            this.panel1.Controls.Add(this.leTel);
            this.panel1.Controls.Add(this.lAdresse);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.leNom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.leMail);
            this.panel1.Controls.Add(this.fref);
            this.panel1.Controls.Add(this.tel);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1315, 664);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(896, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Animaux à charges";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 40;
            this.label3.Text = "Animaux";
            // 
            // lesAnimauxACharges
            // 
            this.lesAnimauxACharges.AllowUserToAddRows = false;
            this.lesAnimauxACharges.AllowUserToDeleteRows = false;
            this.lesAnimauxACharges.AllowUserToResizeColumns = false;
            this.lesAnimauxACharges.AllowUserToResizeRows = false;
            this.lesAnimauxACharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesAnimauxACharges.Location = new System.Drawing.Point(727, 175);
            this.lesAnimauxACharges.Name = "lesAnimauxACharges";
            this.lesAnimauxACharges.ReadOnly = true;
            this.lesAnimauxACharges.RowTemplate.Height = 25;
            this.lesAnimauxACharges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesAnimauxACharges.Size = new System.Drawing.Size(419, 296);
            this.lesAnimauxACharges.TabIndex = 39;
            // 
            // btnRetirer
            // 
            this.btnRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRetirer.Location = new System.Drawing.Point(603, 428);
            this.btnRetirer.Name = "btnRetirer";
            this.btnRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnRetirer.TabIndex = 38;
            this.btnRetirer.Text = "<";
            this.btnRetirer.UseVisualStyleBackColor = true;
            this.btnRetirer.Click += new System.EventHandler(this.btnRetirer_Click);
            // 
            // btnToutRetirer
            // 
            this.btnToutRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutRetirer.Location = new System.Drawing.Point(603, 379);
            this.btnToutRetirer.Name = "btnToutRetirer";
            this.btnToutRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnToutRetirer.TabIndex = 37;
            this.btnToutRetirer.Text = "<<";
            this.btnToutRetirer.UseVisualStyleBackColor = true;
            this.btnToutRetirer.Click += new System.EventHandler(this.btnToutRetirer_Click);
            // 
            // btnToutDeplacer
            // 
            this.btnToutDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutDeplacer.Location = new System.Drawing.Point(603, 224);
            this.btnToutDeplacer.Name = "btnToutDeplacer";
            this.btnToutDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnToutDeplacer.TabIndex = 36;
            this.btnToutDeplacer.Text = ">>";
            this.btnToutDeplacer.UseVisualStyleBackColor = true;
            this.btnToutDeplacer.Click += new System.EventHandler(this.btnToutDeplacer_Click);
            // 
            // btnDeplacer
            // 
            this.btnDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeplacer.Location = new System.Drawing.Point(603, 175);
            this.btnDeplacer.Name = "btnDeplacer";
            this.btnDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnDeplacer.TabIndex = 35;
            this.btnDeplacer.Text = ">";
            this.btnDeplacer.UseVisualStyleBackColor = true;
            this.btnDeplacer.Click += new System.EventHandler(this.btnDeplacer_Click);
            // 
            // lesAnimaux
            // 
            this.lesAnimaux.AllowUserToAddRows = false;
            this.lesAnimaux.AllowUserToDeleteRows = false;
            this.lesAnimaux.AllowUserToResizeColumns = false;
            this.lesAnimaux.AllowUserToResizeRows = false;
            this.lesAnimaux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesAnimaux.Location = new System.Drawing.Point(148, 175);
            this.lesAnimaux.Name = "lesAnimaux";
            this.lesAnimaux.ReadOnly = true;
            this.lesAnimaux.RowTemplate.Height = 25;
            this.lesAnimaux.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesAnimaux.Size = new System.Drawing.Size(419, 296);
            this.lesAnimaux.TabIndex = 34;
            // 
            // leTel
            // 
            this.leTel.AutoSize = true;
            this.leTel.Location = new System.Drawing.Point(459, 619);
            this.leTel.Name = "leTel";
            this.leTel.Size = new System.Drawing.Size(30, 15);
            this.leTel.TabIndex = 13;
            this.leTel.Text = "leTel";
            // 
            // lAdresse
            // 
            this.lAdresse.AutoSize = true;
            this.lAdresse.Location = new System.Drawing.Point(125, 619);
            this.lAdresse.Name = "lAdresse";
            this.lAdresse.Size = new System.Drawing.Size(51, 15);
            this.lAdresse.TabIndex = 12;
            this.lAdresse.Text = "lAdresse";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(105, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 77);
            this.panel2.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(457, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(197, 50);
            this.label9.TabIndex = 0;
            this.label9.Text = "Vétérinaire";
            // 
            // leNom
            // 
            this.leNom.AutoSize = true;
            this.leNom.Location = new System.Drawing.Point(125, 563);
            this.leNom.Name = "leNom";
            this.leNom.Size = new System.Drawing.Size(43, 15);
            this.leNom.TabIndex = 8;
            this.leNom.Text = "leNom";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 563);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 563);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mail : ";
            // 
            // leMail
            // 
            this.leMail.AutoSize = true;
            this.leMail.Location = new System.Drawing.Point(459, 563);
            this.leMail.Name = "leMail";
            this.leMail.Size = new System.Drawing.Size(39, 15);
            this.leMail.TabIndex = 3;
            this.leMail.Text = "leMail";
            // 
            // fref
            // 
            this.fref.AutoSize = true;
            this.fref.Location = new System.Drawing.Point(40, 619);
            this.fref.Name = "fref";
            this.fref.Size = new System.Drawing.Size(54, 15);
            this.fref.TabIndex = 4;
            this.fref.Text = "Adresse :";
            // 
            // tel
            // 
            this.tel.AutoSize = true;
            this.tel.Location = new System.Drawing.Point(356, 619);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(67, 15);
            this.tel.TabIndex = 5;
            this.tel.Text = "Téléphone :";
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(1231, 634);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 1;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // FrmAfficherVeterinaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 670);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAfficherVeterinaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAfficherVeterinaire";
            this.Load += new System.EventHandler(this.FrmAfficherVeterinaire_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxACharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimaux)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label leTel;
        private Label lAdresse;
        private Panel panel2;
        private Label label9;
        private Label leNom;
        private Label label1;
        private Label label2;
        private Label leMail;
        private Label fref;
        private Label tel;
        private Button btnQuitter;
        private DataGridView lesAnimaux;
        private Button btnDeplacer;
        private Button btnToutDeplacer;
        private Button btnToutRetirer;
        private Button btnRetirer;
        private DataGridView lesAnimauxACharges;
        private Label label4;
        private Label label3;
    }
}