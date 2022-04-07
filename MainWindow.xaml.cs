using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using Microsoft.VisualBasic;
using System;

namespace DactyloTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///  Version actuelle du programme
        /// </summary>
        private string _version = "1.0.0";
        /// <summary>
        /// Contrôleur 
        /// </summary>
        private DactylCtrl _dactylCtrl;
        private DactylModel _dactylModel;
        public MainWindow()
        {
            // Définir le pattern MVC
            this._dactylModel = new DactylModel();
            this._dactylCtrl = new DactylCtrl(this, this._dactylModel);

            this.InitializeComponent();

            this.Title = "DactyloTest v" + this._version;
            this.title.Text = "DactyloTest (P_APPRO) v" + this._version + " - Morgane Lebre";           
        }
        /// <summary>
        /// Affiche la fenêtre de sélection du pseudonyme
        /// </summary>
        public void ShowNickNameInput()
        {
            this.nicknameInputCanvas.Visibility = Visibility.Visible;
            this.inputNickname.Focus();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowNickNameInput();
            Debug.WriteLine("La partie commence.");
            this.InputTextBox.TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
            //this._dactylCtrl.StartGame();
            this.inputNickname.Focus();
        }
        
        /// <summary>
        /// Mettre le focus sur la zone à taper pour détecter les caractères entrés.
        /// </summary>
        public void FocusInput()
        {
            this.InputTextBox.Focus();
        }
        /// <summary>
        /// Actualiser le label du temps
        /// </summary>
        /// <param name="time"></param>
        public void UpdateTime(string time)
        {
            this.totalTime.Text = "Temps : " + time;
        }
        public void UpdateMainText(string leftString, string midChar, string rightString)
        {
            Debug.WriteLine("Actualisation main text : " + leftString + midChar + rightString);
            this.leftSideText.Text = leftString;
            this.midSideText.Text = midChar;
            this.rightSideText.Text = rightString;

            this.leftChars.Text = "< " + rightString.Length.ToString();

            this.totalStrokes.Text = "Frappes totales : " + this._dactylCtrl.KeyStrokes.ToString();
            this.totalIncorrectStrokes.Text = "Frappes incorrectes : " + this._dactylCtrl.IncorrectStrokes.ToString();
            this.totalCorrectStrokes.Text = "Frappes correctes : " + this._dactylCtrl.CorrectStrokes.ToString();
        }
        public void UpdateWPM(string wpm)
        {
            this.currentWPM.Text = "Mots/minutes : " + wpm + " MPM";
        }

        public void UpdateAccuracy(string accuracy)
        {
            this.currentAccuracy.Text = "Accuracy : " + accuracy + " %";
        }

        public void ClearAllTexts()
        {
            Debug.WriteLine("Le contenu a été nettoyé.");
            this.InputTextBox.Text = "";
        }

        public void ShowEndMessage(string message)
        {
            MessageBox.Show(message, "DactyloTest");
        }

        public void ShowCorrectChar()
        {
            this.midSideText.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45607C"));
        }

        public void ShowIncorrectChar()
        {
            this.midSideText.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#93032E"));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._dactylCtrl.InputText != null && this._dactylCtrl.InputText.Length > this.InputTextBox.Text.Length){ return; }
            if (this.InputTextBox.Text.Length > 0)
            {
                char newChar = this.InputTextBox.Text[this.InputTextBox.Text.Length - 1];
                Debug.WriteLine("Un caractère a été entré : " + newChar);
                this._dactylCtrl.CheckChar(newChar);
            }
        }

        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.InputText = this.InputTextBox.Text;
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StartGame(true);
        }

        private void anotherTextBtn_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StartGame();
        }

        private void confirmNicknameBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.inputNickname.Text) && MessageBox.Show("Si vous ne renseignez aucun pseudo, votre score ne sera pas enregistré. Continuer ?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            this.nickname.Content = this.inputNickname.Text;
            this._dactylCtrl.SetNickname(this.inputNickname.Text);
            this.nicknameInputCanvas.Visibility = Visibility.Collapsed;
            if (this._dactylCtrl.SelectedGameMode is null)
            {
                this.selectModeCanvas.Visibility = Visibility.Visible;
            }

            // Dans le cas où on est en plein jeu...
            if (this._dactylCtrl.IsPlaying)
            {
                this._dactylCtrl.StartGame(true);
            }
        }

        private void confirmModeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.selectModeCanvas.Visibility = Visibility.Collapsed;
            this._dactylCtrl.StartGame();
        }

        private void inputNickname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.confirmNicknameBtn_Click(sender, e);
            }
        }

        private void ChangeNickname_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StopTimers();
            this.ShowNickNameInput();
        }

        private void scoreBtn_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StopTimers();
            ScoresWindow scoreWindow = new ScoresWindow(this._dactylCtrl, this._dactylModel);
            scoreWindow.Show();
            scoreWindow.Closed += this.ScoreWindow_Closed;
        }

        private void ScoreWindow_Closed(object sender, EventArgs e)
        {
            if (this.nicknameInputCanvas.Visibility == Visibility.Visible)
            {
                this.inputNickname.Focus();
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CADAE8"));
            //((Border)panel.Parent).BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F96D10"));
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = Brushes.Transparent;
            //((Border)panel.Parent).BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212F3D"));
        }

        // Lorsqu'un des deux modes est sélectionné.
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Brush unselectedBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212F3D"));
            Brush selectedBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F97924"));

            this.timeModePanel.BorderBrush = unselectedBorderBrush;
            this.textModePanel.BorderBrush = unselectedBorderBrush;
            border.BorderBrush = selectedBorderBrush;

            switch (border.Name)
            {
                case "timeModePanel":
                    this._dactylCtrl.SelectedGameMode = DactylCtrl.GameMode.Time;
                    this.timeCanvas.Visibility = Visibility.Visible;
                    break;
                case "textModePanel":
                    this._dactylCtrl.SelectedGameMode = DactylCtrl.GameMode.Text;
                    this.timeCanvas.Visibility = Visibility.Collapsed;
                    break;
            }

            // Activer le bouton pour confirmer
            this.confirmModeBtn.IsEnabled = true;
        }

        private void changeTime_Click(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name;
            switch (btnName)
            {
                case "UpMinBtn":
                    this._dactylCtrl.TimeMinutes++;
                    break;
                case "UpSecBtn":
                    this._dactylCtrl.TimeSeconds += 15;
                    if (this._dactylCtrl.TimeSeconds >= 60)
                    {
                        this._dactylCtrl.TimeMinutes++;
                        this._dactylCtrl.TimeSeconds = 0;
                    }
                    break;
                case "DownMinBtn":
                    if (this._dactylCtrl.TimeMinutes > 1)
                    {
                        this._dactylCtrl.TimeMinutes--;
                    }
                    break;
                case "DownSecBtn":
                    if (!(this._dactylCtrl.TimeSeconds == 0 && this._dactylCtrl.TimeMinutes == 1))
                    {
                        this._dactylCtrl.TimeSeconds -= 15;
                        if (this._dactylCtrl.TimeSeconds < 0)
                        {
                            this._dactylCtrl.TimeSeconds = 45;
                            this._dactylCtrl.TimeMinutes--;
                        }
                    }
                    break;
            }

            // Update minuteslbl et secondslbl
            this.MinutesLbl.Content = String.Format("{0:00}", this._dactylCtrl.TimeMinutes);
            this.SecondsLbl.Content = String.Format("{0:00}", this._dactylCtrl.TimeSeconds);
        }
    }
}
