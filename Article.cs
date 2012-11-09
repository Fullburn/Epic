using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicProto
{
    public abstract class Article
    {
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
            this.Links = new List<uint>();
            //this.ArticleId = NextId++;
            //StateManager.Current.AllArticles.Add(this);
        }

        public string Name { get; set; }

        public List<Section> Contents { get; set; }

        public uint ArticleId { get; set; }

        public List<uint> Links { get; set; }

        private const string ArticleIdTag = "##";
    }
}
