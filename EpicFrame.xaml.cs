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
using Microsoft.Win32;



namespace EpicProto
{
    /// <summary>
    /// Interaction logic for EpicFrame
    /// </summary>
    public partial class EpicFrame : Window
    {
        public EpicFrame()
        {
            InitializeComponent();
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                FileName = "world",
                DefaultExt = ".xml",
                Filter = "Xml Document|*.xml",
            };

            bool? openResult = open.ShowDialog();
            if (openResult == true)
            {
                StateManager.Load(open.FileName);
            }
        }

        private void SaveAsCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                FileName = "world",
                DefaultExt = ".xml",
                Filter = "Xml Document|*.xml",
            };

            bool? saveResult = save.ShowDialog();
            if (saveResult == true)
            {
                StateManager.Save(save.FileName);
            }
        }
    }
}
