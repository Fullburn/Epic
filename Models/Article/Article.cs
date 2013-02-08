using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicProto
{
    public abstract class Article
    {
        public const string ArticleIdTag = "##";

        public class Section
        {
            public string Heading { get; set; }

            public string Text { get; set; }
        }

        private static uint NextId = 1;

        /// <summary>
        /// Constructor used for deserialization.
        /// </summary>
        public Article()
        {
            this.Contents = new List<Section>();
            //this.ArticleId = NextId++;
            //StateManager.Current.AllArticles.Add(this);
        }

        public string Name { get; set; }

        public List<Section> Contents { get; set; }

        public uint ArticleId { get; set; }
    }
}
