namespace Breeder
{
    partial class FrmProfilAnimal
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
            this.label6 = new System.Windows.Forms.Label();
            this.lesDepenses = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.poids = new System.Windows.Forms.Button();
            this.DonneeVeto = new System.Windows.Forms.Button();
            this.laFamille = new System.Windows.Forms.DataGridView();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Modifier = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.boxProprio = new System.Windows.Forms.ComboBox();
            this.lesTypes = new System.Windows.Forms.ComboBox();
            this.type = new System.Windows.Forms.Label();
            this.lesRaces = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lesStatuts = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lesMeres = new System.Windows.Forms.ComboBox();
            this.lesPeres = new System.Windows.Forms.ComboBox();
            this.dateNaissance = new System.Windows.Forms.DateTimePicker();
            this.PoidsActuel = new System.Windows.Forms.NumericUpDown();
            this.labelNaissance = new System.Windows.Forms.Label();
            this.Mere = new System.Windows.Forms.Label();
            this.titre = new System.Windows.Forms.Label();
            this.Pere = new System.Windows.Forms.Label();
            this.labelPoids = new System.Windows.Forms.Label();
            this.sexe = new System.Windows.Forms.Label();
            this.leNom = new System.Windows.Forms.TextBox();
            this.Prenom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lesSexes = new System.Windows.Forms.ComboBox();
            this.Nom = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesDepenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laFamille)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoidsActuel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lesDepenses);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.poids);
            this.panel2.Controls.Add(this.DonneeVeto);
            this.panel2.Controls.Add(this.laFamille);
            this.panel2.Controls.Add(this.btnQuitter);
            this.panel2.Location = new System.Drawing.Point(1074, 126);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 702);
            this.panel2.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 298);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 48;
            this.label6.Text = "Les dépenses";
            // 
            // lesDepenses
            // 
            this.lesDepenses.AllowUserToAddRows = false;
            this.lesDepenses.AllowUserToDeleteRows = false;
            this.lesDepenses.AllowUserToResizeColumns = false;
            this.lesDepenses.AllowUserToResizeRows = false;
            this.lesDepenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lesDepenses.Location = new System.Drawing.Point(41, 316);
            this.lesDepenses.Name = "lesDepenses";
            this.lesDepenses.ReadOnly = true;
            this.lesDepenses.RowTemplate.Height = 25;
            this.lesDepenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lesDepenses.Size = new System.Drawing.Size(304, 198);
            this.lesDepenses.TabIndex = 47;
            this.lesDepenses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lesDepenses_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Famille";
            // 
            // poids
            // 
            this.poids.Location = new System.Drawing.Point(107, 556);
            this.poids.Name = "poids";
            this.poids.Size = new System.Drawing.Size(167, 23);
            this.poids.TabIndex = 4;
            this.poids.Text = "Courbe des poids";
            this.poids.UseVisualStyleBackColor = true;
            this.poids.Click += new System.EventHandler(this.poids_Click);
            // 
            // DonneeVeto
            // 
            this.DonneeVeto.Location = new System.Drawing.Point(107, 609);
            this.DonneeVeto.Name = "DonneeVeto";
            this.DonneeVeto.Size = new System.Drawing.Size(167, 23);
            this.DonneeVeto.TabIndex = 3;
            this.DonneeVeto.Text = "Donnée Vétérinaire";
            this.DonneeVeto.UseVisualStyleBackColor = true;
            this.DonneeVeto.Click += new System.EventHandler(this.DonneeVeto_Click);
            // 
            // laFamille
            // 
            this.laFamille.AllowUserToAddRows = false;
            this.laFamille.AllowUserToDeleteRows = false;
            this.laFamille.AllowUserToResizeColumns = false;
            this.laFamille.AllowUserToResizeRows = false;
            this.laFamille.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.laFamille.Location = new System.Drawing.Point(29, 35);
            this.laFamille.MultiSelect = false;
            this.laFamille.Name = "laFamille";
            this.laFamille.ReadOnly = true;
            this.laFamille.RowTemplate.Height = 25;
            this.laFamille.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.laFamille.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.laFamille.Size = new System.Drawing.Size(316, 205);
            this.laFamille.TabIndex = 2;
            this.laFamille.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFamille_CellClick);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(270, 656);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(75, 23);
            this.btnQuitter.TabIndex = 1;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // Modifier
            // 
            this.Modifier.Location = new System.Drawing.Point(474, 670);
            this.Modifier.Name = "Modifier";
            this.Modifier.Size = new System.Drawing.Size(167, 23);
            this.Modifier.TabIndex = 0;
            this.Modifier.Text = "Modifier";
            this.Modifier.UseVisualStyleBackColor = true;
            this.Modifier.Click += new System.EventHandler(this.Modifier_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(324, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 77);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(340, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "ProfilAnimal";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.boxProprio);
            this.panel4.Controls.Add(this.lesTypes);
            this.panel4.Controls.Add(this.type);
            this.panel4.Controls.Add(this.lesRaces);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lesStatuts);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lesMeres);
            this.panel4.Controls.Add(this.lesPeres);
            this.panel4.Controls.Add(this.dateNaissance);
            this.panel4.Controls.Add(this.PoidsActuel);
            this.panel4.Controls.Add(this.labelNaissance);
            this.panel4.Controls.Add(this.Mere);
            this.panel4.Controls.Add(this.titre);
            this.panel4.Controls.Add(this.Pere);
            this.panel4.Controls.Add(this.Modifier);
            this.panel4.Controls.Add(this.labelPoids);
            this.panel4.Controls.Add(this.sexe);
            this.panel4.Controls.Add(this.leNom);
            this.panel4.Controls.Add(this.Prenom);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lesSexes);
            this.panel4.Controls.Add(this.Nom);
            this.panel4.Location = new System.Drawing.Point(12, 126);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1056, 702);
            this.panel4.TabIndex = 6;
            // 
            // boxProprio
            // 
            this.boxProprio.FormattingEnabled = true;
            this.boxProprio.Location = new System.Drawing.Point(710, 454);
            this.boxProprio.Name = "boxProprio";
            this.boxProprio.Size = new System.Drawing.Size(121, 23);
            this.boxProprio.TabIndex = 46;
            // 
            // lesTypes
            // 
            this.lesTypes.FormattingEnabled = true;
            this.lesTypes.Location = new System.Drawing.Point(194, 548);
            this.lesTypes.Name = "lesTypes";
            this.lesTypes.Size = new System.Drawing.Size(121, 23);
            this.lesTypes.TabIndex = 43;
            // 
            // type
            // 
            this.type.AutoSize = true;
            this.type.Location = new System.Drawing.Point(116, 556);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(31, 15);
            this.type.TabIndex = 42;
            this.type.Text = "Type";
            // 
            // lesRaces
            // 
            this.lesRaces.FormattingEnabled = true;
            this.lesRaces.Location = new System.Drawing.Point(709, 548);
            this.lesRaces.Name = "lesRaces";
            this.lesRaces.Size = new System.Drawing.Size(121, 23);
            this.lesRaces.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(609, 556);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "Race";
            // 
            // lesStatuts
            // 
            this.lesStatuts.FormattingEnabled = true;
            this.lesStatuts.Location = new System.Drawing.Point(194, 454);
            this.lesStatuts.Name = "lesStatuts";
            this.lesStatuts.Size = new System.Drawing.Size(121, 23);
            this.lesStatuts.TabIndex = 39;
            this.lesStatuts.SelectedIndexChanged += new System.EventHandler(this.lesStatuts_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 457);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Statut";
            // 
            // lesMeres
            // 
            this.lesMeres.FormattingEnabled = true;
            this.lesMeres.Location = new System.Drawing.Point(710, 356);
            this.lesMeres.Name = "lesMeres";
            this.lesMeres.Size = new System.Drawing.Size(121, 23);
            this.lesMeres.TabIndex = 37;
            // 
            // lesPeres
            // 
            this.lesPeres.FormattingEnabled = true;
            this.lesPeres.Location = new System.Drawing.Point(194, 356);
            this.lesPeres.Name = "lesPeres";
            this.lesPeres.Size = new System.Drawing.Size(121, 23);
            this.lesPeres.TabIndex = 36;
            // 
            // dateNaissance
            // 
            this.dateNaissance.Location = new System.Drawing.Point(699, 217);
            this.dateNaissance.Name = "dateNaissance";
            this.dateNaissance.Size = new System.Drawing.Size(200, 23);
            this.dateNaissance.TabIndex = 35;
            // 
            // PoidsActuel
            // 
            this.PoidsActuel.Location = new System.Drawing.Point(216, 263);
            this.PoidsActuel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PoidsActuel.Name = "PoidsActuel";
            this.PoidsActuel.Size = new System.Drawing.Size(99, 23);
            this.PoidsActuel.TabIndex = 34;
            // 
            // labelNaissance
            // 
            this.labelNaissance.AutoSize = true;
            this.labelNaissance.Location = new System.Drawing.Point(572, 225);
            this.labelNaissance.Name = "labelNaissance";
            this.labelNaissance.Size = new System.Drawing.Size(103, 15);
            this.labelNaissance.TabIndex = 33;
            this.labelNaissance.Text = "Date de Naissance";
            // 
            // Mere
            // 
            this.Mere.AutoSize = true;
            this.Mere.Location = new System.Drawing.Point(607, 356);
            this.Mere.Name = "Mere";
            this.Mere.Size = new System.Drawing.Size(34, 15);
            this.Mere.TabIndex = 31;
            this.Mere.Text = "Mere";
            // 
            // titre
            // 
            this.titre.AutoSize = true;
            this.titre.Location = new System.Drawing.Point(607, 457);
            this.titre.Name = "titre";
            this.titre.Size = new System.Drawing.Size(68, 15);
            this.titre.TabIndex = 4;
            this.titre.Text = "Propriétaire";
            // 
            // Pere
            // 
            this.Pere.AutoSize = true;
            this.Pere.Location = new System.Drawing.Point(116, 356);
            this.Pere.Name = "Pere";
            this.Pere.Size = new System.Drawing.Size(30, 15);
            this.Pere.TabIndex = 30;
            this.Pere.Text = "Pere";
            // 
            // labelPoids
            // 
            this.labelPoids.AutoSize = true;
            this.labelPoids.Location = new System.Drawing.Point(116, 265);
            this.labelPoids.Name = "labelPoids";
            this.labelPoids.Size = new System.Drawing.Size(73, 15);
            this.labelPoids.TabIndex = 28;
            this.labelPoids.Text = "Poids Actuel";
            // 
            // sexe
            // 
            this.sexe.AutoSize = true;
            this.sexe.Location = new System.Drawing.Point(116, 172);
            this.sexe.Name = "sexe";
            this.sexe.Size = new System.Drawing.Size(31, 15);
            this.sexe.TabIndex = 27;
            this.sexe.Text = "Sexe";
            // 
            // leNom
            // 
            this.leNom.Location = new System.Drawing.Point(194, 72);
            this.leNom.Name = "leNom";
            this.leNom.Size = new System.Drawing.Size(121, 23);
            this.leNom.TabIndex = 26;
            this.leNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.leNom_KeyPress);
            // 
            // Prenom
            // 
            this.Prenom.Location = new System.Drawing.Point(709, 72);
            this.Prenom.Name = "Prenom";
            this.Prenom.Size = new System.Drawing.Size(121, 23);
            this.Prenom.TabIndex = 25;
            this.Prenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Prenom_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(609, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Prenom";
            // 
            // lesSexes
            // 
            this.lesSexes.FormattingEnabled = true;
            this.lesSexes.Location = new System.Drawing.Point(194, 172);
            this.lesSexes.Name = "lesSexes";
            this.lesSexes.Size = new System.Drawing.Size(121, 23);
            this.lesSexes.TabIndex = 21;
            // 
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Location = new System.Drawing.Point(120, 72);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(34, 15);
            this.Nom.TabIndex = 1;
            this.Nom.Text = "Nom";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(321, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 15);
            this.label7.TabIndex = 47;
            this.label7.Text = "Kg";
            // 
            // FrmProfilAnimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1434, 831);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmProfilAnimal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmProfilAnimal";
            this.Load += new System.EventHandler(this.FrmProfilAnimal_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lesDepenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laFamille)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoidsActuel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel2;
        private DataGridView laFamille;
        private Button btnQuitter;
        private Button Modifier;
        private Panel panel1;
        private Label label1;
        private Button poids;
        private Button DonneeVeto;
        private Panel panel4;
        private Label Mere;
        private Label titre;
        private Label Pere;
        private Label labelPoids;
        private Label sexe;
        private TextBox leNom;
        private TextBox Prenom;
        private Label label2;
        private ComboBox lesSexes;
        private Label Nom;
        private Label labelNaissance;
        private DateTimePicker dateNaissance;
        private NumericUpDown PoidsActuel;
        private ComboBox lesMeres;
        private ComboBox lesPeres;
        private ComboBox lesStatuts;
        private Label label3;
        private ComboBox lesRaces;
        private Label label4;
        private ComboBox lesTypes;
        private Label type;
        private ComboBox boxProprio;
        private Label label5;
        private Label label6;
        private DataGridView lesDepenses;
        private Label label7;
    }
}