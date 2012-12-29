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
    /// Interaction logic for IndexArticle.xaml
    /// </summary>
    public partial class IndexArticleSection : UserControl
    {
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
                    contents.Text = m.Groups[2].Value;
                    
                    Hyperlink link = new Hyperlink(contents);
                    link.CommandParameter = uint.Parse(m.Groups[1].Value);

                    this.SectionContents.Inlines.Add(link);
                }
                else
                {
                    this.SectionContents.Inlines.Add(contents);
                }
            }
        }

        private static Regex RegexArticle = new Regex(RegexArticleTag);
        private const string RegexArticleTag = @"##(\d+)\|([^\]]+)";
    }
}
