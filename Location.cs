using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace EpicProto
{
    public class Location : Article
    {
        public List<Location> childLocations;
        public Location parentLocation;

        [XmlIgnore]
        public BitmapImage map;
        public uint cornerX;
        public uint cornerY;

        public uint feetLat;
        public uint feetLong;

        public bool isRegion { get { return (this.feetLat * this.feetLong > 0); } }

        public float milesCornerX { get { return (this.cornerX / feetPerMile); } }
        public float milesCornerY { get { return (this.cornerY / feetPerMile); } }
        public float milesLat { get { return (this.feetLat / feetPerMile); } }
        public float milesLong { get { return (this.feetLong / feetPerMile); } }

        public Location()
        {
            map = new BitmapImage(new Uri("C:\\Users\\sh\\Documents\\Visual Studio 2010\\Projects\\Epic\\mainmap.jpg"));
            childLocations = new List<Location>();
            feetLat = (uint)(feetPerMile * map.PixelWidth);
            feetLong = (uint)(feetPerMile * map.PixelHeight);
        }

        public Location(double cornerX, double cornerY)
        {
            this.cornerX = (uint)(cornerX * feetPerMile);
            this.cornerY = (uint)(cornerY * feetPerMile);
        }

        public float PixelPositionX(double pixelX)
        {
            uint inFeet = (uint)(pixelX * feetLat / map.Width) + cornerX;
            return inFeet / feetPerMile;
        }

        public float PixelPositionY(double pixelY)
        {
            uint inFeet = (uint)(pixelY * feetLong / map.Height) + cornerY;
            return inFeet / feetPerMile;
        }

        private const float feetPerMile = 5280;
    }
}
