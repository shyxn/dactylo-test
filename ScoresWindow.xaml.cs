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
    /// Logique d'interaction pour ScoresWindow.xaml
    /// </summary>
    public partial class ScoresWindow : Window
    {
        private DactylCtrl _dactylCtrl;
        public ScoresWindow(DactylCtrl ctrl)
        {
            this._dactylCtrl = ctrl;
            InitializeComponent();
        }
        public ScoresWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] headers = new string[]
            {
                "Pseudonyme",
                "Score",
                "CPS",
                "WPM",
                "Précision",
                "Frappes totales",
                "incorrectes",
                "Temps total",
                "Texte tapé",
                "Date enregistrée"
            };

            this.scoreTable.RowDefinitions.Add(new RowDefinition());
            


            for (int i = 0; i < headers.Length; i++)
            {
                string name = headers[i].Trim();

                this.scoreTable.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    SharedSizeGroup = headers[i].Replace(" ", ""),
                    Width = GridLength.Auto
                });

                TextBlock textBlock = new TextBlock
                {
                    Text = headers[i],
                    FontFamily = new FontFamily("Poppins"),
                    Foreground = Brushes.White,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(15, 0, 15, 5),
                };
                Border border = new Border() { BorderBrush = Brushes.White, BorderThickness = new Thickness(0, 0, 0, 1) };
                border.Child = textBlock;

                border.SetValue(Grid.ColumnProperty, i);
                //textBlock.SetValue(Grid.RowProperty, 0);
                this.scoreTable.Children.Add(border);
            }

            foreach (HighScore highScore in this._dactylCtrl.GetAllScores())
            {
                AddRow(FormatHighScoreData(highScore));
            }
        }

        private void SortScores()
        {
            //List<HighScore> allScores = this._dactylCtrl.GetScores();

            // Ordonner par WPM de manière décroissante
            // List<HighScore> sortedAllScoresallScores = allScores.OrderByDescending(o => o.WPM).ToList();

            // Ordonner par date de manière croissante
            // List<HighScore> sortedAllScoresallScores = allScores.OrderBy(o => o.Date).ToList();

        }
        private void AddRow(List<string> formattedHighScores)
        {
            this.scoreTable.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < formattedHighScores.Count; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = formattedHighScores[i],
                    FontFamily = new FontFamily("Poppins"),
                    Foreground = Brushes.White,
                    FontSize = 14,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(15, 10, 15, 5),
                    MaxWidth = 200,
                    TextTrimming = TextTrimming.CharacterEllipsis
                };
                textBlock.SetValue(Grid.ColumnProperty, i);
                textBlock.SetValue(Grid.RowProperty, this.scoreTable.RowDefinitions.Count - 1);
                this.scoreTable.Children.Add(textBlock);
            }
        }

        private List<string> FormatHighScoreData(HighScore highScore)
        {
            return new List<string>()
            {
                highScore.Nickname,
                highScore.Score.ToString(),
                String.Format("{0:0.00}", highScore.CPS),
                highScore.WPM.ToString(),
                String.Format("{0:0.00} %", highScore.Accuracy * 100),
                highScore.TotalStrokes.ToString(),
                highScore.IncorrectStrokes.ToString(),
                highScore.Time.ToString(@"mm\:ss\:ff"),
                this._dactylCtrl.GetTextFromIndex(highScore.TextIndex),
                highScore.Date.ToString("G")
            };
        }
        
    }
}
