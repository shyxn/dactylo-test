using System;
using System.Collections.Generic;
using System.Text;

namespace DactyloTest
{
    [Serializable()]
    public class HighScore
    {
        /// <summary>
        /// Score calculé en fonction des mots par minute et de la précision des frappes.
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Vitesse exprimée en caractère / seconde.
        /// </summary>
        public double CPS { get; set; }
        /// <summary>
        /// Vitesse exprimée en mots / minutes (1 mot = 5 caractères)
        /// </summary>
        public int WPM { get; set; }

        public string Nickname { get; set; }
        /// <summary>
        /// Pourcentages de frappes correctes par rapport au nombre total de frappes.
        /// </summary>
        public double Accuracy { get; set; }
        public int TotalStrokes { get; set; }
        public int CorrectStrokes { get; set; }
        public int IncorrectStrokes { get; set; }
        /// <summary>
        /// Temps de complétion total d'un test.
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Index répertoriant le texte joué dans DactylModel._texts
        /// </summary>
        public int TextIndex { get; set; }
        public DateTime Date { get; set; }

        public HighScore()
        {
            // Peut-être qu'elles s'exécutent trop tôt par rapport à l'initialisation des propriétés à l'instanciation
            CalculateSpeed();
            CalculateScore();
        }

        /// <summary>
        /// Calcule la vitesse en caractères par minute et l'assigne à la propriété.
        /// </summary>
        public void CalculateSpeed()
        {
            this.CPS = CorrectStrokes / Time.TotalSeconds;
        }

        /// <summary>
        /// Calcule le score et l'assigne à la propriété.
        /// </summary>
        public void CalculateScore()
        {
            Score = Convert.ToInt32(WPM * Accuracy * 10);
        }
    }
}
