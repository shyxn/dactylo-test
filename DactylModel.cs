using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DactyloTest
{
    public class DactylModel
    {
        private string[] _texts = new string[]
        {
            "Dans toutes les créatures qui ne font pas des autres leurs proies et que de violentes passions n'agitent pas, se manifeste un remarquable désir de compagnie, qui les associe les unes les autres. Ce désir est encore plus manifeste chez l'homme: celui-ci est la créature de l'univers qui a le désir le plus ardent d'une société, et il y est adapté par les avantages les plus nombreux. Nous ne pouvons former aucun désir qui ne se réfère pas à la société.",
            "La responsabilité en effet n'est pas un simple attribut de la subjectivité, comme si celle-ci existait déjà en elle-même, avant la relation éthique. La subjectivité n'est pas un pour soi ; elle est, encore une fois, initialement pour un autre. La proximité d'autrui est présentée (...) comme le fait qu'autrui n'est pas simplement proche de moi dans l'espace, ou proche comme un parent, mais s'approche essentiellement de moi en tant que je me sens - en tant que je suis - responsable de lui.",
            "Il est bon de redire que l'homme ne se forme jamais par l'expérience solitaire. Quand par métier il serait presque toujours seul et aux prises avec la nature inhumaine, toujours est-il qu'il n'a pu grandir seul et que ses premières expériences sont de l'homme et de l'ordre humain, dont il dépend d'abord directement ; l'enfant vit de ce qu'on lui donne, et son travail c'est d'obtenir, non de produire."
        };
        public List<HighScore> HighScores { get; set; } = new List<HighScore>();

        public DactylModel()
        {
            LoadHighScores();
            GetEveryonesMeans();
        }
        public string GetRandomText()
        {
            Random rnd = new Random();
            int tempIndex = rnd.Next(0, this._texts.Length);
            return this._texts[tempIndex];
        }

        // https://stackoverflow.com/a/19456639
        public void SaveHighScore(HighScore highScore)
        {
            HighScores.Add(highScore);
            // ... add more scores if needed

            var serializer = new XmlSerializer(HighScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, HighScores);
            }
        }
        public int GetTextIndex(string text)
        {
            return Array.IndexOf(this._texts, text);
        }
        public void LoadHighScores()
        {
            var serializer = new XmlSerializer(HighScores.GetType(), "HighScores.Scores");
            object obj;
            using (var reader = new StreamReader("highscores.xml"))
            {
                obj = serializer.Deserialize(reader.BaseStream);
            }
            HighScores = (List<HighScore>)obj;
        }
        public string GetTextFromIndex(int index)
        {
            return this._texts[index];
        }

        /// <summary>
        /// Retourne un dictionnaire contenant un dictionnaire par type de données que l'on veut afficher. (Score, CPS, WPM, Précision)
        /// </summary>
        public Dictionary<string, Dictionary<string, double>> GetEveryonesMeans()
        {
            Dictionary<string, Dictionary<string, double>> allMeans = new Dictionary<string, Dictionary<string, double>>();

            List<string> nicknames = this.HighScores.Select(x => x.Nickname).AsParallel().Distinct().ToList();
            string[] units = new[]
            {
                "Score",
                "CPS",
                "WPM",
                "Accuracy"
            };

            foreach (string unit in units)
            {
                Dictionary<string, double> unitMeans = new Dictionary<string, double>();

                Func<HighScore, double> essai = x => x.Score;
                switch (unit)
                {
                    case "Score":
                        essai = x => x.Score;
                        break;
                    case "CPS":
                        essai = x => x.CPS;
                        break;
                    case "WPM":
                        essai = x => x.WPM;
                        break;
                    case "Accuracy":
                        essai = x => x.Accuracy;
                        break;
                }
                foreach (string nickname in nicknames)
                {
                    double mean = this.HighScores.Where(x => x.Nickname == nickname).Average(essai);
                    unitMeans.Add(nickname, mean);
                }
                allMeans.Add(unit, unitMeans.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value));
            }
            
            return allMeans;
        }
    }
}
