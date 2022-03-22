using System;
using System.Collections.Generic;
using System.IO;
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
        public List<HighScore> _highScores = new List<HighScore>();


        public DactylModel()
        {
            LoadHighScores();
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
            _highScores.Add(highScore);
            // ... add more scores if needed

            var serializer = new XmlSerializer(_highScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, _highScores);
            }
        }
        public int GetTextIndex(string text)
        {
            return Array.IndexOf(this._texts, text);
        }
        public void LoadHighScores()
        {
            var serializer = new XmlSerializer(_highScores.GetType(), "HighScores.Scores");
            object obj;
            using (var reader = new StreamReader("highscores.xml"))
            {
                obj = serializer.Deserialize(reader.BaseStream);
            }
            _highScores = (List<HighScore>)obj;
        }
    }
}
