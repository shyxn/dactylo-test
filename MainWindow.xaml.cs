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
        private string _version = "0.0.2";
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

            InitializeComponent();

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
            ShowNickNameInput();
            Debug.WriteLine("La partie commence.");
            this.InputTextBox.TextChanged += new TextChangedEventHandler(TextBox_TextChanged);
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
            this.nicknameInputCanvas.Visibility = Visibility.Hidden;
            this._dactylCtrl.StartGame();
        }

        private void inputNickname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                confirmNicknameBtn_Click(sender, e);
            }
        }

        private void ChangeNickname_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StopTimers();
            this.nicknameInputCanvas.Visibility = Visibility.Visible;
            this.inputNickname.Focus();
        }

        private void scoreBtn_Click(object sender, RoutedEventArgs e)
        {
            this._dactylCtrl.StopTimers();
            new ScoresWindow(this._dactylCtrl, this._dactylModel).Show();
        }
    }
}
