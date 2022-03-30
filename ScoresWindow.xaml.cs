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
using System.Diagnostics;

namespace DactyloTest
{
    /// <summary>
    /// Logique d'interaction pour ScoresWindow.xaml
    /// </summary>
    public partial class ScoresWindow : Window
    {
        private DactylCtrl _dactylCtrl;
        private ScoresCtrl _scoresCtrl;
        private DactylModel _dactylModel;
        private Button[] filterButtons;
        private int _selectedColumn = -1;
        private int _hoveredRow = -1;
        public ScoresWindow(DactylCtrl ctrl, DactylModel dactylModel)
        {
            this._dactylCtrl = ctrl;
            this._dactylModel = dactylModel;
            this._scoresCtrl = new ScoresCtrl(dactylModel);
            InitializeComponent();
        }

        public ScoresWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nickname.Content = this._dactylCtrl.PlayerNickname;
            this.filterButtons = new Button[]
            {
                this.OnlyMyScores,
                this.AllScores
            };
            PrintHeaders();
            UpdateTable();
        }

        private void PrintHeaders()
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
                this.scoreTable.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    SharedSizeGroup = headers[i].Replace(" ", ""),
                    Width = GridLength.Auto
                });

                Button headerBtn = new Button
                {
                    Content = headers[i],
                    Style = Application.Current.FindResource("headersBtn") as Style
                };

                if (!(GetToolTipString(headers[i]) is null))
                {
                    ToolTip headerToolTip = new ToolTip()
                    {
                        Content = GetToolTipString(headers[i]),
                        HasDropShadow = true
                    };
                    headerToolTip.FontFamily = Application.Current.FindResource("Poppins") as FontFamily;
                    headerBtn.ToolTip = headerToolTip;
                }

                headerBtn.Click += HeaderBtn_Click;
                Border border = new Border() { BorderBrush = Brushes.White, BorderThickness = new Thickness(0, 0, 0, 1) };
                border.Child = headerBtn;

                border.SetValue(Grid.ColumnProperty, i);
                this.scoreTable.Children.Add(border);
            }
        }

        public string GetToolTipString(string header)
        {
            string toolTipString;
            switch (header)
            {
                case "Score":
                    toolTipString = "WPM x Précision facteur 10";
                    break;
                case "CPS":
                    toolTipString = "CPS (Chars Per Second)\rCaractères corrects entrés par seconde en moyenne";
                    break;
                case "WPM":
                    toolTipString = "WPM (Words Per Minute)\rUnité de mesure universelle de la vitesse de frappe\rUn mot = 5 caractères en moyenne";
                    break;
                default:
                    toolTipString = null;
                    break;
            }
            return toolTipString;
        }

        public void UpdateTable()
        {
            ClearTable();

            // Obtenir le tableau en fonction des filtres
            List<HighScore> dataList = this._scoresCtrl.GetSortedScores();

            foreach (HighScore highScore in dataList)
            {
                if (this._scoresCtrl.BtnFilterMode == "AllScores" || (this._scoresCtrl.BtnFilterMode == "OnlyMyScores" && highScore.Nickname == this._dactylCtrl.PlayerNickname))
                {
                    AddRow(FormatHighScoreData(highScore));
                }
            }
        }
        private void ClearTable()
        {
            // Faire en sorte que ça n'imprime l'en-tête qu'une seule fois
            if (this.scoreTable.RowDefinitions.Count > 1)
            {
                for (int i = this.scoreTable.Children.Count - 1; i >= 0; i--)
                {
                    if (!(Grid.GetRow(this.scoreTable.Children[i]) == 0))
                    {
                        this.scoreTable.Children.Remove(this.scoreTable.Children[i]);
                    }
                }
                // Garder la première RowDefinition pour l'en-tête
                int rowsToDeleteNumber = this.scoreTable.RowDefinitions.Count;
                for (int i = 1; i < rowsToDeleteNumber; i++)
                {
                    this.scoreTable.RowDefinitions.RemoveAt(1);
                }
            }
        }

        private void AddRow(List<string> formattedHighScores)
        {
            this.scoreTable.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < formattedHighScores.Count; i++)
            {
                // Si la colonne est sélectionnée
                string style;
                bool IsRowHovered = this.scoreTable.RowDefinitions.Count - 1 == this._hoveredRow;
                bool IsColumnSelected = i == this._selectedColumn;
                if (IsRowHovered && IsColumnSelected)
                {
                    style = "hoveredAndSelectedTableDataCell";
                }
                else if (IsRowHovered)
                {
                    style = "hoveredTableDataCell";
                }
                else if (IsColumnSelected)
                // Si la ligne est touchée par le curseur 
                {
                    style = "selectedTableDataCell";
                }
                // Cell normale
                else
                {
                    style = "tableDataCell";
                }

                TextBlock textBlock = new TextBlock
                {
                    Text = formattedHighScores[i],
                    Style = Application.Current.FindResource(style) as Style
                };
                textBlock.SetValue(Grid.ColumnProperty, i);
                textBlock.SetValue(Grid.RowProperty, this.scoreTable.RowDefinitions.Count - 1);
                textBlock.MouseEnter += TextBlock_MouseEnter;
                textBlock.MouseLeave += TextBlock_MouseLeave;
                this.scoreTable.Children.Add(textBlock);
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            this._hoveredRow = -1;
            UpdateTable();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock hoveredBlock = (TextBlock)sender;
            this._hoveredRow = Grid.GetRow(hoveredBlock);
            UpdateTable();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._dactylCtrl.StartGame(true);
        }

        private void HeaderBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;
            string filterMode = "";
            string sortIcon = "";
            string filterName = "";
            this._selectedColumn = Grid.GetColumn((Border)clickedBtn.Parent);

            // Si c'est le même qu'avant
            if (clickedBtn == this._scoresCtrl.PreviousBtn || this._scoresCtrl.HeaderFilterMode == null)
            {
                Debug.WriteLine("C'est le même bouton (ou c'est le premier clic)");
                switch (this._scoresCtrl.HeaderFilterMode)
                {
                    case null:
                        filterMode = "Descending";
                        filterName = clickedBtn.Content.ToString();
                        sortIcon = " ⮟";
                        break;
                    case "Ascending":
                        filterMode = null;
                        filterName = this._scoresCtrl.HeaderFilterName;
                        break;
                    case "Descending":
                        filterMode = "Ascending";
                        filterName = clickedBtn.Content.ToString().Substring(0, clickedBtn.Content.ToString().Length - 2);
                        sortIcon = " ⮝";
                        break;
                }
                if (filterMode == null)
                {
                    clickedBtn.Style = Application.Current.FindResource("headersBtn") as Style;
                    this._selectedColumn = -1;
                }
                else
                {
                    clickedBtn.Style = Application.Current.FindResource("selectedHeadersBtn") as Style;
                }
            }
            // Si c'est pas le même 
            else
            {
                Debug.WriteLine("C'est pas le même bouton qu'avant");
                filterMode = "Descending";
                filterName = clickedBtn.Content.ToString();
                sortIcon = " ⮟";

                // le bouton précédent a son nom originel
                this._scoresCtrl.PreviousBtn.Content = this._scoresCtrl.HeaderFilterName;

                // le bouton actuel est mis en forme
                clickedBtn.Style = Application.Current.FindResource("selectedHeadersBtn") as Style;
                this._scoresCtrl.PreviousBtn.Style = Application.Current.FindResource("headersBtn") as Style;
            }

            clickedBtn.Content = filterName + sortIcon;
            this._scoresCtrl.HeaderFilterMode = filterMode;
            this._scoresCtrl.HeaderFilterName = filterName;
            this._scoresCtrl.PreviousBtn = clickedBtn;
            UpdateTable();
        }
        private void StyleSelectionColumn(Button header, string style)
        {
            this._selectedColumn = Grid.GetColumn((Border)header.Parent);
            // ATTENTION GERER CA ENSUITE DANS UPDATETABLE, PAS ICI .

            Debug.WriteLine("La colonne est la " + this._selectedColumn);
        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;
            foreach (Button button in this.filterButtons)
            {
                button.Style = Application.Current.FindResource("RoundBasicBtn") as Style;
            }
            clickedBtn.Style = Application.Current.FindResource("SelectedRoundBtn") as Style;

            this._scoresCtrl.BtnFilterMode = clickedBtn.Name;
            this.UpdateTable();
        }

        private void QuitScores_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowGraph_Click(object sender, RoutedEventArgs e)
        {
            this.scoreTable.Visibility = Visibility.Hidden;
            this.ScoreGraph.Visibility = Visibility.Visible;
        }
    }
}
