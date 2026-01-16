using LiveCharts.Wpf;
using LiveCharts;
using Stage;

using Brushes = System.Windows.Media.Brushes;
using System.Windows.Media;
using iTextSharp.text;


namespace Breeder
{
    public partial class FrmCourbePoids : Form
    {
        private Animal _animal;

        public FrmCourbePoids(Animal animal)
        {
            InitializeComponent();
            _animal = animal;
        }

        private void FrmCourbePoids_Load(object sender, EventArgs e)
        {
            label2.Text = "Animal : " + _animal.Nom + " " + _animal.Prenom;
            label2.Visible = true;

            List<CourbePoids> courbePoids = FacadeProvider.GetInstance().PoidsFacade().GetCourbesPoids(_animal.Id).ToList();
            courbePoids.Sort((x, y) => x.DateSaisie.CompareTo(y.DateSaisie));

            decimal[] poids = courbePoids.Select(x => ConversionUtils.KilogrammesToGrammes(x.Poids)).ToArray();
            DateTime[] dates = courbePoids.Select(x => x.DateSaisie).ToArray();

            StackedAreaSeries poidsSeries = new StackedAreaSeries
            {
                Title = "Poids (en grammes)",
                Values = new ChartValues<decimal>(poids),
                StrokeThickness = 1,
                PointGeometrySize = 10,
                PointForeground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(40, 26, 29)),
            };


            graphique.Series.Clear();
            graphique.Series.Add(poidsSeries);

            AxesCollection xaxis = new AxesCollection();
            xaxis.Add(new Axis
            {
                Title = "Poids",
                Foreground = Brushes.Blue,
                FontSize = 16.0,
            });

            graphique.AxisY = xaxis;


            AxesCollection yaxis = new AxesCollection();
            yaxis.Add(new Axis
            {
                Title = "Jour",
                Foreground = Brushes.Yellow,
                FontSize = 16.0,
            });

            graphique.AxisX = yaxis;


            graphique.Zoom = ZoomingOptions.Xy;
            graphique.Hoverable = true;


            //masquer le bouton mettre à jour si cela à déjà été fait dans la journée et que son statut n'est pas décédé
            button2.Enabled = false;

            if (FacadeProvider.GetInstance().PoidsFacade().VerifierSaisiePoidsAujourdhui(_animal.Id) && _animal.IdStatut != 2)
            {
                button2.Enabled = true;
            }
        }


        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmAccueil());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Êtes-vous sur de mettre à jour le poids ?",
                "Mise à jour poids", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Program.SwitchMainForm(new FrmAjoutPoids(_animal));
                
            }
        }

        private void Tableau_Click(object sender, EventArgs e)
        {
            Program.SwitchMainForm(new FrmTableauPoids(_animal));
        }
 
    }
}