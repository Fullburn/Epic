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
            ArticleTitle.Text = article.Name;
            ArticleContents.DataContext = article != null ? article.Contents : null;
        }
    }
}
