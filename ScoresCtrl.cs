using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DactyloTest
{
    public class ScoresCtrl
    {
        private DactylModel _dactylModel;
        public string filterMode = "All"; // ou "OnlyCurrentNickname

        public ScoresCtrl(DactylModel dactylModel)
        {
            this._dactylModel = dactylModel;
        }
        private List<HighScore> SortScores(string filter, string mode = "Descending")
        {
            List<HighScore> allScores = this._dactylModel._highScores;
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
            Func<HighScore, object> HighScoreProprety = null;
            switch (filter)
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
            }

            if (mode == "Descending")
                return allScores.OrderByDescending(HighScoreProprety).ToList();
            else if (mode == "Ascending")
                return allScores.OrderBy(HighScoreProprety).ToList();
            else
                return null;
        }
    }
}
