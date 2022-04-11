using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace DactyloTest
{
    public class DactylCtrl
    {
        private DactylModel _dactylModel;
        private MainWindow _mainWindow;

        private int _inputIndex;
        private DateTime _gameStartTime;
        public TimeSpan TotalTime { get; set; }
        public int KeyStrokes { get; set; } = 0;
        public int CorrectStrokes { get; set; } = 0;
        public int IncorrectStrokes { get; set; } = 0;
        public string InputText { get; set; }
        public string PlayerNickname { get; set; }
        public int TimeMinutes { get; set; } = 1;
        public int TimeSeconds { get; set; } = 0;
        public GameMode? SelectedGameMode { get; set; } = null;
        public bool IsPlaying { get; set; } = false;
        private TimeSpan _maxTime;

        private string _currentText;
        private System.Windows.Threading.DispatcherTimer _gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer _chronoTickTimer = new System.Windows.Threading.DispatcherTimer();
        public enum GameMode
        {
            Text,
            Time
        }
        public DactylCtrl()
        {

        }
        public DactylCtrl(MainWindow mainW, DactylModel dactylModel)
        {
            this._mainWindow = mainW;
            this._dactylModel = dactylModel;
            this._gameTickTimer.Interval = TimeSpan.FromMilliseconds(100);
            this._chronoTickTimer.Interval = TimeSpan.FromMilliseconds(1);
            this._gameTickTimer.Tick += GameTickTimer_Tick;
            this._chronoTickTimer.Tick += ChronoTickTimer_Tick;
        }
        private void ChronoTickTimer_Tick(object sender, EventArgs e)
        {
            this.CalulatePassedTime();
            this.UpdateTime();
            if (this.IsPlaying)
            {
                int wpm = this.CalculateWPM();
                this._mainWindow.UpdateWPM(wpm.ToString());

                double accuracy = this.CalculateAccuracy();
                this._mainWindow.UpdateAccuracy(String.Format("{0:0.00}", accuracy * 100));
            }
        }
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateTexts();
            this._mainWindow.UpdateStatText();
        }
        private void UpdateTime()
        {
            string timeString = String.Empty;
            switch (this.SelectedGameMode)
            {
                case GameMode.Text:
                    timeString = ": " + this.TotalTime.ToString(@"mm\:ss\.ff");
                    break;
                case GameMode.Time:
                    TimeSpan leftTime = this._maxTime - this.TotalTime;
                    if (leftTime.TotalSeconds <= 0)
                    {
                        this.StopTimers();
                        this.EndGame();
                        return;
                    }
                    timeString = "restant : " + leftTime.ToString(@"mm\:ss\.ff");
                    break;
            }
            this._mainWindow.UpdateTime(timeString);
            this._mainWindow.FocusInput();
        }
        private void CalulatePassedTime()
        {
            this.TotalTime = DateTime.Now - this._gameStartTime;
        }

        /// <summary>
        /// Démarre une partie.
        /// </summary>
        /// <param name="restart">false pour rechercher un nouveau texte, sinon true.</param>
        public void StartGame(bool restart = false)
        {
            this.StopTimers();
            this._inputIndex = 0;
            KeyStrokes = 0;
            CorrectStrokes = 0;
            IncorrectStrokes = 0;
            this._mainWindow.InputTextBox.Text = "";
            if (this.SelectedGameMode == GameMode.Time)
            {
                this._maxTime = new TimeSpan(0, this.TimeMinutes, this.TimeSeconds);
            }
            if (!restart || this._currentText is null)
            {
                this.GetNewText();
            }
            this._mainWindow.ShowCorrectChar();
            this.UpdateTexts();
            this._mainWindow.FocusInput();
            this.TotalTime = TimeSpan.Zero;
            this.IsPlaying = true;
        }
        public void GetNewText()
        {
            string newText;
            do
            {
                newText = this._dactylModel.GetRandomText();

            } while (this._currentText == newText);
            this._currentText = newText;

            this.UpdateTexts();
        }
        public void StartTimers()
        {
            this._gameTickTimer.IsEnabled = true;
            this._chronoTickTimer.IsEnabled = true;
            this._gameStartTime = DateTime.Now;
        }
        public void StopTimers()
        {
            this._gameTickTimer.Stop();
            this._chronoTickTimer.Stop();
        }
        public void SetNickname(string nickname)
        {
            this.PlayerNickname = nickname.Trim();
        }
        public void CheckChar(char newChar)
        {
            // Déclenche les timers lors de la première frappe
            if (this._chronoTickTimer.IsEnabled != true)
            {
                StartTimers();
                this._mainWindow.ShowAllStats();
            }

            this.KeyStrokes++;
            // Si la lettre est correcte
            if (newChar == this._currentText[this._inputIndex])
            {
                this.CorrectStrokes++;
                Debug.WriteLine("Bon caractère");
                this._inputIndex++;
                this._mainWindow.ShowCorrectChar();

                // Si c'est la fin du jeu
                if (this._inputIndex == this._currentText.Length)
                    this.EndGame();
                else
                    this.UpdateTexts();
            }
            else
            {
                this.IncorrectStrokes++;
                Debug.WriteLine("Mauvais caractère");
                // Afficher le caractère mauvais en rouge
                this._mainWindow.ShowIncorrectChar();
            }
        }
        private void EndGame()
        {
            StopTimers();
            this.IsPlaying = false;
            this._mainWindow.HideAllStats();
            // Gérer le score
            var score = new HighScore()
            {
                Accuracy = this.CalculateAccuracy(),
                Nickname = this.PlayerNickname,
                CorrectStrokes = this.CorrectStrokes,
                IncorrectStrokes = this.IncorrectStrokes,
                Time = this.TotalTime,
                TotalStrokes = this.KeyStrokes,
                WPM = this.CalculateWPM(),
                TextIndex = this._dactylModel.GetTextIndex(this._currentText),
                Date = DateTime.Now
            };
            score.CalculateScore();
            score.CalculateSpeed();
            string noSaveMessage = "";
            if (!string.IsNullOrEmpty(this.PlayerNickname))
            {
                this._dactylModel.SaveHighScore(score);
            }
            else
            {
                noSaveMessage = "\r\rVotre score n'a pas été enregistré.";
            }

            this._mainWindow.ShowEndMessage("Bravo " + score.Nickname + ", vous avez terminé le test en " + score.Time.ToString(@"mm\:ss\.ff") + ".\rVitesse : " + score.WPM + " MPM\rPrécision : " + String.Format("{0:0.00}", score.Accuracy * 100) + " %\rFrappes totales : " + score.TotalStrokes + "\rFrappes incorrectes : " + score.IncorrectStrokes + "\rFrappes correctes : " + score.CorrectStrokes + "\rVitesse : " + String.Format("{0:0.00}", score.CPS) + " CPS\rVotre score est de : " + score.Score + noSaveMessage);
            this.StartGame();
        }

        /// <summary>
        /// Découpe les bouts de phrase à afficher (gauche, milieu et droite) et les envoie à la vue
        /// </summary>
        private void UpdateTexts()
        {
            // Char à entrer pour l'utilisateur
            string midChar = this._currentText[this._inputIndex].ToString();

            // Partie gauche du texte (passée)
            // à crop pour qu'elle ne dépasse pas le conteneur
            string leftString = this._inputIndex < 15
                ? this._currentText.Substring(0, this._inputIndex)
                : "…" + this._currentText.Substring(this._inputIndex - 15, 15);

            // Partie droite du texte (future)
            string rightString = this._currentText.Substring(this._inputIndex + 1);

            this._mainWindow.UpdateMainText(leftString, midChar, rightString);
        }

        public int CalculateWPM()
        {
            return (int)(this.CorrectStrokes / 5 / this.TotalTime.TotalSeconds * 60);
        }
        public double CalculateAccuracy()
        {
            return (double)this.CorrectStrokes / (double)this.KeyStrokes;
        }
        public List<HighScore> GetAllScores()
        {
            return _dactylModel.HighScores;
        }
        public string GetTextFromIndex(int index)
        {
            return this._dactylModel.GetTextFromIndex(index);
        }
    }
}
