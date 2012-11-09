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
    /// </summary>
    public partial class MapPanel : UserControl
    {
        public MapPanel()
        {
            InitializeComponent();

            this.scaleTransform = new ScaleTransform();
            this.translateTransform = new TranslateTransform();

            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(this.scaleTransform);
            transformGroup.Children.Add(this.translateTransform);
            mapImage.RenderTransform = transformGroup;
            //mapImage.LayoutTransform = transformGroup;

            this.MouseLeftButtonDown += this.MapLeftMouseDown;
            this.MouseLeftButtonUp += this.MapLeftMouseUp;
            this.MouseMove += this.MapMouseMove;
            this.MouseWheel += this.MapMouseWheel;
        }

        protected void MapMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoomAdjust = e.Delta / 1000f;
            scaler *= (1 + zoomAdjust);

            if (scaler < 1) 
            {
                scaler = 1;
            }
            else if (scaler > 16)
            {
                scaler = 16;
            }

            this.scaleTransform.ScaleX = scaler;
            this.scaleTransform.ScaleY = scaler;
        }

        protected void MapLeftMouseDown(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            originX = position.X;
            originY = position.Y;

            this.CaptureMouse();
        }

        protected void MapLeftMouseUp(object sender, MouseEventArgs e)
        {
           this.ReleaseMouseCapture();
        }

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

        private void KeepImageOnScreen()
        {
            double rightEdge = this.ActualWidth - (mapImage.ActualWidth * scaler);
            double bottomEdge = this.ActualHeight - (mapImage.ActualHeight * scaler);

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

        private ScaleTransform scaleTransform;
        private TranslateTransform translateTransform;

        double scaler = 1.0f;
        double originX, originY;
    }
}
