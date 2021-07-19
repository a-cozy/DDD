using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DispImage.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class DispImage : UserControl
    {
        public DispImage(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<PubSubEvent<float>>().Subscribe((scale) =>
            {
                //scrollviewer.ScrollToHorizontalOffset(scrollviewer.ScrollableWidth / 2);
                //scrollviewer.ScrollToVerticalOffset(scrollviewer.ScrollableHeight / 2);

                ////// canvasサイズの変更
                ////canvas.Height *= scale;
                ////canvas.Width *= scale;

                //// canvasの拡大縮小
                //Matrix m0 = new Matrix();
                //m0.Scale(scale, scale);//元のサイズとの比
                                       //scaleTransform.Transform();

                //// scrollViewerのスクロールバーの位置をマウス位置を中心とする。
                //Point mousePoint = e.GetPosition(scrollViewer);
                //Double x_barOffset = (scrollViewer.HorizontalOffset + mousePoint.X) * scale - mousePoint.X;
                //scrollViewer.ScrollToHorizontalOffset(x_barOffset);

            });
            
        }
    }
}
