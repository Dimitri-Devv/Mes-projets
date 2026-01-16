namespace Breeder
{
    partial class FrmModifierCommande
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
            this.btnToutRetirer = new System.Windows.Forms.Button();
            this.btnToutDeplacer = new System.Windows.Forms.Button();
            this.btnDeplacer = new System.Windows.Forms.Button();
            this.lesAnimaux = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lesCommandes = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRetirer = new System.Windows.Forms.Button();
            this.lesFournisseurs = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lesAnimauxConcernés = new System.Windows.Forms.DataGridView();
            this.leLibelle = new System.Windows.Forms.TextBox();
            this.labelLibelle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimaux)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxConcernés)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnToutRetirer);
            this.panel1.Controls.Add(this.btnToutDeplacer);
            this.panel1.Controls.Add(this.btnDeplacer);
            this.panel1.Controls.Add(this.lesAnimaux);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnModifier);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lesCommandes);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnRetirer);
            this.panel1.Controls.Add(this.lesFournisseurs);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lesAnimauxConcernés);
            this.panel1.Controls.Add(this.leLibelle);
            this.panel1.Controls.Add(this.labelLibelle);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 616);
            this.panel1.TabIndex = 3;
            // 
            // btnToutRetirer
            // 
            this.btnToutRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutRetirer.Location = new System.Drawing.Point(480, 294);
            this.btnToutRetirer.Name = "btnToutRetirer";
            this.btnToutRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnToutRetirer.TabIndex = 36;
            this.btnToutRetirer.Text = "<<";
            this.btnToutRetirer.UseVisualStyleBackColor = true;
            this.btnToutRetirer.Click += new System.EventHandler(this.btnToutRetirer_Click);
            // 
            // btnToutDeplacer
            // 
            this.btnToutDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutDeplacer.Location = new System.Drawing.Point(480, 140);
            this.btnToutDeplacer.Name = "btnToutDeplacer";
            this.btnToutDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnToutDeplacer.TabIndex = 35;
            this.btnToutDeplacer.Text = ">>";
            this.btnToutDeplacer.UseVisualStyleBackColor = true;
            this.btnToutDeplacer.Click += new System.EventHandler(this.btnToutDeplacer_Click);
            // 
            // btnDeplacer
            // 
            this.btnDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeplacer.Location = new System.Drawing.Point(480, 91);
            this.btnDeplacer.Name = "btnDeplacer";
            this.btnDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnDeplacer.TabIndex = 34;
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
            this.lesAnimaux.Location = new System.Drawing.Point(28, 91);
            this.lesAnimaux.Name = "lesAnimaux";
            this.lesAnimaux.ReadOnly = true;
            this.lesAnimaux.RowTemplate.Height = 25;
            this.lesAnimaux.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesAnimaux.Size = new System.Drawing.Size(419, 296);
            this.lesAnimaux.TabIndex = 33;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(948, 590);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Quitter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(507, 569);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(75, 23);
            this.btnModifier.TabIndex = 31;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 30;
            this.label5.Text = "Commande";
            // 
            // lesCommandes
            // 
            this.lesCommandes.FormattingEnabled = true;
            this.lesCommandes.Location = new System.Drawing.Point(125, 15);
            this.lesCommandes.Name = "lesCommandes";
            this.lesCommandes.Size = new System.Drawing.Size(121, 23);
            this.lesCommandes.TabIndex = 29;
            this.lesCommandes.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(600, 446);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(84, 23);
            this.numericUpDown1.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Coût total";
            // 
            // btnRetirer
            // 
            this.btnRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRetirer.Location = new System.Drawing.Point(480, 343);
            this.btnRetirer.Name = "btnRetirer";
            this.btnRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnRetirer.TabIndex = 25;
            this.btnRetirer.Text = "<";
            this.btnRetirer.UseVisualStyleBackColor = true;
            this.btnRetirer.Click += new System.EventHandler(this.btnRetirer_Click);
            // 
            // lesFournisseurs
            // 
            this.lesFournisseurs.FormattingEnabled = true;
            this.lesFournisseurs.Location = new System.Drawing.Point(125, 519);
            this.lesFournisseurs.Name = "lesFournisseurs";
            this.lesFournisseurs.Size = new System.Drawing.Size(121, 23);
            this.lesFournisseurs.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Animaux";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 522);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "Fournisseur";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(761, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Animaux concernés";
            // 
            // lesAnimauxConcernés
            // 
            this.lesAnimauxConcernés.AllowUserToAddRows = false;
            this.lesAnimauxConcernés.AllowUserToDeleteRows = false;
            this.lesAnimauxConcernés.AllowUserToResizeColumns = false;
            this.lesAnimauxConcernés.AllowUserToResizeRows = false;
            this.lesAnimauxConcernés.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesAnimauxConcernés.Location = new System.Drawing.Point(600, 91);
            this.lesAnimauxConcernés.Name = "lesAnimauxConcernés";
            this.lesAnimauxConcernés.ReadOnly = true;
            this.lesAnimauxConcernés.RowTemplate.Height = 25;
            this.lesAnimauxConcernés.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesAnimauxConcernés.Size = new System.Drawing.Size(419, 296);
            this.lesAnimauxConcernés.TabIndex = 19;
            // 
            // leLibelle
            // 
            this.leLibelle.Location = new System.Drawing.Point(125, 440);
            this.leLibelle.Name = "leLibelle";
            this.leLibelle.Size = new System.Drawing.Size(121, 23);
            this.leLibelle.TabIndex = 18;
            this.leLibelle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leLibelle_KeyPress);
            // 
            // labelLibelle
            // 
            this.labelLibelle.AutoSize = true;
            this.labelLibelle.Location = new System.Drawing.Point(28, 440);
            this.labelLibelle.Name = "labelLibelle";
            this.labelLibelle.Size = new System.Drawing.Size(41, 15);
            this.labelLibelle.TabIndex = 1;
            this.labelLibelle.Text = "Libelle";
            // 
            // FrmModifierCommande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 655);
            this.Controls.Add(this.panel1);
            this.Name = "FrmModifierCommande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmModifierCommande";
            this.Load += new System.EventHandler(this.FrmModifierCommande_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimaux)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lesAnimauxConcernés)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label label5;
        private ComboBox lesCommandes;
        private NumericUpDown numericUpDown1;
        private Label label4;
        private Button btnRetirer;
        private ComboBox lesFournisseurs;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView lesAnimauxConcernés;
        private TextBox leLibelle;
        private Label labelLibelle;
        private Button btnModifier;
        private Button button1;
        private DataGridView lesAnimaux;
        private Button btnDeplacer;
        private Button btnToutDeplacer;
        private Button btnToutRetirer;
    }
}