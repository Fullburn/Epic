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

namespace EpicProto
{
    /// <summary>
    /// Interaction logic for IndexArticle.xaml
    /// </summary>
    public partial class IndexArticle : UserControl
    {
        public IndexArticle()
        {
            InitializeComponent();
        }

        public void SetArticle(Article article)
        {
            this.ArticleTitle.Text = article.Name;
            this.Sections.Children.Clear();

            foreach (Article.Section section in article.Contents)
            {
                this.Sections.Children.Add(new IndexArticleSection(section));
            }
        }
    }
}
