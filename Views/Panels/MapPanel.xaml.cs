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
    public partial class MapPanel : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MapPanel()
        {
            InitializeComponent();

            this.scaleTransform = new ScaleTransform();
            this.antiScaler = new ScaleTransform();
            this.translateTransform = new TranslateTransform();

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(this.scaleTransform);
            transformGroup.Children.Add(this.translateTransform);
            this.MapCanvas.RenderTransform = transformGroup;

            this.SizeChanged += this.MapSizeChanged;
            this.MouseLeftButtonDown += this.MapLeftMouseDown;
            this.MouseLeftButtonUp += this.MapLeftMouseUp;
            this.MouseMove += this.MapMouseMove;
            this.MouseWheel += this.MapMouseWheel;
        }

        protected void MapSizeChanged(object sender, EventArgs e)
        {
            this.KeepImageOnScreen();
        }

        /// <summary>
        /// Event handler for double click. Adds a pin to the map under the pointer.
        /// </summary>
        protected void MapDoubleClick(object sender, MouseEventArgs e)
        {
            Point panelPosition = e.GetPosition(this);
            double mapPositionX = (panelPosition.X - translateTransform.X) / scaler;
            double mapPositionY = (panelPosition.Y - translateTransform.Y) / scaler;
            double mapFractionX = mapPositionX / mapImage.Source.Width;
            double mapFractionY = mapPositionY / mapImage.Source.Height;
            uint feetX = (uint)(StateManager.Current.MainMap.FeetX * mapFractionX);
            uint feetY = (uint)(StateManager.Current.MainMap.FeetY * mapFractionY);
            Location l = new Location(feetX, feetY);
            this.AddPin(l);
        }


        /// <summary>
        /// Event handler for mouse wheel. Adjusts map zoom within fixed limits.
        /// </summary>
        protected void MapMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoomAdjust = e.Delta / 1000f;
            scaler *= (1 + zoomAdjust);

            scaler = Math.Min(16, Math.Max(1, scaler));

            this.scaleTransform.ScaleX = scaler;
            this.scaleTransform.ScaleY = scaler;

            this.antiScaler.ScaleX = 1 / scaler;
            this.antiScaler.ScaleY = 1 / scaler;

            KeepImageOnScreen();
        }

        /// <summary>
        /// Event handler for left mouse down. Begins map drag.
        /// </summary>
        protected void MapLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                Point position = e.GetPosition(this);
                originX = position.X;
                originY = position.Y;

                this.CaptureMouse();
            }
            else if (e.ClickCount == 2)
            {
                MapDoubleClick(sender, e);
            }
        }

        /// <summary>
        /// Event handler for left mouse up. Ends map drag.
        /// </summary>
        protected void MapLeftMouseUp(object sender, MouseEventArgs e)
        {
           this.ReleaseMouseCapture();
        }

        /// <summary>
        /// Event handler for mouse movement. Drags map if button is held down. Drag is limited by container edge.
        /// </summary>
        protected void MapMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsMouseCaptured)
            {
                Point position = e.GetPosition(this);

                translateTransform.X += (position.X - originX);
                translateTransform.Y += (position.Y - originY);
                originX = position.X;
                originY = position.Y;

                KeepImageOnScreen();
            }
        }

        public void LoadMap(Map map)
        {
            this.mapImage.Source = map.MapImage;
        }

        public void AddPin(Location location)
        {
            MapLocation ml = new MapLocation(location);
            TransformGroup elGroup = new TransformGroup();
            TranslateTransform fixPoint = new TranslateTransform();
            fixPoint.X = (location.FeetX / StateManager.Current.MainMap.FeetX) * mapImage.Source.Width;
            fixPoint.Y = (location.FeetY / StateManager.Current.MainMap.FeetY) * mapImage.Source.Height;

            elGroup.Children.Add(antiScaler);
            elGroup.Children.Add(fixPoint);
            ml.RenderTransform = elGroup;
            this.LocationCanvas.Children.Add(ml);
        }

        public void ClearPins()
        {
            this.LocationCanvas.Children.Clear();
        }

        /// <summary>
        /// Checks the map image boundaries vs. the panel boundaries. Blocks translation (drag) that would pull past the edge.
        /// </summary>
        private void KeepImageOnScreen()
        {
            if (this.mapImage.Source != null)
            {
                double rightEdge = this.ActualWidth - (mapImage.Source.Width * scaler);
                double bottomEdge = this.ActualHeight - (mapImage.Source.Height * scaler);

                if (rightEdge > 0)
                {
                    if (translateTransform.X < 0) { translateTransform.X = 0; }
                    if (translateTransform.X > rightEdge) { translateTransform.X = rightEdge; }
                }
                else
                {
                    if (translateTransform.X > 0) { translateTransform.X = 0; }
                    if (translateTransform.X < (rightEdge)) { translateTransform.X = rightEdge; }
                }

                if (bottomEdge > 0)
                {
                    if (translateTransform.Y < 0) { translateTransform.Y = 0; }
                    if (translateTransform.Y > bottomEdge) { translateTransform.Y = bottomEdge; }
                }
                else
                {
                    if (translateTransform.Y > 0) { translateTransform.Y = 0; }
                    if (translateTransform.Y < (bottomEdge)) { translateTransform.Y = bottomEdge; }
                }
            }
        }

        /// <summary>
        /// Render transform used for scaling (zooming) the map image.
        /// </summary>
        private ScaleTransform scaleTransform;

        /// <summary>
        /// Render transform used for scaling (zooming) the map image.
        /// </summary>
        private ScaleTransform antiScaler;

        /// <summary>
        /// Render transform used for translating (dragging) the map image.
        /// </summary>
        private TranslateTransform translateTransform;

        /// <summary>
        /// Scale factor for the map image within the panel.
        /// </summary>
        private double scaler = 1.0f;

        /// <summary>
        /// Coordinates prior to translation.
        /// </summary>
        private double originX, originY;

    }
}
