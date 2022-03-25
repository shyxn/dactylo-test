﻿using System;
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
        private Button[] filterButtons;
        public ScoresWindow(DactylCtrl ctrl, DactylModel dactylModel)
        {
            this._dactylCtrl = ctrl;
            this._scoresCtrl = new ScoresCtrl(dactylModel);
            InitializeComponent();
        }

        public ScoresWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                headerBtn.Click += HeaderBtn_Click;
                Border border = new Border() { BorderBrush = Brushes.White, BorderThickness = new Thickness(0, 0, 0, 1) };
                border.Child = headerBtn;

                border.SetValue(Grid.ColumnProperty, i);
                this.scoreTable.Children.Add(border);
            }
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
                TextBlock textBlock = new TextBlock
                {
                    Text = formattedHighScores[i],
                    Style = Application.Current.FindResource("tableDataCell") as Style
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
                clickedBtn.Style = filterMode == null
                    ? Application.Current.FindResource("headersBtn") as Style
                    : Application.Current.FindResource("selectedHeadersBtn") as Style;
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
            if (style == "selected")
            {
                header.Style = Application.Current.FindResource("selectedHeadersBtn") as Style;
                // POUR TOUTES LES COLONNES QUI FONT PARTIE DU MÊME INDEX COLUMNPROPRETY DE GRID
                // appliquer le style correspondant

                for (int i = 0 ; i < this.scoreTable.Children.Count - 1; i--)
                {
                    if (!(Grid.GetColumn(this.scoreTable.Children[i].) == 0))
                    {
                        this.scoreTable.Children.Remove(this.scoreTable.Children[i]);
                    }
                }
            }
            if (style == "normal")
            {
                header.Style = Application.Current.FindResource("headersBtn") as Style;
                // POUR TOUTES LES COLONNES QUI FONT PARTIE DU MÊME INDEX COLUMNPROPRETY DE GRID
                // appliquer le style correspondant
                foreach (var item in collection)
                {

                }
            }
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
    }
}
