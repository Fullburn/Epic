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
    /// Interaction logic for IndexHome.xaml
    /// </summary>
    public partial class IndexHome : UserControl
    {
        public IndexHome()
        {
            InitializeComponent();

            CharacterSearch.DataContext = StateManager.Current.AllCharacters;
            ChapterSearch.DataContext = StateManager.Current.AllStoryEvents;
        }

        public void CharacterSearchDisplay(IEnumerable<Article> articleList)
        {
            CharacterSearch.DataContext = articleList;
        }

        public void ChapterSearchDisplay(IEnumerable<Article> articleList)
        {
            ChapterSearch.DataContext = articleList;
        }
    }
}
