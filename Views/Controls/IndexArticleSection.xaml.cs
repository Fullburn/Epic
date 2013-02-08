using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace EpicProto
{
    /// <summary>
    /// Display control for a section of an article. Replaces appropriate text tags with hyperlinks.
    /// </summary>
    public partial class IndexArticleSection : UserControl
    {
        /// <summary>
        /// Constructor from Article.Section
        /// </summary>
        /// <param name="section">Section to be displayed.</param>
        public IndexArticleSection(Article.Section section)
        {
            InitializeComponent();

            this.SectionTitle.Text = section.Heading;

            string[] chunks = section.Text.Split('[', ']');
            foreach (string chunk in chunks)
            {
                Run contents = new Run(chunk);

                if (chunk.StartsWith(Article.ArticleIdTag))
                {
                    Match m = RegexArticle.Match(chunk);
                    if (m.Success)
                    {
                        contents.Text = m.Groups[ArticleTitleGroup].Value;

                        Hyperlink link = new Hyperlink(contents);
                        link.CommandParameter = uint.Parse(m.Groups[ArticleIdGroup].Value);

                        this.SectionContents.Inlines.Add(link);
                    }
                    else
                    {
                        this.SectionContents.Inlines.Add(contents);
                    }
                }
                else
                {
                    this.SectionContents.Inlines.Add(contents);
                }
            }
        }

        private const int ArticleIdGroup = 1;
        private const int ArticleTitleGroup = 2;
        private static Regex RegexArticle = new Regex(Article.ArticleIdTag + @"(\d+)\|([^\]]+)");
    }
}
