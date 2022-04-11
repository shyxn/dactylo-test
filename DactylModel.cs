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
            "Le monde est ma représentation. - Cette proposition est une vérité pour tout être vivant et pensant, bien que, chez l'homme seul, elle arrive à se transformer en connaissance abstraite et réfléchie. Dès qu'il est capable de l'amener à cet état, on peut dire que l'esprit philosophique est né en lui. Il possède alors l'entière certitude de ne connaître ni un soleil ni une terre, mais seulement un oeil qui voit ce soleil, une main qui touche cette terre ; il sait, en un mot, que le monde dont il est entouré n'existe que comme représentation, dans son rapport avec un être percevant, qui est l'homme lui-même. S'il est une vérité qu'on puisse affirmer a priori, c'est bien celle-là ; car elle exprime le mode de toute expérience possible et imaginable, concept de beaucoup plus général que ceux même de temps, d'espace et de causalité qui l'impliquent. Chacun de ces concepts, en effet, dans lesquels nous avons reconnu des formes diverses du principe de raison, n'est applicable qu'à un ordre déterminé de représentations ; la distinction du sujet et de l'objet, au contraire, est le mode commun à toutes, le seul sous lequel on puisse concevoir une représentation quelconque, abstraite ou intuitive, rationnelle ou empirique. Aucune vérité n'est donc plus certaine, plus absolue, plus évidente que celle-ci : tout ce qui existe existe pour la pensée, c'est-à-dire, l'univers entier n'est objet qu'à l'égard d'un sujet, perception que par rapport à un esprit percevant, en un mot, il est pure représentation. Cette loi s'applique naturellement à tout le présent, à tout le passé et à tout l'avenir, à ce qui est loin comme à ce qui est près de nous ; car elle est vraie du temps et de l'espace eux-mêmes, grâce auxquels les représentations particulières se distinguent les unes des autres. Tout ce que le monde renferme ou peut renfermer est dans cette dépendance nécessaire vis-à-vis du sujet et n'existe que pour le sujet. Le monde est donc représentation. Cette vérité est d'ailleurs loin d'être neuve. Elle fait déjà le fond des considérations sceptiques d'où procède la philosophie de Descartes. Mais ce fut Berkeley qui le premier la formula d'une manière catégorique ; par là il a rendu à la philosophie un immortel service, encore que le reste de ses doctrines ne mérite guère de vivre. Le grand tort de Kant, comme je l'expose dans l'Appendice qui lui est consacré, a été de méconnaître ce principe fondamental. En revanche, cette importante vérité a été de bonne heure admise par les sages de l'Inde, puisqu'elle apparaît comme la base même de la philosophie védanta, attribuée à Vyâsa. Nous avons sur ce point le témoignage de W. Jones, dans sa dernière dissertation ayant pour objet la philosophie asiatique : \"Le dogme essentiel de l'école védanta consistait, non à nier l'existence de la matière, c'est-à-dire de la solidité, de l'impénétrabilité, de l'étendue (négation qui, en effet, serait absurde), mais seulement à réformer sur ce point l'opinion vulgaire, et à soutenir que cette matière n'a pas une réalité indépendante de la perception de l'esprit, existence et perceptibilité étant deux termes équivalents\". Cette simple indication montre suffisamment dans le védantisme le réalisme empirique associé à l'idéalisme transcendantal. C'est à cet unique point de vue et comme pure représentation que le monde sera étudié dans ce premier livre. Une telle conception, absolument vraie d'ailleurs en elle-même, est cependant exclusive et résulte d'une abstraction volontairement opérée par l'esprit ; la meilleure preuve en est dans la répugnance naturelle des hommes à admettre que le monde ne soit qu'une simple représentation, idée néanmoins incontestable. Mais cette vue, qui ne porte que sur une face des choses, sera complétée dans le livre suivant par une autre vérité, moins évidente, il faut l'avouer, que la première ; la seconde demande, en effet, pour être comprise, une recherche plus approfondie, un plus grand effort d'abstraction, enfin une dissociation des éléments hétérogènes accompagnée d'une synthèse des principes semblables. Cette austère vérité, bien propre à faire réfléchir l'homme, sinon à le faire trembler, voici comment il peut et doit l'énoncer à côté de l'autre : \" Le monde est ma volonté. \" En attendant, il nous faut, dans ce premier livre, envisager le monde sous un seul de ses aspects, celui qui sert de point de départ à notre théorie, c'est-à-dire la propriété qu'il possède d'être pensé. Nous devons, dès lors, considérer tous les objets présents, y compris notre propre corps (ceci sera développé plus loin), comme autant de représentations et ne jamais les appeler d'un autre nom. La seule chose dont il soit fait abstraction ici (chacun, j'espère, s'en pourra convaincre par la suite), c'est uniquement la volonté, qui constitue l'autre côté du monde : à un premier point de vue, en effet, ce monde n'existe absolument que comme représentation ; à un autre point de vue, il n'existe que comme volonté. Une réalité qui ne peut se ramener ni au premier ni au second de ces éléments, qui serait un objet en soi (et c'est malheureusement la déplorable transformation qu'a subie, entre les mains même de Kant, sa chose en soi), cette prétendue réalité, dis-je, est une pure chimère, un feu follet propre seulement à égarer la philosophie qui lui fait accueil.",
            "Dans toutes les créatures qui ne font pas des autres leurs proies et que de violentes passions n'agitent pas, se manifeste un remarquable désir de compagnie, qui les associe les unes les autres. Ce désir est encore plus manifeste chez l'homme: celui-ci est la créature de l'univers qui a le désir le plus ardent d'une société, et il y est adapté par les avantages les plus nombreux. Nous ne pouvons former aucun désir qui ne se réfère pas à la société.",
            "La responsabilité en effet n'est pas un simple attribut de la subjectivité, comme si celle-ci existait déjà en elle-même, avant la relation éthique. La subjectivité n'est pas un pour soi ; elle est, encore une fois, initialement pour un autre. La proximité d'autrui est présentée (...) comme le fait qu'autrui n'est pas simplement proche de moi dans l'espace, ou proche comme un parent, mais s'approche essentiellement de moi en tant que je me sens - en tant que je suis - responsable de lui.",
            "Il est bon de redire que l'homme ne se forme jamais par l'expérience solitaire. Quand par métier il serait presque toujours seul et aux prises avec la nature inhumaine, toujours est-il qu'il n'a pu grandir seul et que ses premières expériences sont de l'homme et de l'ordre humain, dont il dépend d'abord directement ; l'enfant vit de ce qu'on lui donne, et son travail c'est d'obtenir, non de produire.",
            "Si vous deviez chercher comment préparer le thé dans le premier livre de cuisine qui vous tomberait sous la main, il y a fort à partier que vous feriez chou blanc. Au mieux, vous trouveriez quelques lignes d'instructions sommaires qui ne mentionneront aucunement les points les plus importants. Cela est assez curieux, non seulement parce que le thé a participé au développement de la civilisation dans notre pays, ainsi qu'en Irlande, en Australie et en Nouvelle-Zélande, mais aussi parce que la meilleure façon de le préparer peut donner à de violentes disputes. Quand je me réfère à ma propre recette pour une tasse de thé parfaite, j'y trouve pas moins de onze points essentiels. Peut-être que deux d'entre eux font l'unanimité mais au moins quatre autres sont sujets à controverse. Voici mes propres onzes règles - chacune d'entre elles étant à mes yeux une règle d'or : Premièrement, il faut utiliser du thé d'Inde ou de Ceylan. Le thé de Chine a des vertus qu'il ne faut pas mépriser de nos jours - il est bon marché et on peut le boire sans lait - mais il n'apporte que peu de stimulation. On ne se sent pas plus sage, brave ou optimiste après en avoir bu. Quiconque a déjà utilisé la phrase réconfortante \"une bonne tasse de thé\" faisait invariablement référence au thé indien. Deuxièmement, le thé doit être préparé en petites quantités, c'est-à-dire en théière. Le thé préparé en samovar n'a jamais de goût et le thé de l'armée, préparé en chaudron, a le goût de graisse et de lait de chaux. La théière doit être en porcelaine ou en faïence. Les théières en argent ou en britannium produisent un thé inférieur et les théières en émail sont encore pires ; toutefois, de façon assez étonnante, les théières en étain, que l'on ne trouve plus guère de nos jours, ne sont pas si terribles. Troisièmement, la théière doit être chauffée avant utilisation. La meilleure façon pour ce faire est de la placer sur un réchaud, plutôt que de la remplir d'eau chaude comme cela est fait plus généralement. Quatrièmement, le thé doit être fort. Pour une théière d'un litre, si vous comptez la remplir à ras bord, six cuillerées à café bombées semblent être la bonne quantité de thé. En pleine période de rationnement, on peut difficilement appliquer cette règle chaque jour de la semaine mais je maintiens qu'une tasse de thé bien fort vaut mille fois mieux que vingt tasses de jus de chaussette. Non seulement les vrais amateurs de thé aiment que leur thé soit fort, mai sil l'aiment un peu plus fort chaque année qui passe - preuve en est avec les tickets de rationnement supplémentaires que l'on accorde aux personnes âgées. Cinquièmement,  le thé doit être mis directement dans la théière. Pas de filtre, de sachets de mousseline ou d'autre système qui emprisonne le thé. Dans certains pays, on trouve sur les théières de petits paniers qui se balancent au bout du bec verseur pour rattraper les feuilles de thé qui s'échappent, car elles sont supposées mauvaises pour la santé. En réalité il faudrait en avaler des quantités considérables pour que cela produise un effet néfaste et si le thé n'est pas libre dans la théière, il n'infusera jamais comme il faut. Sixièmement, on doit amener la théière à la bouilloire et pas l'inverse. L'eau doit réellement bouillir au moment où elle atteint les feuilles, ce qui signifie qu'il faut garder la bouilloire sur le feu pendant qu'on verse l'eau sur le thé. Certaines personnes ajoutent qu'il ne faut utiliser que de l'eau qui n'a bouilli qu'une fois mais je n'ai jamais remarqué que cela faisait une différence. Septièmement, après avoir fait le thé, il faut le remuer ou, mieux encore, secouer la théière un bon coup avant de laisser les feuilles retomber. Huitièmement, il faut boire dans une bonne tasse de petit-déjeuner, c'est-à-dire une tasse de forme cylindrique et pas une de forme plus plate et peu profonde. La tasse à thé de petit-déjeuner contient plus de liquide alors qu'avec l'autre sorte le thé est toujours déjà presque froid lorsqu'on le commence. Neuvièmement, il faut écrémer le lait avant de l'utiliser pour le thé. Un lait trop crémeux donnera un goût écoeurant au thé. Dixièmement, il faut verser le thé dans la tasse en premier. De toutes ces règles, c'est l'une des plus sujettes à controverse. Dans chaque famille en Grande-Bretagne on trouvera probablement les deux écoles. Celle du lait en premier a des arguments assez convaincants mais je maintiens que le mien est indiscutable : en versant le thé en premier et en mélangeant en même temps qu'on verse le lait, on peut en mettre exactement la quantité qu'il faut alors qu'on risque de mettre trop de lait en faisant l'inverse. Dernièrement, le thé - à moins de le boire à la russe - se boit sans sucre. Je sais très bien que je me retrouve en minorité en disant cela, toutefois comment peut-on se déclarer un amateur de thé lorsque l'on y met du sucre ? Ce serait tout aussi déraisonnable d'y mettre du sel ou du poivre. Le thé est supposé être amer. Si vous le rendez plus doux, vous retirez au thé son goût et n'avez guère en bouche que celui du sucre. À la rigueur vous obtiendiez une boisson très similaire en dissolvant du sucre dans de l'eau chaude. Certaines personnes répondront qu'elles n'aiment pas le thé en lui-même, qu'elles ne le boivent que pour se réchauffer et se donner un coup de fouet et qu'elles ont besoin du sucre pour faire passer le goût du thé. À ces gens malavisés je répondrai la chose suivante  : essayez de boire votre thé sans sucre durant, disons, quinze jours et il est fort probable que vous ne voudrez plus jamais en ruiner le goût en le rendant plus doux après cela. Il ne s'agit pas là des seuls points qui prêtent à controverse, mais ils suffisent à montrer à quel point toute cette histoire est devenue subtile. Il y a aussi toute cette étiquette mystérieuse qui entoure la théière (pourquoi par exemple est-il considéré comme vulgaire de boire son thé dans sa sous-tasse ?) et on pourrait écrire des pages sur les autres utilisations du thé (lire l'avenir, prédire l'arrivée de visiteurs, nourrir les lapins, soigner les brûlures et nettoyer les tapis). Cela vaut le coup de faire attention à des détails tels que le fait de réchauffer la théière et d'utiliser de l'eau qui bout vraiment, afin de tirer réellement les vingt bonnes tasses de thé bien fort que représente une ration de deux onces, lorsqu'elle est bien utilisée.",

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

            HighScores = HighScores.OrderByDescending(x => x.Score).ToList();
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

                Func<HighScore, double> selector = x => x.Score;
                switch (unit)
                {
                    case "Score":
                        selector = x => x.Score;
                        break;
                    case "CPS":
                        selector = x => x.CPS;
                        break;
                    case "WPM":
                        selector = x => x.WPM;
                        break;
                    case "Accuracy":
                        selector = x => x.Accuracy;
                        break;
                }
                foreach (string nickname in nicknames)
                {
                    double mean = this.HighScores.Where(x => x.Nickname == nickname).Average(selector);
                    unitMeans.Add(nickname, mean);
                }
                allMeans.Add(unit, unitMeans.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value));
            }
            
            return allMeans;
        }
        public List<HighScore> GetPersonalScore(string givenNickname)
        {
            return this.HighScores.Where(x => x.Nickname == givenNickname).OrderBy(x => x.Date).ToList();
        }
    }
    public class ChartModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public ChartModel(DateTime dateTime, double value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }
    }
}
