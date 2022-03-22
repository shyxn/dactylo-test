﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace DactyloTest
{
    public class DactylCtrl
    {
        private DactylModel _dactylModel;
        private MainWindow _mainWindow;
        private string _playerNickname;
        
        private int _inputIndex;
        private DateTime _gameStartTime;
        private TimeSpan _totalTime;
        public int KeyStrokes { get; set; } = 0;
        public int CorrectStrokes { get; set; } = 0;
        public int IncorrectStrokes { get; set; } = 0;
        public string InputText { get; set; }

        private string _currentText;
        private System.Windows.Threading.DispatcherTimer _gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        private System.Windows.Threading.DispatcherTimer _chronoTickTimer = new System.Windows.Threading.DispatcherTimer();

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
            this.CalculateWPM();
            this.CalculateAccuracy();
        }
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateTexts();
        }
        private void UpdateTime()
        {   
            string timeString = this._totalTime.ToString(@"mm\:ss\.ff");
            this._mainWindow.UpdateTime(timeString);
            this._mainWindow.FocusInput();
        }
        private void CalulatePassedTime()
        {
            this._totalTime = DateTime.Now - this._gameStartTime;
        }

        public void StartGame(bool restart = false)
        {
            this.StopTimers();
            this._inputIndex = 0;
            KeyStrokes = 0;
            CorrectStrokes = 0;
            IncorrectStrokes = 0;
            this._mainWindow.ClearAllTexts();
            if (!restart)
            {
                this.GetNewText();
            }
            this.UpdateTexts();
            this._mainWindow.FocusInput();
            this._totalTime = TimeSpan.Zero;
            // StartTimers();
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
            this._playerNickname = nickname;
        }
        public void CheckChar(char newChar)
        {
            if (this._chronoTickTimer.IsEnabled != true)
            {
                StartTimers();
            }

            this.KeyStrokes++;
            if (newChar == this._currentText[this._inputIndex])
            {
                this.CorrectStrokes++;
                Debug.WriteLine("Bon caractère");
                // Si la lettre est correcte
                this._inputIndex++;
                this._mainWindow.ShowCorrectChar();

                // Si c'est la fin du jeu
                if (this._inputIndex == this._currentText.Length)
                {
                    this.EndGame();
                }
                else
                {
                    this.UpdateTexts();
                }
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
            
            // Gérer le score
            var score = new HighScore()
            {
                Accuracy = this.CalculateAccuracy(),
                Nickname = this._playerNickname,
                CorrectStrokes = this.CorrectStrokes,
                IncorrectStrokes = this.IncorrectStrokes,
                Time = this._totalTime,
                TotalStrokes = this.KeyStrokes,
                WPM = this.CalculateWPM(),
                TextIndex = this._dactylModel.GetTextIndex(this._currentText)
            };
            score.CalculateScore();
            score.CalculateSpeed();
            if (string.IsNullOrEmpty(this._playerNickname))
            {
                this._dactylModel.SaveHighScore(score);
            }

            this._mainWindow.ShowEndMessage("Bravo " + score.Nickname + ", vous avez terminé le test en " + score.Time.ToString(@"mm\:ss\.ff") + ".\rVitesse : " + score.WPM + " MPM\rPrécision : " + String.Format("{0:0.00}", score.Accuracy * 100) + " %\rFrappes totales : " + score.TotalStrokes + "\rFrappes incorrectes : " + score.IncorrectStrokes + "\rFrappes correctes : " + score.CorrectStrokes + "\rVitesse : " + String.Format("{0:0.00}", score.CPS) + " CPS\rVotre score est de : " + score.Score);
            this.StartGame();
        }

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

        private int CalculateWPM()
        {
            double secondsPassed = this._totalTime.TotalSeconds;
            int WPM = (int)(this.CorrectStrokes / 5 / secondsPassed * 60);
            this._mainWindow.UpdateWPM(WPM.ToString());
            return WPM;
        }

        private double CalculateAccuracy()
        {
            double percent = (double)this.CorrectStrokes / (double)this.KeyStrokes;
            this._mainWindow.UpdateAccuracy(String.Format("{0:0.00}", percent * 100));
            return percent;
        }
    }
}