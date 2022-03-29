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
        public Func<double, string> UnitOnX { get; set; }
        private DactylModel _dactylModel;
        public Graph(DactylModel dactylModel)
        {
            this._dactylModel = dactylModel;

            InitializeComponent();

            // tableau avec toutes les unités

            Dictionary<string, Dictionary<string, double>> allMeans = this._dactylModel.GetEveryonesMeans();

            IEnumerable<double> scoreValues = allMeans["Score"].Values;

            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Scores",
                    Values = scoreValues.AsChartValues(),
                    
                }
            };

            // adding series will update and animate the chart automatically
            //SeriesCollection.Add(new RowSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> {  }
            //});

            // also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            LabelsOnY = allMeans["Score"].Keys.ToArray();
            UnitOnX = value => value.ToString("N");

            DataContext = this;
        }
    }
}
