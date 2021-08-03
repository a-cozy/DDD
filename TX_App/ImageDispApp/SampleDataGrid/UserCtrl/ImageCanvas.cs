using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDataGrid.UserCtrl
{
    class ImageCanvas : Canvas
    {


        //public int Zoom { get; set; }

        public ImageCanvas()
        {
            //Zoom = 1;
            ZoomRate = 1F;
            this.VisualEdgeMode = EdgeMode.Aliased;
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
        }

        ImageSource _source;
        public ImageSource Source
        {
            get { return _source; }
            set
            {
                _source = value;
                InvalidateVisual();
            }
        }

        public float ZoomRate
        {
            get { return (float)GetValue(ZoomRateProperty); }
            set { SetValue(ZoomRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZoomRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomRateProperty =
            DependencyProperty.Register("ZoomRate", typeof(float), typeof(ImageCanvas), new PropertyMetadata(1F));


        protected override void OnRender(DrawingContext dc)
        {
            if (_source == null)
                return;

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = ZoomRate;
            scaleTransform.ScaleY = ZoomRate;

            dc.PushTransform(scaleTransform);

            RenderOptions.SetEdgeMode(_source, EdgeMode.Unspecified);
            RenderOptions.SetBitmapScalingMode(_source, BitmapScalingMode.NearestNeighbor);

            int w = (int)(_source.Width * ZoomRate);
            int h = (int)(_source.Height * ZoomRate);

            dc.DrawImage(_source, new Rect(0, 0, _source.Width, _source.Height));

            dc.Pop();

            if (ZoomRate > 1)
            {
                for (int i = 0; i < w; i += (int)ZoomRate)
                {
                    dc.DrawLine(new Pen(Brushes.White, 1), new Point(i, 0), new Point(i, h));
                }

                for (int j = 0; j < h; j += (int)ZoomRate)
                {
                    dc.DrawLine(new Pen(Brushes.White, 1), new Point(0, j), new Point(w, j));
                }
            }
        }
    }
}
