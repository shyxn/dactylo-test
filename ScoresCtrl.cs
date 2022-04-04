using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DactyloTest
{
    public class ScoresCtrl
    {
        private DactylModel _dactylModel;

        // TODO : À remplacer par un enum
        public string BtnFilterMode { get; set; } = "AllScores"; // ou "OnlyMyScores"
        public string HeaderFilterMode { get; set; } = null; // Ou "Ascending" ou "Descending"
        public string HeaderFilterName { get; set; } = "Score";
        public Button PreviousBtn { get; set; }
        public ScoresCtrl(DactylModel dactylModel)
        {
            this._dactylModel = dactylModel;
        }
        public List<HighScore> GetSortedScores()
        {
            List<HighScore> allScores = this._dactylModel.HighScores;
            //string[] headers = new string[]
            //{
            //    "Pseudonyme",
            //    "Score",
            //    "CPS",
            //    "WPM",
            //    "Précision",
            //    "Frappes totales",
            //    "incorrectes",
            //    "Temps total",
            //    "Texte tapé",
            //    "Date enregistrée"
            //};
            Func<HighScore, object> HighScoreProprety = null;
            switch (this.HeaderFilterName)
            {
                case "Pseudonyme":
                    HighScoreProprety = o => o.Nickname;
                    break;
                case "Score":
                    HighScoreProprety = o => o.Score;
                    break;
                case "CPS":
                    HighScoreProprety = o => o.CPS;
                    break;
                case "WPM":
                    HighScoreProprety = o => o.WPM;
                    break;
                case "Précision":
                    HighScoreProprety = o => o.Accuracy;
                    break;
                case "Frappes totales":
                    HighScoreProprety = o => o.TotalStrokes;
                    break;
                case "incorrectes":
                    HighScoreProprety = o => o.IncorrectStrokes;
                    break;
                case "Temps total":
                    HighScoreProprety = o => o.Time;
                    break;
                case "Texte tapé":
                    HighScoreProprety = o => o.TextIndex;
                    break;
                case "Date enregistrée":
                    HighScoreProprety = o => o.Date;
                    break;
                default:
                    return allScores;
            }

            if (this.HeaderFilterMode == "Descending")
                return allScores.OrderByDescending(HighScoreProprety).ToList();
            else if (this.HeaderFilterMode == "Ascending")
                return allScores.OrderBy(HighScoreProprety).ToList();
            else
                return allScores;
        }
    }
}
