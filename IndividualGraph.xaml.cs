using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DactyloTest
{
    /// <summary>
    /// Logique d'interaction pour IndividualGraph.xaml
    /// </summary>
    public partial class IndividualGraph : UserControl
    {

        public SeriesCollection SeriesCollection { get; set; }

        public DateTime InitialDateTime { get; set; }
        public Func<double, string> XDateFormatter { get; set; } = value => new DateTime((long)value).ToString("yyyy-MM:dd HH:mm:ss");

        private DactylModel _dactylModel;
        private List<HighScore> _personalScores;
        private bool _isInitialized = false;

        public IndividualGraph()
        {
            InitializeComponent();
        }
        public void InitializeGraph(string nickname)
        {
            if (_isInitialized){ return; }
            this._dactylModel = new DactylModel();

            // Dictionnaire avec toutes les unités (WPM, CPS, Score et Accuracy)
            this._personalScores = this._dactylModel.GetPersonalScore(nickname);

            List<string> dates = new List<string>();
            dates.Add(null);
            foreach (HighScore score in this._personalScores)
            {
                dates.Add(score.Date.ToString("G"));
            }

            SeriesCollection = new SeriesCollection();
            if (this.SeriesCollection.Count == 0)
            {
                ShowLine("Score");
            }
            this.Score.Style = Application.Current.FindResource("SelectedRoundBtn") as Style;

            // Affecter les valeurs à l'axe X
            this.MainGraph.AxisX[0].Labels = dates;

            // Effacer les séparateurs sur l'axe X non nécessaires
            this.MainGraph.AxisX[0].Separator.StrokeThickness = 0;

            DataContext = this;
            _isInitialized = true;
        }

        public void HideLine(string unitName)
        {
            // Supprimer la série de données correspondantes
            for (int i = 0; i < SeriesCollection.Count; i++)
            {
                if (SeriesCollection[i].Title == unitName)
                {
                    SeriesCollection.Remove(SeriesCollection[i]);
                    break;
                }
            }

            // Supprimer l'axe correspondant
            for (int i = 0; i < this.MainGraph.AxisY.Count; i++)
            {
                if (this.MainGraph.AxisY[i].Name == unitName)
                {
                    this.MainGraph.AxisY.Remove(this.MainGraph.AxisY[i]);
                    //CropStopper();

                    // Attention à bien réactualiser l'index des axes pour les prochaines séries
                    foreach (LineSeries line in SeriesCollection)
                    {
                        line.ScalesYAt = this.MainGraph.AxisY.IndexOf(this.MainGraph.AxisY.First(x => x.Name == line.Name));
                    }
                }
            }
        }
        public void ShowLine(string unitName)
        {
            // Couleur de la ligne
            string hexColor = null;
            // Formattage
            Func<double, string> UnitOnY = null;
            // Valeurs limites pour uniformiser les axes Y
            double minValue = 0;
            double maxValue = 0;
            Func<HighScore, double> searchUnitValue = null;

            switch (unitName)
            {
                case "Score":
                    UnitOnY = x => string.Format("{0:0.##} pts", x);
                    hexColor = "#CCFA8334";
                    searchUnitValue = x => x.Score;
                    minValue = 0;
                    maxValue = 1500;
                    break;
                case "CPS":
                    UnitOnY = x => string.Format("{0:0.00} CPS", x);
                    hexColor = "#CCFCB686";
                    searchUnitValue = x => x.CPS;
                    minValue = 0;
                    maxValue = 15;
                    break;
                case "WPM":
                    UnitOnY = x => string.Format("{0:0.##} WPM", x);
                    hexColor = "#CCFDCFAF";
                    searchUnitValue = x => x.WPM;
                    minValue = 0;
                    maxValue = 200;
                    break;
                case "Accuracy":
                    UnitOnY = x => string.Format("{0:0.00} %", x * 100);
                    hexColor = "#CCFDE8D8";
                    searchUnitValue = x => x.Accuracy;
                    minValue = 0;
                    maxValue = 1;
                    break;
            }

            List<double> dataCollection = new List<double>();
            dataCollection.Add(double.NaN);
            foreach (HighScore score in this._personalScores)
            {
                dataCollection.Add(searchUnitValue(score));
            }
            dataCollection.Add(double.NaN);
            Brush strokeBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            Brush fillBrush = strokeBrush.Clone();
            fillBrush.Opacity = 0.1;

            // Ajouter l'axe Y correspondant
            this.MainGraph.AxisY.Insert(0, new Axis()
            {
                Foreground = Brushes.White,
                ShowLabels = true,
                IsEnabled = false,
                Name = unitName,
                MinValue = minValue,
                MaxValue = maxValue,
                LabelFormatter = UnitOnY,
                Separator = new LiveCharts.Wpf.Separator()
                {
                    Stroke = strokeBrush,
                    // Permettre des lignes quadrillées
                    StrokeDashArray = new DoubleCollection { 5 }
                }
            });

            LineSeries lineSeries = new LineSeries()
            {
                Title = unitName,
                Name = unitName,
                Values = dataCollection.AsChartValues(),
                Foreground = Brushes.White,
                Stroke = strokeBrush,
                Fill = fillBrush,
                DataLabels = false,
                LabelPoint = data => UnitOnY(data.Y),
                Margin = new Thickness(20)
            };

            // Ajoute la série de données
            SeriesCollection.Add(lineSeries);

            //CropStopper();

            // Attention à bien réactualiser l'index des axes Y pour toutes les séries
            foreach (LineSeries line in SeriesCollection)
            {
                line.ScalesYAt = this.MainGraph.AxisY.IndexOf(this.MainGraph.AxisY.Where(x => x.Name == line.Name).First());
            }
        }

        private void StyleSelectedButton(Button btn)
        {
            btn.Style = Application.Current.FindResource("SelectedRoundBtn") as Style;
            string newStyleName = "";
            switch (btn.Name)
            {
                case "Score":
                    newStyleName = "SelectedRoundBtn";
                    break;
                case "CPS":
                    newStyleName = "SelectedCPSRoundBtn";
                    break;
                case "WPM":
                    newStyleName = "SelectedWPMRoundBtn";
                    break;
                case "Accuracy":
                    newStyleName = "SelectedAccuracyRoundBtn";
                    break;
            }

            btn.Style = Application.Current.FindResource(newStyleName) as Style;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;
            string unitName = clickedBtn.Name;
            Debug.WriteLine(unitName + " cliqué");

            // Si elle existe, supprimer
            foreach (LineSeries lineSerie in SeriesCollection)
            {
                if (lineSerie.Title == unitName)
                {
                    if (this.SeriesCollection.Count != 1)
                    {
                        Debug.WriteLine("La série existe.");
                        try
                        {
                            HideLine(unitName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        clickedBtn.Style = Application.Current.FindResource("RoundBasicBtn") as Style;
                    }
                    return;
                }
            }
            // Si la série n'existe pas
            try
            {
                ShowLine(unitName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            StyleSelectedButton(clickedBtn);
        }
    }
}
