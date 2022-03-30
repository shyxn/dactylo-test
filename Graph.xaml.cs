using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] LabelsOnY { get; set; }
        private DactylModel _dactylModel;

        private Dictionary<string, Dictionary<string, double>> _allMeans;

        public Graph()
        {
            InitializeComponent();
            InitializeTable();
        }
        public void ShowRow(string unitName)
        {
            Dictionary<string, double> dataCollection = this._allMeans[unitName];

            // Formattage
            Func<double, string> UnitOnX = null;
            switch (unitName)
            {
                case "Score":
                    UnitOnX = x => string.Format("{0:0.##}", x);
                    break;
                case "CPS":
                    UnitOnX = x => string.Format("{0:0.00}", x);
                    break;
                case "WPM":
                    UnitOnX = x => string.Format("{0:0.##}", x);
                    break;
                case "Accuracy":
                    UnitOnX = x => string.Format("{0} %", x * 100);
                    break;
            }

            // Ajouter l'axe X correspondant
            this.MainGraph.AxisX.Add(new Axis()
            {
                Foreground = Brushes.White,
                Title = unitName,
                LabelFormatter = UnitOnX
            });
        }
        public void HideRow(string unitName)
        {
            // Supprimer l'axe correspondant
            foreach (Axis axis in this.MainGraph.AxisX)
            {
                if (axis.Title == unitName)
                {
                    this.MainGraph.AxisX.Remove(axis);
                    break;
                }
            }
            // Supprimer la série de données correspondantes
            foreach (RowSeries row in this.SeriesCollection)
            {
                if (row.Title == unitName)
                {
                    this.SeriesCollection.Remove(row);
                }
            }
        }
        private void InitializeTable()
        {
            this._dactylModel = new DactylModel();

            // tableau avec toutes les unités
            _allMeans = this._dactylModel.GetEveryonesMeans();

            IEnumerable<double> scoreValues = _allMeans["Score"].Values;

            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Scores",
                    Values = scoreValues.AsChartValues(),
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FA8334"))
                }
            };

            // adding series will update and animate the chart automatically
            //SeriesCollection.Add(new RowSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> {  }
            //});

            // Nombre de séries existantes
            SeriesCollection.Count();
            // also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            LabelsOnY = _allMeans["Score"].Keys.ToArray();


            DataContext = this;
        }
    }
}
