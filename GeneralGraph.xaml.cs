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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace DactyloTest
{
    /// <summary>
    /// Logique d'interaction pour GeneralGraph.xaml
    /// </summary>
    public partial class GeneralGraph : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] LabelsOnY { get; set; }
        private DactylModel _dactylModel;

        private Dictionary<string, Dictionary<string, double>> _allMeans;

        public GeneralGraph()
        {
            InitializeComponent();
            InitializeTable();
        }
        public void ShowRow(string unitName)
        {
            IEnumerable<double> dataCollection = this._allMeans[unitName].Values;

            // Couleur de la barre
            string hexColor = null;

            // Formattage
            Func<double, string> UnitOnX = null;

            switch (unitName)
            {
                case "Score":
                    UnitOnX = x => string.Format("{0:0.##} pts", x);
                    hexColor = "#CCFA8334";
                    break;
                case "CPS":
                    UnitOnX = x => string.Format("{0:0.00} CPS", x);
                    hexColor = "#CCFCB686";
                    break;
                case "WPM":
                    UnitOnX = x => string.Format("{0:0.##} WPM", x);
                    hexColor = "#CCFDCFAF";
                    break;
                case "Accuracy":
                    UnitOnX = x => string.Format("{0:0.00} %", x * 100);
                    hexColor = "#CCFDE8D8";
                    break;
            }
            Brush fillBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));


            // Ajouter l'axe X correspondant
            this.MainGraph.AxisX.Insert(0, new Axis()
            {
                Foreground = Brushes.White,
                ShowLabels = true,
                IsEnabled = false,
                Name = unitName,
                LabelFormatter = UnitOnX,
                Separator = new LiveCharts.Wpf.Separator()
                {
                    Stroke = fillBrush,
                    StrokeDashArray = new DoubleCollection { 5 }
                }
            });

            RowSeries rowSeries = new RowSeries()
            {
                Title = unitName,
                Name = unitName,
                Values = dataCollection.AsChartValues(),
                Foreground = Brushes.White,
                Fill = fillBrush,
                DataLabels = true,
                RowPadding = 5,
                LabelPoint = data => UnitOnX(data.X),
            };

            // Ajoute une série de données
            SeriesCollection.Add(rowSeries);
            CropStopper();

            // Attention à bien réactualiser l'index des axes pour les prochaines séries
            foreach (RowSeries row in SeriesCollection)
            {
                row.ScalesXAt = this.MainGraph.AxisX.IndexOf(this.MainGraph.AxisX.Where(x => x.Name == row.Name).First());
            }
        }
        public void HideRow(string unitName)
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
            for (int i = 0; i < this.MainGraph.AxisX.Count; i++)
            {
                if (this.MainGraph.AxisX[i].Name == unitName)
                {
                    this.MainGraph.AxisX.Remove(this.MainGraph.AxisX[i]);
                    CropStopper();
                    // Attention à bien réactualiser l'index des axes pour les prochaines séries
                    foreach (RowSeries row in SeriesCollection)
                    {
                        row.ScalesXAt = this.MainGraph.AxisX.IndexOf(this.MainGraph.AxisX.Where(x => x.Name == row.Name).First());
                    }
                }
            }
        }
        private void InitializeTable()
        {
            this._dactylModel = new DactylModel();

            // tableau avec toutes les unités
            _allMeans = this._dactylModel.GetEveryonesMeans();

            this.MainGraph.AxisY[0].Separator.StrokeThickness = 0;

            this.MainGraph.Margin = new Thickness(10);

            SeriesCollection = new SeriesCollection();
            ShowRow("Score");
            this.Score.Style = Application.Current.FindResource("SelectedRoundBtn") as Style;
            LabelsOnY = _allMeans["Score"].Keys.ToArray();

            DataContext = this;
        }
        private void CropStopper()
        {
            // Supprimer le(s) axe(s) déjà existant(s)
            for (int i = 0; i < this.MainGraph.AxisX.Count; i++)
            {
                if (this.MainGraph.AxisX[i].Name == "CropStopper")
                {
                    this.MainGraph.AxisX.RemoveAt(i);
                }
            }

            // Ajouter un axe vide et invisible après tout les autres, pour régler un bug de crop d'axe
            this.MainGraph.AxisX.Insert(0, new Axis()
            {
                Name = "CropStopper",
                Foreground = Brushes.Transparent,
                Separator = new LiveCharts.Wpf.Separator()
                {
                    StrokeThickness = 0,
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;
            string unitName = clickedBtn.Name;
            Debug.WriteLine(unitName + " cliqué");
            
            // Si elle existe, supprimer
            foreach (RowSeries rowSerie in SeriesCollection)
            {
                if (rowSerie.Title == unitName)
                {
                    if (this.SeriesCollection.Count != 1)
                    {
                        Debug.WriteLine("La série existe.");
                        try
                        {
                            HideRow(unitName);
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
                ShowRow(unitName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            clickedBtn.Style = Application.Current.FindResource("SelectedRoundBtn") as Style;
        }
    }
}
