namespace Breeder
{
    partial class FrmFactureDevis
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
            this.btnFacture = new System.Windows.Forms.RadioButton();
            this.btnDevis = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Animal = new System.Windows.Forms.DataGridView();
            this.btnExporter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRetirer = new System.Windows.Forms.Button();
            this.btnToutRetirer = new System.Windows.Forms.Button();
            this.btnToutDeplacer = new System.Windows.Forms.Button();
            this.btnDeplacer = new System.Windows.Forms.Button();
            this.lesClients = new System.Windows.Forms.DataGridView();
            this.AnimalConcerné = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVente = new System.Windows.Forms.RadioButton();
            this.btnReserve = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Animal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimalConcerné)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFacture
            // 
            this.btnFacture.AutoSize = true;
            this.btnFacture.Location = new System.Drawing.Point(312, 31);
            this.btnFacture.Name = "btnFacture";
            this.btnFacture.Size = new System.Drawing.Size(64, 19);
            this.btnFacture.TabIndex = 0;
            this.btnFacture.TabStop = true;
            this.btnFacture.Text = "Facture";
            this.btnFacture.UseVisualStyleBackColor = true;
            // 
            // btnDevis
            // 
            this.btnDevis.AutoSize = true;
            this.btnDevis.Location = new System.Drawing.Point(535, 31);
            this.btnDevis.Name = "btnDevis";
            this.btnDevis.Size = new System.Drawing.Size(53, 19);
            this.btnDevis.TabIndex = 1;
            this.btnDevis.TabStop = true;
            this.btnDevis.Text = "Devis";
            this.btnDevis.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choisir un animal";
            // 
            // Animal
            // 
            this.Animal.AllowUserToAddRows = false;
            this.Animal.AllowUserToDeleteRows = false;
            this.Animal.AllowUserToResizeColumns = false;
            this.Animal.AllowUserToResizeRows = false;
            this.Animal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Animal.Location = new System.Drawing.Point(143, 405);
            this.Animal.Name = "Animal";
            this.Animal.ReadOnly = true;
            this.Animal.RowTemplate.Height = 25;
            this.Animal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Animal.Size = new System.Drawing.Size(359, 269);
            this.Animal.TabIndex = 3;
            // 
            // btnExporter
            // 
            this.btnExporter.Location = new System.Drawing.Point(586, 737);
            this.btnExporter.Name = "btnExporter";
            this.btnExporter.Size = new System.Drawing.Size(111, 23);
            this.btnExporter.TabIndex = 6;
            this.btnExporter.Text = "Exporter en PDF";
            this.btnExporter.UseVisualStyleBackColor = true;
            this.btnExporter.Click += new System.EventHandler(this.btnExporter_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReserve);
            this.panel1.Controls.Add(this.btnVente);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnRetirer);
            this.panel1.Controls.Add(this.btnToutRetirer);
            this.panel1.Controls.Add(this.btnToutDeplacer);
            this.panel1.Controls.Add(this.btnDeplacer);
            this.panel1.Controls.Add(this.lesClients);
            this.panel1.Controls.Add(this.AnimalConcerné);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnFacture);
            this.panel1.Controls.Add(this.btnExporter);
            this.panel1.Controls.Add(this.btnDevis);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Animal);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1281, 775);
            this.panel1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(842, 387);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 36;
            this.label5.Text = "Animaux concernés";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 35;
            this.label4.Text = "Animaux";
            // 
            // btnRetirer
            // 
            this.btnRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRetirer.Location = new System.Drawing.Point(564, 631);
            this.btnRetirer.Name = "btnRetirer";
            this.btnRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnRetirer.TabIndex = 34;
            this.btnRetirer.Text = "<";
            this.btnRetirer.UseVisualStyleBackColor = true;
            this.btnRetirer.Click += new System.EventHandler(this.btnRetirer_Click);
            // 
            // btnToutRetirer
            // 
            this.btnToutRetirer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutRetirer.Location = new System.Drawing.Point(564, 582);
            this.btnToutRetirer.Name = "btnToutRetirer";
            this.btnToutRetirer.Size = new System.Drawing.Size(82, 43);
            this.btnToutRetirer.TabIndex = 33;
            this.btnToutRetirer.Text = "<<";
            this.btnToutRetirer.UseVisualStyleBackColor = true;
            this.btnToutRetirer.Click += new System.EventHandler(this.btnToutRetirer_Click);
            // 
            // btnToutDeplacer
            // 
            this.btnToutDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnToutDeplacer.Location = new System.Drawing.Point(564, 405);
            this.btnToutDeplacer.Name = "btnToutDeplacer";
            this.btnToutDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnToutDeplacer.TabIndex = 32;
            this.btnToutDeplacer.Text = ">>";
            this.btnToutDeplacer.UseVisualStyleBackColor = true;
            this.btnToutDeplacer.Click += new System.EventHandler(this.btnToutDeplacer_Click);
            // 
            // btnDeplacer
            // 
            this.btnDeplacer.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeplacer.Location = new System.Drawing.Point(564, 452);
            this.btnDeplacer.Name = "btnDeplacer";
            this.btnDeplacer.Size = new System.Drawing.Size(82, 43);
            this.btnDeplacer.TabIndex = 31;
            this.btnDeplacer.Text = ">";
            this.btnDeplacer.UseVisualStyleBackColor = true;
            this.btnDeplacer.Click += new System.EventHandler(this.btnDeplacer_Click);
            // 
            // lesClients
            // 
            this.lesClients.AllowUserToAddRows = false;
            this.lesClients.AllowUserToDeleteRows = false;
            this.lesClients.AllowUserToResizeColumns = false;
            this.lesClients.AllowUserToResizeRows = false;
            this.lesClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesClients.Location = new System.Drawing.Point(441, 105);
            this.lesClients.Name = "lesClients";
            this.lesClients.ReadOnly = true;
            this.lesClients.RowTemplate.Height = 25;
            this.lesClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesClients.Size = new System.Drawing.Size(362, 228);
            this.lesClients.TabIndex = 11;
            // 
            // AnimalConcerné
            // 
            this.AnimalConcerné.AllowUserToAddRows = false;
            this.AnimalConcerné.AllowUserToDeleteRows = false;
            this.AnimalConcerné.AllowUserToResizeColumns = false;
            this.AnimalConcerné.AllowUserToResizeRows = false;
            this.AnimalConcerné.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnimalConcerné.Location = new System.Drawing.Point(716, 405);
            this.AnimalConcerné.Name = "AnimalConcerné";
            this.AnimalConcerné.RowTemplate.Height = 25;
            this.AnimalConcerné.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AnimalConcerné.Size = new System.Drawing.Size(359, 269);
            this.AnimalConcerné.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(574, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Client concerné";
            // 
            // btnVente
            // 
            this.btnVente.AutoSize = true;
            this.btnVente.Location = new System.Drawing.Point(737, 31);
            this.btnVente.Name = "btnVente";
            this.btnVente.Size = new System.Drawing.Size(54, 19);
            this.btnVente.TabIndex = 37;
            this.btnVente.TabStop = true;
            this.btnVente.Text = "Vente";
            this.btnVente.UseVisualStyleBackColor = true;
            // 
            // btnReserve
            // 
            this.btnReserve.AutoSize = true;
            this.btnReserve.Location = new System.Drawing.Point(950, 31);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(86, 19);
            this.btnReserve.TabIndex = 38;
            this.btnReserve.TabStop = true;
            this.btnReserve.Text = "Réservation";
            this.btnReserve.UseVisualStyleBackColor = true;
            // 
            // FrmFactureDevis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 814);
            this.Controls.Add(this.panel1);
            this.Name = "FrmFactureDevis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFactureDevis";
            this.Load += new System.EventHandler(this.FrmFactureDevis_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Animal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimalConcerné)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton btnFacture;
        private RadioButton btnDevis;
        private Label label1;
        private DataGridView Animal;
        private Button btnExporter;
        private Panel panel1;
        private Label label3;
        private DataGridView lesClients;
        private DataGridView AnimalConcerné;
        private Button btnDeplacer;
        private Button btnToutDeplacer;
        private Button btnToutRetirer;
        private Button btnRetirer;
        private Label label4;
        private Label label5;
        private RadioButton btnReserve;
        private RadioButton btnVente;
    }
}