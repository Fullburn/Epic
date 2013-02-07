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
    /// Interaction logic for MapPanel.xaml
    /// Pixel coordinate 0,0 refers to the top left corner of the panel.
    /// </summary>
    public partial class MapLocation : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MapLocation(Location location)
        {
            InitializeComponent();

            this.MouseLeftButtonDown += PinMouseDown;

            this.Location = location;
        }

        protected void PinMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                e.Handled = true;
            }
        }

        public Location Location { get; set; }
    }
}
