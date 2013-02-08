using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace EpicProto
{
    /// <summary>
    /// An article describing a map coordinate.
    /// </summary>
    public class Location : Article
    {
        public uint FeetX { get; set; }
        public uint FeetY { get; set; }

        public double MilesX { get { return (this.FeetX / feetPerMile); } }
        public double MilesY { get { return (this.FeetY / feetPerMile); } }

        public Location()
        {
            StateManager.Current.AllLocations.Add(this);
            StateManager.Current.AllArticles.Add(this);
        }

        public Location(uint feetX, uint feetY)
        {
            this.FeetX = feetX;
            this.FeetY = feetY;

            StateManager.Current.AllLocations.Add(this);
            StateManager.Current.AllArticles.Add(this);
        }

        /*
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
        */
        private const float feetPerMile = 5280;
    }
}
