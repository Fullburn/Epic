using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace EpicProto
{
    /// <summary>
    /// A map with image and world dimensions.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// File location of the main map.
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                this._filePath = value;
                this.MapImage = new BitmapImage(new Uri(value));
            }
        }

        public double MilesX { get; set; }
        public double MilesY { get; set; }

        public double FeetX
        {
            get
            {
                return this.MilesX * AppConstants.FeetPerMile;
            }
        }

        public double FeetY
        {
            get
            {
                return this.MilesY * AppConstants.FeetPerMile;
            }
        }

        [XmlIgnore]
        public ImageSource MapImage { get; private set; }


        private string _filePath;
    }
}
