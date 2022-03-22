using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace DactyloTest
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ScoresWindow : Window
    {
        private DactylCtrl _dactylCtrl;
        public ScoresWindow(DactylCtrl ctrl)
        {
            this._dactylCtrl = ctrl;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SortScores();
        }

        private void SortScores()
        {
            List<HighScore> allScores = this._dactylCtrl.GetScores();

            // Ordonner par WPM de manière décroissante
            // List<HighScore> sortedAllScoresallScores = allScores.OrderByDescending(o => o.WPM).ToList();

            // Ordonner par date de manière croissante
            // List<HighScore> sortedAllScoresallScores = allScores.OrderBy(o => o.Date).ToList();

        }
    }
}
