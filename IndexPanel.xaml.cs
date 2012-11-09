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
    /// Interaction logic for IndexPanel.xaml
    /// </summary>
    public partial class IndexPanel : UserControl
    {
        public IndexPanel()
        {
            InitializeComponent();
        }

        public bool ViewArticle { get; private set; }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<Article> characterMatch = StateManager.Current.AllCharacters.Where(c => c.Name.Contains(filter.Text, StringComparison.OrdinalIgnoreCase));
            IEnumerable<Article> chapterMatch = StateManager.Current.AllStoryEvents.Where(c => c.Name.Contains(filter.Text, StringComparison.OrdinalIgnoreCase));

            HomeView.CharacterSearchDisplay(characterMatch);
            HomeView.ChapterSearchDisplay(chapterMatch);
        }

        private void HomeToggle_Click(object sender, RoutedEventArgs e)
        {
            this.ViewArticle = false;
            this.HomeView.Visibility = Visibility.Visible;
            this.ArticleView.Visibility = Visibility.Collapsed;
        }


        private void SelectArticleCommand(object sender, ExecutedRoutedEventArgs e)
        {
            uint articleId = (uint)e.Parameter;
            Article article = StateManager.Current.AllArticles.Where(a => a.ArticleId == articleId).FirstOrDefault();
            
            ViewArticle = (article != null);
            if (this.ViewArticle)
            {
                this.ArticleView.SetArticle(article);
            }

            this.HomeView.Visibility = this.ViewArticle ? Visibility.Collapsed : Visibility.Visible;
            this.ArticleView.Visibility = this.ViewArticle ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
