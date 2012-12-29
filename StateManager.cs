using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace EpicProto
{
    /// <summary>
    /// Tracks the activity of the user with respect to the data. Marks certain data as active for specific purposes.
    ///  - Also handles load/save (for now).
    /// </summary>
    [
        XmlInclude(typeof(Character)),
        XmlInclude(typeof(Location)),
        XmlInclude(typeof(StoryEvent)),
    ]
    public class StateManager
    {
        public static StateManager Current { get; private set; }

        public static XmlSerializer Serializer { get; private set; }

        /// <summary>
        /// Static constructor, preparing singleton instance.
        /// </summary>
        static StateManager()
        {
            Current = new StateManager();
            Serializer = new XmlSerializer(typeof(StateManager));
        }

        /// <summary>
        /// Instance constructor.
        /// </summary>
        public StateManager()
        {
            this.Calendar = new CalendarSystem();
            this.AllArticles = new List<Article>();
            this.AllCharacters = new List<Character>();
            this.AllStoryEvents = new List<StoryEvent>();
        }

        /// <summary>
        /// Calendar system used for interpreting StoryEvent times.
        /// </summary>
        public CalendarSystem Calendar { get; set; }

        /// <summary>
        /// Master table of all articles.
        /// </summary>
        public List<Article> AllArticles { get; set; }

        /// <summary>
        /// Tracking subset of all articles that are characters.
        /// </summary>
        [XmlIgnore]        
        public List<Character> AllCharacters { get; set; }

        /// <summary>
        /// Tracking subset of all articles that are story events.
        /// </summary>
        [XmlIgnore]
        public List<StoryEvent> AllStoryEvents { get; set; }

        /// <summary>
        /// Currently selected article used for operations.
        /// </summary>
        [XmlIgnore]
        public Article CurrentArticle { get; set; }

        /// <summary>
        /// Clears all the state data from the state manager.
        /// </summary>
        public void Clear()
        {
            this.CurrentArticle = null;

            this.AllArticles.Clear();
            this.AllCharacters.Clear();
            this.AllStoryEvents.Clear();
        }

        /// <summary>
        /// Loads a World from a named file, replacing the existing world. 
        /// </summary>
        /// <param name="fileName">source file for save data.</param>
        public static void Load(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            StateManager.Current = (StateManager)Serializer.Deserialize(reader);
            reader.Close();

            StateManager.Current.Index();
            StateManager.CurrentFile = fileName;
        }

        /// <summary>
        /// Saves a World to the previously saved file.
        /// </summary>
        public static void Save()
        {
            Save(StateManager.CurrentFile);
        }

        /// <summary>
        /// Saves a World to a named file, overwriting if it already exists.
        /// </summary>
        /// <param name="fileName">destination file for save data</param>
        public static void Save(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            StateManager.Serializer.Serialize(writer, StateManager.Current);
            writer.Close();

            StateManager.CurrentFile = fileName;
        }

        /// <summary>
        /// Sets a selected article as being currently viewed or edited.
        /// </summary>
        /// <param name="id">ArticleID of the selected article</param>
        /// <returns>True if an article with a matching ID was found.</returns>
        public static bool SetCurrentArticle(uint id)
        {
            StateManager.Current.CurrentArticle = StateManager.Current.AllArticles.Where(a => a.ArticleId == id).First();
            return StateManager.Current.CurrentArticle != null;
        }

        /// <summary>
        /// Recreates type-specific indexes after deserializing.
        /// </summary>
        private void Index()
        {
            this.AllCharacters.Clear();
            this.AllStoryEvents.Clear();

            foreach (Article article in this.AllArticles)
            {
                if (article is Character)
                {
                    this.AllCharacters.Add((Character)article);
                }
                else if (article is StoryEvent)
                {
                    this.AllStoryEvents.Add((StoryEvent)article);
                }

                foreach (Article.Section section in article.Contents)
                {
                    Match m = Regex.Match(section.Text, StateManager.ArticleTagMatch, RegexOptions.IgnoreCase);
                    while (m.Success)
                    {
                        uint articleId = m.Groups[1].Success ? uint.Parse(m.Groups[1].Value) : 0;
                        string articleName = m.Groups[m.Groups.Count].Value;

                        if (articleId > 0)
                        {
                            article.Links.Add(articleId);
                        }
                        else
                        {
                            Article ar = StateManager.Current.AllArticles.Where(a => a.Name == m.Groups[2].Value).FirstOrDefault();
                            if (ar != null)
                            {
                                article.Links.Add(ar.ArticleId);
                            }
                        }

                        m = m.NextMatch();
                    }
                }
            }
        }

        /// <summary>
        /// Path to the world last loaded or saved.
        /// </summary>
        static string CurrentFile { get; set; }

        static Location CurrentMap { get; set; }

        static StoryEvent CurrentTime { get; set; }

        private const string ArticleTagMatch = @"\[(?:(?:##)(\d+))?\|?([^\]\n]+)\]";
    }
}
