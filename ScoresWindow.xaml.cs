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
                "Accuracy",
                "TotalStrokes",
                "CorrectStrokes",
                "Time",
                "Texte",
                "Date"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                this.scoreTable.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    SharedSizeGroup = headers[i],
                    Width = new GridLength(1, GridUnitType.Star)
                });

                TextBlock textBlock = new TextBlock
                {
                    Text = headers[i],
                    FontFamily = new FontFamily("Poppins"),
                    FontSize = 13,
                };
                textBlock.SetValue(Grid.ColumnProperty, i);
                this.scoreTable.Children.Add(textBlock);
            }
            
            //foreach (HighScore highScore in this._dactylCtrl.GetAllScores())
            //{
            //    AddRow(FormatHighScoreData(highScore));
            //}
            //SortScores();
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
            TableRow newTableRow = new TableRow();

            foreach (string data in formattedHighScores)
            {
                Paragraph paragraph = new Paragraph();
                TableCell tableCell = new TableCell();
                paragraph.Inlines.Add(data);
                tableCell.Blocks.Add(paragraph);
                tableCell.BorderThickness = new Thickness(0, 1, 0, 0);
                tableCell.BorderBrush = Brushes.Black;
                newTableRow.Cells.Add(tableCell);
            }
            this.Records.Rows.Add(newTableRow);
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
                highScore.CorrectStrokes.ToString(),
                highScore.Time.ToString(@"mm\:ss\:ff"),
                this._dactylCtrl.GetTextFromIndex(highScore.TextIndex).Substring(0, 10) + "…",
                highScore.Date.ToString("G")
            };
        }
    }
}
